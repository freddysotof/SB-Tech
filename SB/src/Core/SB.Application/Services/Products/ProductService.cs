using Microsoft.EntityFrameworkCore;
using SB.Application.Interfaces.Persistence.Products;
using SB.Application.Services.Products.Projections;
using SB.Domain.Entities.Products;
using SB.Domain.Exceptions;

namespace SB.Application.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }
        public async Task<GetProduct> AddAsync(CreateProduct create, CancellationToken cancellationToken = default)
        {
            Product createdProduct = create;
            createdProduct.CreatedBy = create.CreatedBy;
            var product = await _repository.GetByCodeAsync(create.Code, cancellationToken);
            if (product != null)
                throw new BadRequestException("Configuración de producto ya existe");
            await _repository.AddAsync(createdProduct, cancellationToken);
            return createdProduct;
        }

        public Task<bool> DeleteAsync(string id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            _ = await _repository.GetByIdAsync(id, cancellationToken) ?? throw new NotFoundException("No se encontró el registro de este producto");
            return await _repository.DeleteAsync(id, cancellationToken);
        }

        public async Task<List<GetProduct>> GetAllAsync(CancellationToken cancellationToken = default)
            => await _repository.Queryable(cancellationToken).AsQueryable()
                .Select(ProductProjection.GetAll)
                .ToListAsync(cancellationToken);

        public async Task<GetProduct> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
        {
            var product = await _repository.GetByCodeAsync(code, cancellationToken);
            return product == null
                ? throw new NotFoundException("No se encontraron datos de este producto")
                : product;
        }

        public Task<GetProduct> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<GetProduct> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var product = await _repository.GetByIdAsync(id, cancellationToken) ?? throw new NotFoundException("No se encontró el registro de este producto");
            return product;
        }

        public async Task<GetProduct> UpdateAsync(int id, UpdateProduct update, CancellationToken cancellationToken = default)
        {
            var product = await _repository.GetByIdAsync(id,cancellationToken) ?? throw new NotFoundException("No se encontró el registro de este producto");
            product.Code = update.Code;
            product.UnitPrice = update.UnitPrice;
            product.UpdatedBy = update.UpdatedBy;
            product.Description = update.Description;
            product.StatusId = update.StatusId;

            await _repository.UpdateAsync(product, cancellationToken);
            return product;
        }

        public Task<GetProduct> UpdateAsync(string id, UpdateProduct update, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
