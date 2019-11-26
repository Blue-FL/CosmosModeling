using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FullModel.Data.Configuration
{
	public class FinancialAccountConfiguration : IEntityTypeConfiguration<FinancialAccount>
	{
		public void Configure(EntityTypeBuilder<FinancialAccount> builder)
		{
			builder.Property(p => p.PartitionKey)
				.HasConversion<string>();
			builder.HasPartitionKey(e => e.PartitionKey);

			builder.Property(p => p.Type)
				.HasConversion<string>();

			builder.Property(p => p.ExtendedType)
				.HasConversion<string>();

			builder.OwnsOne(e => e.InsurancePolicy, ip =>
			{
				ip.OwnsMany(e => e.InsuredPeople);
				ip.OwnsMany(e => e.Beneficiaries);
				ip.OwnsMany(e => e.CoverageElements, ce =>
				{
					ce.OwnsMany(e => e.Coverages, c =>
					{
						c.OwnsOne(e => e.Amount);
					});
				});
			});

			builder.OwnsMany(e => e.Properties);

			builder.OwnsMany(e => e.Owners);

			builder.OwnsMany(e => e.CurrentInvestmentPositions, ip =>
			{
				ip.OwnsOne(e => e.SecurityInfo, si =>
				{
					si.OwnsMany(e => e.Properties);
				});
				ip.OwnsOne(e => e.SecurityPosition, sp =>
				{
					sp.OwnsOne(e => e.CostBasis);
					sp.OwnsOne(e => e.MarketValue);
					sp.OwnsOne(e => e.UnitPrice);
				});
			});

			builder.OwnsMany(e => e.CurrentMutualFundPositions, ip =>
			{
				ip.OwnsOne(e => e.SecurityInfo, si =>
				{
					si.OwnsMany(e => e.Properties);
				});
				ip.OwnsOne(e => e.SecurityPosition, sp =>
				{
					sp.OwnsOne(e => e.CostBasis);
					sp.OwnsOne(e => e.MarketValue);
					sp.OwnsOne(e => e.UnitPrice);
				});
			});
		}
	}
}
