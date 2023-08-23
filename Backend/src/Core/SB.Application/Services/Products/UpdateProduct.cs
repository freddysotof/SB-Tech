using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SB.Application.Services.Products
{
    public class UpdateProduct
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string? Barcode { get; set; }
        public string? PhotoUrl { get; set; }
        public string? Description { get; set; }
        public decimal UnitPrice { get; set; }
        public string? UpdatedBy { get; set; }
        public int? StatusId { get; set; }
    }
}
