using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullModel.Data
{
	public class UserInstitution
	{
		public UserInstitution()
		{
			FinancialAccounts = new HashSet<FinancialAccountSlim>();
		}

		public Guid Id { get; set; } = Guid.NewGuid();

		public Guid PartitionKey { get; set; }

		public Guid UserId { get; set; }

		public long FinancialInstitutionLoginAccountId { get; set; }

		public InstitutionSlim Institution { get; set; }

		public ICollection<FinancialAccountSlim> FinancialAccounts { get; set; }

		public DateTime? CredentialUpdateRequestDateTimeUTC { get; set; }

		public string HarvestAddId { get; set; }

		public long RunId { get; set; }

		public bool Active { get; set; }

		public bool HasMfa { get; set; }

		public bool HasInvalidCredentials { get; set; }

		public string LoginParameterHash { get; set; }

		public DateTime CreatedDateTimeUTC { get; set; } = DateTime.UtcNow;

		public DateTime ModifiedDateTimeUTC { get; set; }
	}
}
