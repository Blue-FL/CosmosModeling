namespace FullModel.Configuration
{
	public class CosmosConfiguration
	{
		public string Endpoint { get; set; }

		public string Key { get; set; }

		public string Database { get; set; }

		public int Throughput { get; set; } = 10000;
	}
}
