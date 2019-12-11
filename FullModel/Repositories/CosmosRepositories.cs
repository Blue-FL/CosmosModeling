using Microsoft.Azure.Cosmos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FullModel.Extensions;
using System;

namespace FullModel.Repositories
{
    public abstract class CosmosRepositories
	{
		protected readonly CosmosClient _cosmosClient;
		protected readonly Container _container;

        private static int MAX_BATCH = 100;

		public CosmosRepositories(CosmosClient cosmosClient, string databaseId, string containerId)
		{
			_cosmosClient = cosmosClient;

			_container = _cosmosClient.GetContainer(databaseId, containerId);
            var currentContainerThroughput = _container.ReadThroughputAsync().GetAwaiter().GetResult();

            var taskCount = Math.Max(currentContainerThroughput.Value / 1000, 1);
            taskCount = Math.Min(taskCount, Environment.ProcessorCount * 50);
        }

        public async Task<ItemResponse<T>> AddItemAsync<T>(T item) where T : CosmosEntity
        {
            return await this._container.CreateItemAsync(item, new PartitionKey(item.PartitionKey.ToString())); 
        }

        public async Task AddItemsAsync<T>(ICollection<T> items) where T : CosmosEntity
        {
            var chunks = items.ChunkBy(MAX_BATCH);
            var chunkTasks = new List<Task>();
            foreach (var chunk in chunks)
            {
                chunkTasks.Add(SaveChunk(chunk));
            }
            await Task.WhenAll(chunkTasks);
        }

        private async Task<IEnumerable<ItemResponse<T>>> SaveChunk<T>(List<T> chunk) where T : CosmosEntity
        {
            List<Task<ItemResponse<T>>> tasks = new List<Task<ItemResponse<T>>>();
            foreach (var item in chunk)
            {
                tasks.Add(_container.CreateItemAsync(item, new PartitionKey(item.PartitionKey.ToString())));
            }

            return await Task.WhenAll(tasks);
        }

        public async Task DeleteItemAsync<T>(string id) where T : CosmosEntity
        {
            await this._container.DeleteItemAsync<T>(id, new PartitionKey(id));
        }

        public async Task<T> GetItemAsync<T>(string id, Guid partitionKey) where T : CosmosEntity
        {
            try
            {
                ItemResponse<T> response = await this._container.ReadItemAsync<T>(id, new PartitionKey(partitionKey.ToString()));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

        }

        public async Task<(IEnumerable<T>, List<CosmosDiagnostics>)> GetItemsAsync<T>(string queryString) where T : CosmosEntity
        {
            var query = this._container.GetItemQueryIterator<T>(new QueryDefinition(queryString));
            List<T> results = new List<T>();
            var diagnostics = new List<CosmosDiagnostics>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                diagnostics.Add(response.Diagnostics);
                results.AddRange(response.ToList());
            }

            return (results, diagnostics);
        }

        public async Task UpdateItemAsync<T>(T item) where T : CosmosEntity
        {
            await this._container.UpsertItemAsync<T>(item, new PartitionKey(item.PartitionKey.ToString()));
        }
    }
}
