using System;

namespace FullModel.Data
{
	public class Beneficiary
	{
		public string Name { get; set; }

		public decimal Percentage { get; set; }

		public DateTime CreatedDateTimeUTC { get; set; }

		public DateTime ModifiedDateTimeUTC { get; set; }
	}
}
