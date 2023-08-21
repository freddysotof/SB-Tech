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
        Task<GetProduct> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
    }
}
