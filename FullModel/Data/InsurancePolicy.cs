using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullModel.Data
{
	public class InsurancePolicy
	{
		public InsurancePolicy()
		{
			InsuredPeople = new HashSet<InsuredPerson>();
			Beneficiaries = new HashSet<Beneficiary>();
			CoverageElements = new HashSet<CoveredElement>();
		}
		public string PolicyType { get; set; }

		public string PolicyNumber { get; set; }

		public string PolicyOwnerName { get; set; }

		public string PolicyStatus { get; set; }

		public string Description { get; set; }

		public int? PaymentsPerYear { get; set; }

		public int? TermInYears { get; set; }

		public int? EliminationPeriodInDays { get; set; }

		public int? BenefitPeriodInDays { get; set; }

		public int? BenefitPeriodAge { get; set; }

		public string OwnOccupation { get; set; }

		public string BenefitTaxable { get; set; }

		public string ColaType { get; set; }

		public double? ColaRate { get; set; }

		public int? PremiumTermInYears { get; set; }

		public DateTime CreatedDateTimeUTC { get; set; }

		public DateTime ModifiedDateTimeUTC { get; set; }

		public ICollection<InsuredPerson> InsuredPeople { get; set; }

		public ICollection<Beneficiary> Beneficiaries { get; set; }

		public ICollection<CoveredElement> CoverageElements { get; set; }
	}
}
