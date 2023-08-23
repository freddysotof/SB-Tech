using SB.Application.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SB.Application.Services.Products
{
    public interface IProductService : IGenericService<GetProduct, CreateProduct, UpdateProduct>
    {
        Task<List<GetProduct>> FilterProductsAsync(string criteria, CancellationToken cancellationToken = default);
        Task<GetProduct> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
        Task<GetProduct> GetByBarcodeAsync(string barcode, CancellationToken cancellationToken = default);
    }
}
