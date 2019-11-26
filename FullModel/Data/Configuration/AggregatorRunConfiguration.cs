using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullModel.Data.Configuration
{
	public class AggregatorRunConfiguration : IEntityTypeConfiguration<AggregatorRun>
	{
		public void Configure(EntityTypeBuilder<AggregatorRun> builder)
		{

			builder.HasPartitionKey(e => e.PartitionKey);
			builder.Property(p => p.PartitionKey)
				.HasConversion<string>();

			builder.HasIndex(p => p.CreatedDateTimeUTC);
		}
	}
}
