using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullModel.Data
{
	public class CoveredElement
	{
		public CoveredElement()
		{
			Coverages = new HashSet<Coverage>();
		}

		public string Name { get; set; }

		public string UniqueId { get; set; }

		public int? Years { get; set; }

		public ICollection<Coverage> Coverages { get; set; }

		public DateTime CreatedDateTimeUTC { get; set; }

		public DateTime ModifiedDateTimeUTC { get; set; }
	}
}
