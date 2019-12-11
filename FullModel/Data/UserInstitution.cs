using FullModel.Repositories;
using System;
using System.Collections.Generic;

namespace FullModel.Data
{
	public class UserInstitution : CosmosEntity
	{
		public UserInstitution()
		{
			FinancialAccounts = new HashSet<FinancialAccountSlim>();
			EntityType = nameof(UserInstitution);
		}

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

		public DateTime ModifiedDateTimeUTC { get; set; }
	}
}
