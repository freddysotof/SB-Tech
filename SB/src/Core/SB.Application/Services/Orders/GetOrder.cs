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
    public class GetOrder:GetOrderHeader
    {
        public List<GetOrderDetail>? Details { get; set; }
        public static implicit operator GetOrder(OrderHeader entity)
        {
            return new GetOrder
            {
                Id=entity.Id,
                CreatedDate=entity.CreatedDate,
                StatusId=entity.StatusId,
                UpdatedBy=entity.UpdatedBy,
                UpdatedDate=entity.UpdatedDate,
                CreatedBy = entity.CreatedBy,
                CustomerCode = entity.CustomerCode,
                CustomerEmail = entity.CustomerEmail,
                CustomerName = entity.CustomerName,
                CustomerPhone = entity.CustomerPhone,
                DiscountAmount = entity.DiscountAmount,
                DocDate = entity.DocDate,
                NetAmount = entity.NetAmount,
                Note = entity.Note,
                TaxAmount = entity.TaxAmount,
                OrderNumber = entity.OrderNumber,
                TotalAmount = entity.TotalAmount,
                CustomerAddressId = entity.CustomerAddressId,
                Details = entity.OrderDetails?.Select(u => new GetOrderDetail
                {
                    Id = u.Id,
                    CreatedDate = u.CreatedDate,
                    StatusId = u.StatusId,
                    UnitPrice = u.UnitPrice,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedDate= u.UpdatedDate,
                    TotalAmount = u.TotalAmount,
                    TaxAmount = u.TaxAmount,
                    CreatedBy = u.CreatedBy,
                    DiscountAmount = u.DiscountAmount,
                    DiscountPercentage = u.DiscountPercentage,
                    ItemDescription = u.ItemDescription,
                    ItemNumber = u.ItemNumber,
                    NetAmount = u.NetAmount,
                    Notes = u.Notes,
                    OrderHeaderId = u.OrderHeaderId,
                    Quantity = u.Quantity,
                }).ToList(),
            };
        }
    }
}
