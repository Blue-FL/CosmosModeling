using FullModel.Repositories;
using System;
using System.Collections.Generic;

namespace FullModel.Data
{
	public class UserTransaction : CosmosEntity
	{
		public UserTransaction()
		{
			EntityType = nameof(UserTransaction);

			Attachments = new HashSet<Attachment>();
		}

		public Guid FinancialAccountId { get; set; }

		public Amount Amount { get; set; }

		public string CheckNumber { get; set; }

		public string Memo { get; set; }

		public DateTime PostedDate { get; set; }

		public string Category { get; set; }

		public string Subcategory { get; set; }

		public long AggregatorTransactionId { get; set; }

		public TransactionType Type { get; set; }

		public bool IsRemoved { get; set; }

		public DateTime? RemovedDateTimeUTC { get; set; }

		public DateTime ModifiedDateTimeUTC { get; set; }

		public ICollection<Attachment> Attachments { get; set; }

		public string Note { get; set; }

		public SecurityInfo SecurityInfo { get; set; }

		public string Action { get; set; }

		public Amount UnitPrice { get; set; }

		public double Units { get; set; }
	}
}
