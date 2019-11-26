using System;

namespace FullModel.Data
{
	public class Balance
	{
		public Guid Id { get; set; } = Guid.NewGuid();

		public Guid PartitionKey { get; set; }

		public BalType Type { get; set; }

		public Amount Amount { get; set; }

		public DateTime CreatedDateTimeUTC { get; set; }

		public DateTime ModifiedDateTimeUTC { get; set; }
	}
}
