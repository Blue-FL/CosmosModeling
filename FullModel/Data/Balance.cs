using FullModel.Repositories;
using System;

namespace FullModel.Data
{
	public class Balance : CosmosEntity
	{
		public Balance()
		{
			EntityType = nameof(Balance);
		}

		public BalType Type { get; set; }

		public Amount Amount { get; set; }

		public DateTime ModifiedDateTimeUTC { get; set; }
	}
}
