using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SB.Application.Interfaces.Persistence.Products;
using SB.Domain.Entities.Orders;
using SB.Domain.Entities.Products;
using SB.Persistence.Concretes.Repositories.Common;
using SB.Persistence.Context;

namespace SB.Persistence.Concretes.Repositories.Products
{
    public class ProductRepository : BaseEntityRepository<Product, SBDbContext>, IProductRepository
    {
        private readonly DbSet<Product> _products;
        public ProductRepository(IHttpContextAccessor httpContextAccessor, SBDbContext dbContext)
             : base(httpContextAccessor, dbContext)
        {
            _products = dbContext.Set<Product>();
        }

        public async Task<IEnumerable<Product>> FilterAsync(string criteria, CancellationToken cancellationToken = default)
        {
            if(string.IsNullOrEmpty(criteria) || criteria=="null")
                return  await _products.AsQueryable().ToListAsync();
            return await _products.Where(x => x.Code.Contains(criteria) | x.Description.Contains(criteria) | x.Name.Contains(criteria) | x.Barcode.Contains(criteria)).ToListAsync();
        }

        public async Task<Product> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
        {
            return await _products.FirstOrDefaultAsync(x => x.Code.ToLower() == code.ToLower());
        }

        public async Task<Product> GetByBarcodeAsync(string barcode, CancellationToken cancellationToken = default)
        {
            return await _products.FirstOrDefaultAsync(x => x.Barcode.ToLower() == barcode.ToLower());
        }


        //public async Task<GetGPProductByCodeResult> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
        //{
        //    var res = await _procedures.GetGPProductByCodeAsync(code,null,cancellationToken);
        //    return res.FirstOrDefault();
        //}



        // public async Task<IEnumerable<Product>> FilterAsync(string criteria, CancellationToken cancellationToken = default)
        //=> await _procedures.FilterGPProductAsync(criteria, null, cancellationToken); 

    }
}
