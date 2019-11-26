using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullModel.Data.Configuration
{
	public class BalanceConfiguration : IEntityTypeConfiguration<Balance>
	{
		public void Configure(EntityTypeBuilder<Balance> builder)
		{
			builder.Property(p => p.PartitionKey)
				.HasConversion<string>();
			builder.HasPartitionKey(e => e.PartitionKey);

			builder.Property(p => p.Type)
				.HasConversion<string>();

			builder.OwnsOne(e => e.Amount, a =>
			{

			});
		}
	}
}
