using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullModel.Data
{
	public class SecurityPosition
	{
		public Amount CostBasis { get; set; }

		public Amount MarketValue { get; set; }

		public Amount UnitPrice { get; set; }

		public double? Units { get; set; }
	}
}
