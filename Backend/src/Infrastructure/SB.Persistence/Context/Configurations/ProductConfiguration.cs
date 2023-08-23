using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SB.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SB.Domain.Entities.Products;

namespace SB.Persistence.Context.Configurations
{
    public partial class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> entity)
        {
            entity.ToTable("Product");

            entity.Property(x => x.Id).HasColumnName("ProductId");

            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.Property(e => e.Code)
                .HasMaxLength(101)
                .IsUnicode(false);

            entity.Property(e => e.Description)
                .HasMaxLength(101)
                .IsUnicode(false);

            entity.Property(e => e.Name)
     .HasMaxLength(101)
     .IsUnicode(false);



            entity.Property(e => e.UnitPrice).HasColumnType("numeric(19, 5)");

            OnConfigurePartial(entity);

            entity.Property(e => e.CreatedBy)
                .IsRequired()
                  .HasMaxLength(105);

            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

          


            entity.Property(e => e.StatusId).HasDefaultValueSql("((1))");



            entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 5)");

            entity.Property(e => e.UpdatedBy).HasMaxLength(105);

            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

          

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Product> entity);
    }
}
