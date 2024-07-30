using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MyFinances.Spendings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinances.Configurations.Spendings
{
    public class SpendingConfiguration : IEntityTypeConfiguration<Spending>
    {
        public void Configure(EntityTypeBuilder<Spending> builder)
        {
            builder.ToTable("Spendings");

            builder.HasKey(c => c.Id);

            builder.Property(x => x.Categoria).HasColumnType("NVARCHAR").HasMaxLength(150);
            builder.Property(x => x.Descricao).HasColumnType("NVARCHAR").HasMaxLength(200);

            builder.HasOne(s => s.User)
                  .WithMany(u => u.Spendings)
                  .HasForeignKey(s => s.UserId)
                  .IsRequired();
        }
    }
}
