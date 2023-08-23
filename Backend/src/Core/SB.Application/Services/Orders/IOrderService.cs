using SB.Application.Services.Common;
using SB.Application.Services.Orders.Detail;
using SB.Application.Services.Orders.Header;

namespace SB.Application.Services.Orders
{
    public interface IOrderService : IGenericService<GetOrder, CreateOrder, UpdateOrder>
    {
        Task<List<GetOrderDetail>> GetDetailsByOrderId(int orderId, CancellationToken cancellationToken = default);
        Task<GetOrder> GetByOrderNumberAsync(string code, CancellationToken cancellationToken = default);
    }
}
