using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullModel.Data.Configuration
{
	public class InstitutionConfiguration : IEntityTypeConfiguration<Institution>
	{
		public void Configure(EntityTypeBuilder<Institution> builder)
		{
			builder.HasPartitionKey(e => e.Type);

			builder.OwnsMany(e => e.LoginParameters, lp =>
			{

			});
		}
	}
}
