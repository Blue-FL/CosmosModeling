using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullModel.Data
{
	public class AggregatorRun
	{
		/// <summary>
		/// Id to uniquely identify the aggregator run
		/// </summary>
		public Guid Id { get; set; } = Guid.NewGuid();

		/// <summary>
		/// Status of the run
		/// </summary>
		public RunStatus RunStatus { get; set; }

		/// <summary>
		/// User's unique ID. Used as the partition key
		/// </summary>
		public Guid PartitionKey { get; set; }

		/// <summary>
		/// Unique ID of the institution for the run
		/// </summary>
		public Guid InstitutionId { get; set; }

		/// <summary>
		/// Unique ID of the user institution for the run
		/// </summary>
		public Guid? UserInstitutionId { get; set; }

		public DateTime CreatedDateTimeUTC { get; set; }

		public DateTime ModifiedDateTimeUTC { get; set; }
	}
}
