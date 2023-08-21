using SB.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SB.Domain.Entities.Products
{
    public class Product:BaseEntity
    {
        public string? Code { get; set; }
        public string? Description { get; set; }
        //public string? Segment { get; set; }
        //public string? PriceLevel { get; set; }
        public decimal UnitPrice { get; set; }
        //public string? TaxPlan { get; set; }
        //public string? ProductType { get; set; }
        //public int TaxPercentage { get; set; }
    }
}
