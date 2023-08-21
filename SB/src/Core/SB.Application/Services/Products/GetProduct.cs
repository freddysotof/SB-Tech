using SB.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SB.Application.Services.Products
{
    public class GetProduct
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string? Description { get; set; }
        public decimal UnitPrice { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? StatusId { get; set; }

        public static implicit operator GetProduct(Product entity)
        {
            return new GetProduct
            {
                Id = entity.Id,
                Code = entity.Code,
                Description = entity.Description,
                CreatedDate = entity.CreatedDate,
                CreatedBy = entity.CreatedBy,
                StatusId = entity.StatusId,
                UnitPrice = entity.UnitPrice
            };
        }
    }
}
