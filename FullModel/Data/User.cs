using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullModel.Data
{
	public class User
	{
		public User()
		{
			UserDefinedTransactionSubcategories = new HashSet<UserDefinedTransactionSubcategory>();
			AggregatorRuns = new HashSet<AggregatorRunSlim>();
		}
		public Guid Id { get; set; } = Guid.NewGuid();

		public Guid PartitionKey { get; set; }

		public FiservUser FiservUser { get; set; }

		public ICollection<UserDefinedTransactionSubcategory> UserDefinedTransactionSubcategories { get; set; }

		public ICollection<AggregatorRunSlim> AggregatorRuns { get; set; }

		public DateTime CreatedDateTimeUTC { get; set; }

		public DateTime ModifiedDateTimeUTC { get; set; }
	}
}
