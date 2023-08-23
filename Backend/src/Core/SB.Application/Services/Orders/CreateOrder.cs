using SB.Application.Services.Orders.Detail;
using SB.Application.Services.Orders.Header;
using SB.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SB.Application.Services.Orders
{
    public class CreateOrder:CreateOrderHeader
    {
        public List<CreateOrderDetail> Details { get; set; }
        public static implicit operator OrderHeader(CreateOrder create)
        {
            return new OrderHeader
            {
                CreatedBy=create.CreatedBy,
                CustomerCode = create.CustomerCode,
                CustomerEmail = create.CustomerEmail,
                CustomerName = create.CustomerName,
                CustomerPhone = create.CustomerPhone,
                DiscountAmount = create.DiscountAmount,
                DocDate = create.DocDate,
                NetAmount = create.NetAmount,
                Note = create.Note,
                TaxAmount = create.TaxAmount,
                TotalAmount = create.TotalAmount,
                CustomerAddressId = create.CustomerAddressId,
                Details = create.Details.Select(u => new OrderDetail
                {
                    TotalAmount = u.TotalAmount,
                    TaxAmount = u.TaxAmount,
                    DiscountAmount = u.DiscountAmount,
                    DiscountPercentage = u.DiscountPercentage,
                    ItemDescription = u.ItemDescription,
                    ItemNumber = u.ItemNumber,
                    NetAmount = u.NetAmount,
                    Notes = u.Notes,
                    Quantity = u.Quantity,
                }).ToList(),
            };
        }

    }
}
