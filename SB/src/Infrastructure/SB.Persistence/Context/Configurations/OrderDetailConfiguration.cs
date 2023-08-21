﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SB.Domain.Entities.Common;
using SB.Domain.Entities.Orders;

namespace SB.Persistence.Context.Configurations
{
    public partial class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> entity)
        {
            entity.ToTable("OrderDetail");

            entity.Property(x=>x.Id).HasColumnName("OrderDetailId");

            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.Property(e => e.CreatedBy)
                .IsRequired()
                .HasMaxLength(25);

            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            entity.Property(e => e.DiscountAmount).HasColumnType("decimal(18, 5)");

            entity.Property(e => e.DiscountPercentage).HasColumnType("decimal(18, 2)");

            entity.Property(e => e.ItemDescription)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(e => e.ItemNumber)
                .IsRequired()
                .HasMaxLength(25);

            entity.Property(e => e.NetAmount).HasColumnType("decimal(18, 5)");

            entity.Property(e => e.Notes).HasMaxLength(500);



            entity.Property(e => e.StatusId).HasDefaultValueSql("((1))");

            entity.Property(e => e.TaxAmount).HasColumnType("decimal(18, 5)");

            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 5)");

     

            entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 5)");

            entity.Property(e => e.UpdatedBy).HasMaxLength(25);

            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.OrderHeader)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderHeaderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderDetail_OrderHeader");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<OrderDetail> entity);
    }
}
