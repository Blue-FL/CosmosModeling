using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullModel.Data
{
	public class TransactionCategory
	{
		public TransactionCategory()
		{
			Subcategories = new HashSet<TransactionSubcategory>();
		}
		public Guid Id { get; set; } = Guid.NewGuid();

		public string Type { get; set; }

		public string Name { get; set; }

		public int CategoryId { get; set; }

		public ICollection<TransactionSubcategory> Subcategories { get; set; }

		public DateTime CreatedDateTimeUTC { get; set; }

		public DateTime ModifiedDateTimeUTC { get; set; }
	}
}
