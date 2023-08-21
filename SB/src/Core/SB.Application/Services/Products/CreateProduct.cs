using SB.Application.Services.Orders.Detail;
using SB.Domain.Entities.Orders;
using SB.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SB.Application.Services.Products
{
    public class CreateProduct
    {
        public string Code { get; set; }
        public string? Description { get; set; }
        public decimal UnitPrice { get; set; }
        public string CreatedBy { get; set; }

        public static implicit operator Product(CreateProduct create)
        {
            return new Product
            {
                Code = create.Code,
                Description = create.Description,
                UnitPrice = create.UnitPrice,
                CreatedBy = create.CreatedBy,
            };
        }
    }
}
