using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SB.Application.Interfaces.Persistence.Orders;
using SB.Domain.Entities.Orders;
using SB.Persistence.Concretes.Repositories.Common;
using SB.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SB.Persistence.Concretes.Repositories.Orders
{
    public class OrderDetailRepository : BaseEntityRepository<OrderDetail, SBDbContext>, IOrderDetailRepository
    {
        private readonly DbSet<OrderDetail> _details;
        public OrderDetailRepository(IHttpContextAccessor httpContextAccessor, SBDbContext dbContext)
             : base(httpContextAccessor, dbContext)
        {
            _details = dbContext.Set<OrderDetail>();
        }

        public async Task<IEnumerable<OrderDetail>> GetByOrderId(int orderId, CancellationToken cancellationToken = default)
        {
            return await _details.Where(x => x.OrderHeaderId == orderId).ToListAsync(cancellationToken);
        }
    }
}
