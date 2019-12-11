using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullModel.Repositories
{
	public class UserPartitionRepository : CosmosRepositories
	{
		public UserPartitionRepository(CosmosClient cosmosClient, string databaseId, string containerId) 
			: base(cosmosClient, databaseId, containerId)
		{
		}
	}
}
