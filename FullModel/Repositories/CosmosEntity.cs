using Newtonsoft.Json;
using System;

namespace FullModel.Repositories
{
	public abstract class CosmosEntity
	{
		[JsonProperty("id")]
		public string Id { get; set; } = Guid.NewGuid().ToString();

		public string PartitionKey { get; set; }

		public string EntityType { get; set; }

		public DateTime CreatedDateTimeUTC { get; set; } = DateTime.UtcNow;
	}
}
