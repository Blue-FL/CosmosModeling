using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FullModel.Data.Configuration
{
	public class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.Property(p => p.PartitionKey)
				.HasConversion<string>();
			builder.HasPartitionKey(e => e.PartitionKey);

			builder.OwnsOne(e => e.FiservUser, fu =>
			{
				fu.ToJsonProperty("FiservUser");
			});

			builder.OwnsMany(e => e.UserDefinedTransactionSubcategories, udc =>
			{
				udc.ToJsonProperty("UserDefinedSubcategories");
			});

			builder.OwnsMany(e => e.AggregatorRuns, ar =>
			{

			});
		}
	}
}
