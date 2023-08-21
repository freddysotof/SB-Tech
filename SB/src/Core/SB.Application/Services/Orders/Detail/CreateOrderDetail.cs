using SB.Application.Services.Orders.Header;
using SB.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SB.Application.Services.Orders.Detail
{
    public class CreateOrderDetail
    {
        public int OrderHeaderId { get; set; }
        public string ItemNumber { get; set; }
        public string ItemDescription { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal NetAmount { get; set; }
        public string? Notes { get; set; }
        public string CreatedBy { get; set; }
        public static implicit operator OrderDetail(CreateOrderDetail create)
        {
            return new OrderDetail
            {
                TotalAmount = create.TotalAmount,
                TaxAmount = create.TaxAmount,
                NetAmount = create.NetAmount,
                CreatedBy = create.CreatedBy,
                DiscountAmount = create.DiscountAmount,
                DiscountPercentage = create.DiscountPercentage,
                ItemDescription = create.ItemDescription,
                ItemNumber = create.ItemNumber,
                Notes = create.Notes,
                OrderHeaderId = create.OrderHeaderId,
                Quantity = create.Quantity,
                UnitPrice = create.UnitPrice,

            };
        }
    }
}
