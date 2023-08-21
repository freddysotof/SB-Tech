using SB.Application.Services.Orders.Detail;
using SB.Application.Services.Products;
using SB.Domain.Entities.Orders;
using SB.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SB.Application.Services.Orders.Projections
{
    public static class OrderProjection
    {
        public static Expression<Func<OrderHeader, GetOrder>> GetAll { get; } = element =>
           new GetOrder
           {
               Id = element.Id,
               UpdatedDate = element.UpdatedDate,
               StatusId = element.StatusId,
               CreatedDate = element.CreatedDate,
               CreatedBy = element.CreatedBy,
               UpdatedBy = element.UpdatedBy,
               CustomerAddressId = element.CustomerAddressId,
               CustomerCode = element.CustomerCode,
               CustomerEmail = element.CustomerEmail,
               CustomerName = element.CustomerName,
               CustomerPhone = element.CustomerPhone,
               DiscountAmount = element.DiscountAmount,
               DocDate = element.DocDate,
               NetAmount = element.NetAmount,
               Note = element.Note,
               OrderNumber = element.OrderNumber,
               TaxAmount = element.TaxAmount,
               TotalAmount = element.TotalAmount,
               Details = element.OrderDetails.Select(u => new GetOrderDetail
               {
                   Id = u.Id,
                   TotalAmount = u.TotalAmount,
                   TaxAmount = u.TaxAmount,
                   CreatedBy = u.CreatedBy,
                   CreatedDate = u.CreatedDate,
                   DiscountAmount = u.DiscountAmount,
                   DiscountPercentage = u.DiscountPercentage,
                   ItemDescription = u.ItemDescription,
                   ItemNumber = u.ItemNumber,
                   NetAmount = u.NetAmount,
                   Notes = u.Notes,
                   OrderHeaderId = u.OrderHeaderId,
                   Quantity = u.Quantity,
                   StatusId = u.StatusId,
                   UnitPrice = u.UnitPrice,
                   UpdatedBy = u.UpdatedBy,
                   UpdatedDate = u.UpdatedDate,
               }).ToList(),
           };

        public static Expression<Func<OrderDetail, GetOrderDetail>> GetDetails { get; } = element =>
           new GetOrderDetail
           {
             
                   Id =element.Id,
                   TotalAmount =element.TotalAmount,
                   TaxAmount =element.TaxAmount,
                   CreatedBy =element.CreatedBy,
                   CreatedDate =element.CreatedDate,
                   DiscountAmount =element.DiscountAmount,
                   DiscountPercentage =element.DiscountPercentage,
                   ItemDescription =element.ItemDescription,
                   ItemNumber =element.ItemNumber,
                   NetAmount =element.NetAmount,
                   Notes =element.Notes,
                   OrderHeaderId =element.OrderHeaderId,
                   Quantity =element.Quantity,
                   StatusId =element.StatusId,
                   UnitPrice =element.UnitPrice,
                   UpdatedBy =element.UpdatedBy,
                   UpdatedDate =element.UpdatedDate,
           };
    }
}
