using SB.Application.Interfaces.Persistence.Common;
using SB.Domain.Entities.Orders;
using SB.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SB.Application.Interfaces.Persistence.Orders
{
    public interface IOrderHeaderRepository : IBaseEntityRepository<OrderHeader>
    {
        Task<OrderHeader> GetByOrderNumberAsync(string orderNumber, CancellationToken cancellationToken = default);
    }
}
