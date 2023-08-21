using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SB.Application.Services.Orders.Detail;
using SB.Application.Services.Orders;
using SB.WebApi.Controllers.Base;
using SB.WebApi.Wrappers;
using SB.Domain.Common.Models;

namespace SB.WebApi.Controllers
{
    public class OrdersController : ApiBaseController<OrdersController>
    {
        private readonly IOrderService _orderService;
        public OrdersController(IServiceProvider serviceProvider, IOrderService orderService)
        : base(serviceProvider)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Get Operation 
        /// </summary>
        /// <returns>Customers Array</returns>
        [HttpGet("{id:int}", Name = "GetOrderById")]
        [ProducesResponseType(typeof(PagedResponseWrapper<GetOrder>), StatusCodes.Status200OK, "application/json")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var order = await _orderService.GetByIdAsync(id);
            return Ok(order);
        }


        /// <summary>
        /// Get Operation 
        /// </summary>
        /// <returns>Customers Array</returns>
        [HttpGet("{orderId:int}/details")]
        [ProducesResponseType(typeof(PagedResponseWrapper<GetOrderDetail>), StatusCodes.Status200OK, "application/json")]
        public async Task<IActionResult> GetOrderDetail(int orderId)
        {
            var order = await _orderService.GetDetailsByOrderId(orderId);
            return Ok(order);
        }

        /// <summary>
        /// Post Operation 
        /// </summary>
        /// <returns>Customers Array</returns>
        [HttpPost]
        [ProducesResponseType(typeof(PagedResponseWrapper<GetOrder>), StatusCodes.Status200OK, "application/json")]
        public async Task<IActionResult> CreateOrder([FromBody] PetitionHandler<CreateOrder> petition)
        {
            var createdOrder = await _orderService.AddAsync(petition.Data);
            return new CreatedAtRouteResult(
                          "GetOrderById",
                          new { id = createdOrder.Id },
                          createdOrder
                      );
        }


    }

}
