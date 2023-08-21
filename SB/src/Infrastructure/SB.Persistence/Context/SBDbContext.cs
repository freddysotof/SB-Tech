using Microsoft.EntityFrameworkCore;
using SB.Domain.Entities.Common;
using SB.Domain.Entities.Orders;
using SB.Domain.Entities.Products;

namespace SB.Persistence.Context
{
    public class SBDbContext : DbContext
    {
        public SBDbContext()
        {
        }
        public SBDbContext(DbContextOptions options) : base(options)
        {

        }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<OrderHeader> OrderHeaders { get; set; }
        public virtual DbSet<Product> Products{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new Configurations.OrderDetailConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.OrderHeaderConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.ProductConfiguration());

            //OnModelCreatingPartial(modelBuilder);
        }

        //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
        // Entityleri db ye kaydederken içerdiði dateTime propertylerini otomatik olarak ekleyen bir metod
        // Not : Eðer PostgreSql kullanacaksanýz DateTime yerine DateTime.Utc.Now Kullanmanýz gerekiyor

        // A method that automatically adds dateTime properties contained in entities while saving to the database
        // Note: If you're using PostgreSql, you need to use DateTime.Utc.Now instead of DateTime
        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var datas = ChangeTracker.Entries<IEntity>();
            foreach (var data in datas)
            {
                _ = data.State switch
                {
                    EntityState.Added => data.Entity.CreatedDate = DateTime.Now,
                    EntityState.Modified => data.Entity.UpdatedDate = DateTime.Now,
                    _ => DateTime.Now
                };
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
