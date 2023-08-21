using SB.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SB.Application.Services.Products.Projections
{
    public static class ProductProjection
    {
        public static Expression<Func<Product, GetProduct>> GetAll { get; } = element =>
           new GetProduct
           {
               Id = element.Id,
               UpdatedDate = element.UpdatedDate,
               StatusId = element.StatusId,
               CreatedDate = element.CreatedDate,
               CreatedBy = element.CreatedBy,
               UpdatedBy = element.UpdatedBy,
               UnitPrice = element.UnitPrice,
               Code = element.Code,
               Description = element.Description
           };
    }
}
