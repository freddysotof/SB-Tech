﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable

using SB.Domain.Entities.Common;

namespace SB.Domain.Entities.Orders
{
    public partial class OrderDetail : BaseEntity
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
        public string Notes { get; set; }
       

        public virtual OrderHeader OrderHeader { get; set; }
    }
}