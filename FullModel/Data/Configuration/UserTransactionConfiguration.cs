using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullModel.Data.Configuration
{
	public class UserTransactionConfiguration : IEntityTypeConfiguration<UserTransaction>
	{
		public void Configure(EntityTypeBuilder<UserTransaction> builder)
		{
			builder.HasPartitionKey(e => e.PartitionKey);
			builder.Property(p => p.PartitionKey)
				.HasConversion<string>();

			builder.OwnsOne(e => e.Amount);

			builder.Property(p => p.Type)
				.HasConversion<string>();

			builder.OwnsMany(e => e.Attachments, a =>
			{

			});

			builder.OwnsOne(e => e.SecurityInfo, si =>
			{
				si.OwnsMany(e => e.Properties);
			});

			builder.OwnsOne(e => e.UnitPrice, up =>
			{

			});
		}
	}
}
