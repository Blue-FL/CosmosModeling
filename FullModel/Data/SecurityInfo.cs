using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullModel.Data
{
	public class SecurityInfo
	{
		public SecurityInfo()
		{
			Properties = new HashSet<Property>();
		}

		public bool Marginable { get; set; }

		public string Description { get; set; }

		public string Type { get; set; }

		public string Ticker { get; set; }

		public ICollection<Property> Properties { get; set; }
	}
}
