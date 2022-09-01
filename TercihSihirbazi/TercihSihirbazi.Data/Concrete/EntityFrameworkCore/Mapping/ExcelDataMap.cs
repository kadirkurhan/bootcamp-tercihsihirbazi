using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TercihSihirbazi.Entities.Concrete;

namespace TercihSihirbazi.Data.Concrete.EntityFrameworkCore.Mapping
{
    public class ExcelDataMap : IEntityTypeConfiguration<DetailObject>
    {
        public void Configure(EntityTypeBuilder<DetailObject> builder)
        {
            builder.HasKey(I => I.Id);
            builder.Property(I => I.Id).UseIdentityColumn();

            builder.Property(I => I.Year2018).IsRequired(required: false);
            builder.Property(I => I.Year2019).IsRequired(required: false);
            builder.Property(I => I.Year2020).IsRequired(required: false);
            builder.Property(I => I.Year2021).IsRequired(required: false);
            builder.Property(I => I.Year2022).IsRequired(required: false);
            builder.Property(I => I.Year2023).IsRequired(required: false);
        }
    }
}
