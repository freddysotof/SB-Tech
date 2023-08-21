using SB.Application.Services.Orders.Detail;
using SB.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SB.Application.Services.Orders.Header
{
    public class CreateOrderHeader
    {
        public string OrderNumber { get; set; }

        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerAddressId { get; set; }
        public string? Note { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal NetAmount { get; set; }
        public DateTime? DocDate { get; set; }
        public string CreatedBy { get; set; }

     
    }
}
