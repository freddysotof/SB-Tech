using SB.Application.Interfaces.Persistence.Common;
using SB.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SB.Application.Interfaces.Persistence.Products
{
    public interface IProductRepository:IBaseEntityRepository<Product>
    {
        Task<IEnumerable<Product>> FilterAsync(string criteria, CancellationToken cancellationToken = default);
        Task<Product> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
        Task<Product> GetByBarcodeAsync(string barcode, CancellationToken cancellationToken = default);
    }
}
