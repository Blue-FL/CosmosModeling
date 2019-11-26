using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FullModel.Data.Configuration
{
	public class UserInstitutionConfiguration : IEntityTypeConfiguration<UserInstitution>
	{
		public void Configure(EntityTypeBuilder<UserInstitution> builder)
		{
			builder.HasPartitionKey(e => e.PartitionKey);
			builder.Property(p => p.PartitionKey)
				.HasConversion<string>();

			builder.OwnsOne(e => e.Institution, i =>
			{
				i.ToJsonProperty("Institution");
			});

			builder.OwnsMany(e => e.FinancialAccounts, fa =>
			{
				fa.ToJsonProperty("FinancialAccount");
				fa.OwnsMany(e => e.CurrentBalances, cb =>
				{
					cb.OwnsOne(e => e.Amount);
					cb.Property(p => p.Type)
						.HasConversion<string>();
				});
			});
		}
	}
}
