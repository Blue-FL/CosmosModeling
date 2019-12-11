using Microsoft.Azure.Cosmos;

namespace FullModel.Repositories
{
	public class FinancialAccountPartitionRepository : CosmosRepositories
	{
		public FinancialAccountPartitionRepository(CosmosClient cosmosClient, string databaseId, string containerId) 
			: base(cosmosClient, databaseId, containerId)
		{
		}
	}
}
