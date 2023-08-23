using SB.Application.Interfaces.Persistence.Common;
using SB.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SB.Application.Interfaces.Persistence.Orders
{
    public interface IOrderDetailRepository : IBaseEntityRepository<OrderDetail>
    {
        Task<IEnumerable<OrderDetail>> GetByOrderId(int orderId, CancellationToken cancellationToken = default);
    }
}
