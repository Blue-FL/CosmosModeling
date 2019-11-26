using System;

namespace FullModel.Data
{
	public class Coverage
	{
		public string Type { get; set; }

		public string Limit { get; set; }

		public Amount Amount { get; set; }

		public DateTime CreatedDateTimeUTC { get; set; }

		public DateTime ModifiedDateTimeUTC { get; set; }
	}
}
