using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullModel.Data
{
	public class Institution
	{
		public Institution()
		{
			LoginParameters = new HashSet<LoginParameter>();
		}
		public int Id { get; set; }
		public string Name { get; set; }
		public string ImageUrl { get; set; }
		public string FinancialInstitutionId { get; set; }
		public string UrlName { get; set; }
		public string UrlValue { get; set; }
		public string Country { get; set; }
		public bool Crawlable { get; set; }
		public bool Harvestable { get; set; }
		public int EnrollmentCount { get; set; }
		public bool Archived { get; set; }
		public string RoutingNumber { get; set; }
		public DateTime CreatedDateTimeUtc { get; set; }
		public DateTime ModifiedDateTimeUtc { get; set; }

		public string Type { get; set; }

		public ICollection<LoginParameter> LoginParameters { get; set; }
	}
}
