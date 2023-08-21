using SB.Application.Interfaces.Persistence.Orders;
using SB.Application.Interfaces.Persistence.Orders;
using SB.Application.Services.Orders.Detail;
using SB.Application.Services.Orders.Projections;
using SB.Application.Services.Orders;
using SB.Domain.Entities.Orders;
using SB.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SB.Application.Services.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IOrderHeaderRepository _repository;
        private readonly IOrderDetailRepository _detailRepository;
        public OrderService(IOrderHeaderRepository repository,IOrderDetailRepository detailRepository)
        {
            _repository = repository;
            _detailRepository = detailRepository;
        }
        public async Task<GetOrder> AddAsync(CreateOrder create, CancellationToken cancellationToken = default)
        {
            OrderHeader createdOrder = create;
            createdOrder.CreatedBy = create.CreatedBy;
            var order = await _repository.GetByOrderNumberAsync(create.OrderNumber, cancellationToken);
            if (order != null)
                throw new BadRequestException("Configuración de orden ya existe");
            await _repository.AddAsync(createdOrder, cancellationToken);
            return createdOrder;
        }

        public Task<bool> DeleteAsync(string id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            _ = await _repository.GetByIdAsync(id, cancellationToken) ?? throw new NotFoundException("No se encontró el registro de este orden");
            return await _repository.DeleteAsync(id, cancellationToken);
        }

        public async Task<List<GetOrder>> GetAllAsync(CancellationToken cancellationToken = default)
            => await _repository.Queryable(cancellationToken).AsQueryable()
                .Select(OrderProjection.GetAll)
                .ToListAsync(cancellationToken);

        public async Task<GetOrder> GetByOrderNumberAsync(string code, CancellationToken cancellationToken = default)
        {
            var order = await _repository.GetByOrderNumberAsync(code, cancellationToken);
            return order == null
                ? throw new NotFoundException("No se encontraron datos de este orden")
                : order;
        }

        public Task<GetOrder> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<GetOrder> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var order = await _repository.GetByIdAsync(id, cancellationToken) ?? throw new NotFoundException("No se encontró el registro de este orden");
            //var order = await _repository.Queryable(cancellationToken)
            //.AsNoTracking()
            //.FirstOrDefaultAsync(x=>x.Id==id,cancellationToken) ?? throw new NotFoundException("No se encontró el registro de este orden");
            return order;
        }

        public async Task<GetOrder> UpdateAsync(int id, UpdateOrder update, CancellationToken cancellationToken = default)
        {
            var order = await _repository.GetByIdAsync(id, cancellationToken) ?? throw new NotFoundException("No se encontró el registro de este orden");
            order.Note= update.Note;

            await _repository.UpdateAsync(order, cancellationToken);
            return order;
        }

        public Task<GetOrder> UpdateAsync(string id, UpdateOrder update, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<List<GetOrderDetail>> GetDetailsByOrderId(int orderId, CancellationToken cancellationToken = default)
         => await _detailRepository.Queryable(cancellationToken).Where(x=>x.OrderHeaderId==orderId).AsQueryable()
                .Select(OrderProjection.GetDetails)
                .ToListAsync(cancellationToken);
    }
}
