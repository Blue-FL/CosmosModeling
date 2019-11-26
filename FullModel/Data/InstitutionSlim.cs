using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullModel.Data
{
	public class InstitutionSlim
	{
		/// <summary>
		/// Unique ID for institution to get details
		/// </summary>
		public Guid InstitutionId { get; set; }

		public string Name { get; set; }

		public string ImageUrl { get; set; }
	}
}
