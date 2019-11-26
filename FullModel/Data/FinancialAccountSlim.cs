using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullModel.Data
{
	public class FinancialAccountSlim
	{
		public FinancialAccountSlim()
		{
			CurrentBalances = new List<CurrentBalance>();
		}
		/// <summary>
		/// Unique ID to retrieve financial account details
		/// </summary>
		public Guid FinancialAccountId { get; set; }

		public string Name { get; set; }

		public string AcountNumber { get; set; }

		public AcctType Type { get; set; }

		public ExtAcctType ExtendedType { get; set; }

		public bool IsActive { get; set; }

		public bool IsOver90DaysOld { get; set; }

		public bool NeedsClassification { get; set; }

		public bool NeedsDeletion { get; set; }

		public ICollection<CurrentBalance> CurrentBalances { get; set; }

		public DateTime CreatedDateTimeUTC { get; set; }

		public DateTime ModifiedDateTimeUTC { get; set; }
	}
}
