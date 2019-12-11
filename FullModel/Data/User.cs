using FullModel.Repositories;
using System;
using System.Collections.Generic;

namespace FullModel.Data
{
	public class User : CosmosEntity
	{
		public User()
		{
			UserDefinedTransactionSubcategories = new HashSet<UserDefinedTransactionSubcategory>();
			AggregatorRuns = new HashSet<AggregatorRunSlim>();
			EntityType = nameof(User);
		}

		public FiservUser FiservUser { get; set; }

		public ICollection<UserDefinedTransactionSubcategory> UserDefinedTransactionSubcategories { get; set; }

		public ICollection<AggregatorRunSlim> AggregatorRuns { get; set; }

		public DateTime ModifiedDateTimeUTC { get; set; }
	}
}
