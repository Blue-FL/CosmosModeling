using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace FullModel.Data
{
	public class FinancialAggregationContext : DbContext
	{
		public FinancialAggregationContext([NotNull] DbContextOptions options) 
			: base(options)
		{
		}

		public DbSet<AggregatorRun> AggregatorRuns { get; set; }

		public DbSet<UserInstitution> UserInstitutions { get; set; }

		public DbSet<FinancialAccount> FinancialAccounts { get; set; }

		public DbSet<Transaction> Transactions { get; set; }

		public DbSet<UserTransaction> UserTransactions { get; set; }

		public DbSet<User> Users { get; set; }

		public DbSet<TransactionCategory> TransactionCategories { get; set; }

		public DbSet<Balance> Balances { get; set; }

		public DbSet<Institution> Institutions { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// User ID partition key
			_ = modelBuilder.Entity<AggregatorRun>().ToContainer(DataConstants.USER_CONTAINER);
			_ = modelBuilder.Entity<UserInstitution>().ToContainer(DataConstants.USER_CONTAINER);
			_ = modelBuilder.Entity<User>().ToContainer(DataConstants.USER_CONTAINER);
			_ = modelBuilder.Entity<UserTransaction>().ToContainer(DataConstants.USER_CONTAINER);

			// user institution ID partition key
			_ = modelBuilder.Entity<FinancialAccount>().ToContainer(DataConstants.USER_INSTITUTION_CONTAINER);

			// Financial account ID partition key
			_ = modelBuilder.Entity<Transaction>().ToContainer(DataConstants.FINANCIAL_ACCOUNT_CONTAINER);
			_ = modelBuilder.Entity<Balance>().ToContainer(DataConstants.FINANCIAL_ACCOUNT_CONTAINER);

			// Type partition key
			_ = modelBuilder.Entity<TransactionCategory>().ToContainer(DataConstants.CATEGORY_CONTAINER);
			
			// Type partition key
			_ = modelBuilder.Entity<Institution>().ToContainer(DataConstants.INSTITUTION_CONTAINER);

			_ = modelBuilder.ApplyConfigurationsFromAssembly(typeof(FinancialAggregationContext).Assembly);
			base.OnModelCreating(modelBuilder);
		}
	}
}
