using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SB.Application.Extensions;
using SB.Application.Interfaces.Persistence.Products;
using SB.Application.Services.Products.Projections;
using SB.Domain.Entities.Products;
using SB.Domain.Exceptions;

namespace SB.Application.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IHttpContextAccessor _contextAccessor;
        public ProductService(IProductRepository repository,IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _contextAccessor = httpContextAccessor;
        }
        public async Task<GetProduct> AddAsync(CreateProduct create, CancellationToken cancellationToken = default)
        {
            try
            {
                Product createdProduct = create;
                createdProduct.CreatedBy = _contextAccessor.GetCurrentUserName();
                createdProduct.Code = Guid.NewGuid().ToString();
                var product = await _repository.GetByCodeAsync(createdProduct.Code, cancellationToken);
                if (product != null)
                    throw new BadRequestException("Configuración de producto ya existe");
                await _repository.AddAsync(createdProduct, cancellationToken);
                return createdProduct;
            }
            catch (Exception e)
            {

                throw;
            }
           
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

        public async Task<List<GetProduct>> FilterProductsAsync(string criteria, CancellationToken cancellationToken = default)
        {
            var filteredResult = await _repository.FilterAsync(criteria, cancellationToken);
            return filteredResult.AsQueryable()
                    .Select(ProductProjection.GetAll)
                    .ToList();
        }

        public async Task<GetProduct> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
        {
            var product = await _repository.GetByCodeAsync(code, cancellationToken);
            return product == null
                ? throw new NotFoundException("No se encontraron datos de este producto")
                : product;
        }

        public async Task<GetProduct> GetByBarcodeAsync(string barcode, CancellationToken cancellationToken = default)
        {
            var product = await _repository.GetByBarcodeAsync(barcode, cancellationToken);
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
            product.UnitPrice = update.UnitPrice;
            product.UpdatedBy = _contextAccessor.GetCurrentUserName();
            product.Description = update.Description;
            product.Barcode= update.Barcode;
            product.Name = update.Name;
            product.PhotoUrl = update.PhotoUrl;
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
