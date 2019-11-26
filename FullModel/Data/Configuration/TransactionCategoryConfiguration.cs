using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullModel.Data.Configuration
{
	public class TransactionCategoryConfiguration : IEntityTypeConfiguration<TransactionCategory>
	{
		public void Configure(EntityTypeBuilder<TransactionCategory> builder)
		{

			builder.HasPartitionKey(e => e.Type);

			builder.OwnsMany(e => e.Subcategories);
		}
	}
}
