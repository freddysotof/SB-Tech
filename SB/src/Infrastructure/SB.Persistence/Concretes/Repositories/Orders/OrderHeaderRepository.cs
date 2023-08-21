using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SB.Application.Interfaces.Persistence.Orders;
using SB.Domain.Entities.Orders;
using SB.Persistence.Concretes.Repositories.Common;
using SB.Persistence.Context;

namespace SB.Persistence.Concretes.Repositories.Orders
{
    public class OrderHeaderRepository : BaseEntityRepository<OrderHeader, SBDbContext>, IOrderHeaderRepository
    {
        private readonly DbSet<OrderHeader> _orders;
        public OrderHeaderRepository(IHttpContextAccessor httpContextAccessor, SBDbContext dbContext)
             : base(httpContextAccessor, dbContext)
        {
            _orders = dbContext.Set<OrderHeader>();
        }

        public async Task<OrderHeader> GetByOrderNumberAsync(string orderNumber, CancellationToken cancellationToken = default)
        {
            return await _orders.FirstOrDefaultAsync(x => x.OrderNumber == orderNumber);
        }
    }
}
