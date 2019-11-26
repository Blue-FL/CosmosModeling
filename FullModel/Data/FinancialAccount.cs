﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullModel.Data
{
	public class FinancialAccount
	{
		public FinancialAccount()
		{
			Properties = new HashSet<Property>();
			Owners = new HashSet<Owner>();
			CurrentInvestmentPositions = new HashSet<InvestmentPosition>();
			CurrentMutualFundPositions = new HashSet<MutualFundPosition>();
		}

		/// <summary>
		/// Unique ID for a financial account
		/// </summary>
		public Guid Id { get; set; } = Guid.NewGuid();

		/// <summary>
		/// User Institution key
		/// </summary>
		public Guid PartitionKey { get; set; }

		public bool IsActive { get; set; }

		public string AggregatorAccountId { get; set; }

		public DateTime? AggregatorLastUpdateAttempt { get; set; }

		public DateTime? AggregatorLastUpdateSuccess { get; set; }

		public string Name { get; set; }

		public string NickName { get; set; }

		public string Number { get; set; }

		public AcctType Type { get; set; }

		public ExtAcctType ExtendedType { get; set; }

		public string RetirementStatus { get; set; }

		public string Instrument { get; set; }

		public string BalanceCalcType { get; set; }

		public bool BalanceCalcFlipped { get; set; }

		public bool DoNotCalculateBalances { get; set; }

		public bool DoNotDisplayBalances { get; set; }

		public string DoNotDisplayBalancesReason { get; set; }

		public bool IsOver90DaysOld { get; set; }

		public bool NeedsClassification { get; set; }

		public bool NeedsDeletion { get; set; }

		public DateTime CreatedDateTimeUTC { get; set; }

		public DateTime ModifiedDateTimeUTC { get; set; }

		public InsurancePolicy InsurancePolicy { get; set; }

		public ICollection<Property> Properties { get; set; }

		public ICollection<Owner> Owners { get; set; }

		public ICollection<InvestmentPosition> CurrentInvestmentPositions { get; set; }

		public ICollection<MutualFundPosition> CurrentMutualFundPositions { get; set; }
	}
}
