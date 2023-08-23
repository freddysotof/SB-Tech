using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SB.Domain.Entities.Auth;

namespace SB.Persistence.Context.Configurations
{
    public partial class AccessTokenConfiguration : IEntityTypeConfiguration<AccessToken>
    {
        public void Configure(EntityTypeBuilder<AccessToken> entity)
        {
            entity.ToTable("AccessToken");

            entity.Property(x => x.Id).HasColumnName("AccessTokenId");

            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.Property(e => e.Email)
                .HasMaxLength(101)
                .IsUnicode(false);



            OnConfigurePartial(entity);

            entity.Property(e => e.CreatedBy)
                .IsRequired()
       .HasMaxLength(105);

            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

          


            entity.Property(e => e.StatusId).HasDefaultValueSql("((1))");



            entity.Property(e => e.UpdatedBy).HasMaxLength(105);

            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

          

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<AccessToken> entity);
    }
}
