using Microsoft.Azure.Cosmos;

namespace FullModel.Repositories
{
	public class UserInstitutionPartitionRepository : CosmosRepositories
	{
		public UserInstitutionPartitionRepository(CosmosClient cosmosClient, string databaseId, string containerId) 
			: base(cosmosClient, databaseId, containerId)
		{
		}
	}
}
