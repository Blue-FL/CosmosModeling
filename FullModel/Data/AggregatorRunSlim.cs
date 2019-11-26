using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullModel.Data
{
	public class AggregatorRunSlim
	{
		/// <summary>
		/// Unique ID of the aggregator run
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Status of the run. Updated by the change feed
		/// </summary>
		public RunStatus RunStatus { get; set; }
	}
}
