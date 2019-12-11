using FullModel.Configuration;
using FullModel.Data;
using FullModel.Repositories;
using FullModel.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace FullModel
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			var cosmosConfiguration = new CosmosConfiguration();
			Configuration.GetSection("Cosmos").Bind(cosmosConfiguration);

			var testConfiguration = new TestConfiguration();
			Configuration.GetSection("TestData").Bind(testConfiguration);
			services.AddSingleton(testConfiguration);

			services.AddControllers();

			var cosmosClient = InitializeCosmosDb(cosmosConfiguration).GetAwaiter().GetResult();

			services.AddSingleton(new UserPartitionRepository(cosmosClient, cosmosConfiguration.Database, DataConstants.USER_CONTAINER));
			services.AddSingleton(new UserInstitutionPartitionRepository(cosmosClient, cosmosConfiguration.Database, DataConstants.USER_INSTITUTION_CONTAINER));
			services.AddSingleton(new FinancialAccountPartitionRepository(cosmosClient, cosmosConfiguration.Database, DataConstants.FINANCIAL_ACCOUNT_CONTAINER));
			services.AddHostedService<StartupService>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}

		private async Task<CosmosClient> InitializeCosmosDb(CosmosConfiguration cosmosConfiguration)
		{
			var cosmosClient = new CosmosClientBuilder(cosmosConfiguration.Endpoint, cosmosConfiguration.Key)
				.WithBulkExecution(true)
				.WithConnectionModeDirect()
				.WithThrottlingRetryOptions(TimeSpan.FromSeconds(10), 100)
				.Build();

			var databaseResponse = await cosmosClient.CreateDatabaseIfNotExistsAsync(cosmosConfiguration.Database);

			// Set up containers
			await databaseResponse.Database
				.DefineContainer(DataConstants.USER_CONTAINER, PartitionConstants.PARTITION_KEY)
				.WithIndexingPolicy()
					.WithIndexingMode(IndexingMode.Consistent)
					.WithIncludedPaths()
						.Path("/PartititionKey/?")
						.Path("/CreatedDateTimeUTC/?")
						.Path("/EntityType/?")
						.Path("/Category/?")
						.Path("/Subcategory/?")
						.Path("/FinancialAccountId/?")
						.Attach()
					.WithExcludedPaths()
						.Path("/*")
						.Attach()
					.Attach()
				.CreateIfNotExistsAsync(cosmosConfiguration.Throughput);

			await databaseResponse.Database
				.DefineContainer(DataConstants.USER_INSTITUTION_CONTAINER, PartitionConstants.PARTITION_KEY)
				.WithIndexingPolicy()
					.WithIndexingMode(IndexingMode.Consistent)
					.WithIncludedPaths()
						.Path("/PartititionKey/?")
						.Path("/CreatedDateTimeUTC/?")
						.Path("/EntityType/?")
						.Attach()
					.WithExcludedPaths()
						.Path("/*")
						.Attach()
					.Attach()
				.CreateIfNotExistsAsync(cosmosConfiguration.Throughput);

			await databaseResponse.Database
				.DefineContainer(DataConstants.FINANCIAL_ACCOUNT_CONTAINER, PartitionConstants.PARTITION_KEY)
				.WithIndexingPolicy()
					.WithIndexingMode(IndexingMode.Consistent)
					.WithIncludedPaths()
						.Path("/PartititionKey/?")
						.Path("/CreatedDateTimeUTC/?")
						.Path("/EntityType/?")
						.Path("/Category/?")
						.Path("/Subcategory/?")
						.Path("/FinancialAccountId/?")
						.Attach()
					.WithExcludedPaths()
						.Path("/*")
						.Attach()
					.Attach()
				.CreateIfNotExistsAsync(cosmosConfiguration.Throughput);

			await databaseResponse.Database
				.DefineContainer(DataConstants.CATEGORY_CONTAINER, PartitionConstants.PARTITION_KEY)
				.WithIndexingPolicy()
					.WithIndexingMode(IndexingMode.Consistent)
					.WithIncludedPaths()
						.Path("/PartititionKey/?")
						.Path("/CreatedDateTimeUTC/?")
						.Attach()
					.WithExcludedPaths()
						.Path("/*")
						.Attach()
					.Attach()
				.CreateIfNotExistsAsync(cosmosConfiguration.Throughput);

			await databaseResponse.Database
				.DefineContainer(DataConstants.INSTITUTION_CONTAINER, PartitionConstants.PARTITION_KEY)
				.WithIndexingPolicy()
					.WithIndexingMode(IndexingMode.Consistent)
					.WithIncludedPaths()
						.Path("/PartititionKey/?")
						.Path("/CreatedDateTimeUTC/?")
						.Attach()
					.WithExcludedPaths()
						.Path("/*")
						.Attach()
					.Attach()
				.CreateIfNotExistsAsync(cosmosConfiguration.Throughput);

			return cosmosClient;
		}
	}
}
