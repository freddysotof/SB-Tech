using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SB.Application.Services.Products;
using SB.Domain.Common.Models;
using SB.WebApi.Controllers.Base;
using SB.WebApi.Wrappers;

namespace SB.WebApi.Controllers
{
    public class ProductsController : ApiBaseAuthorizationController<ProductsController>
    {
        private readonly IProductService _productService;
        public ProductsController(IServiceProvider serviceProvider, IProductService productService)
        : base(serviceProvider)
        {
            _productService = productService;
        }


        /// <summary>
        /// Get Operation 
        /// </summary>
        /// <returns>Products Array</returns>
        [HttpGet()]
        [ProducesResponseType(typeof(PagedResponseWrapper<GetProduct>), StatusCodes.Status200OK, "application/json")]
        public async Task<IActionResult> GetProductsAsync([FromQuery] PaginationOption paginationOption, string? criteria)
        {

            var product = await _productService.FilterProductsAsync(criteria);
            return Ok(product);
        }

        /// <summary>
        /// Get Operation 
        /// </summary>
        /// <returns>Products Array</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(PagedResponseWrapper<GetProduct>), StatusCodes.Status200OK, "application/json")]
        public async Task<IActionResult> GetProductAsync(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            return Ok(product);
        }

        /// <summary>
        /// Get Operation 
        /// </summary>
        /// <returns>Products Array</returns>
        [HttpGet("by-code/{code}")]
        [ProducesResponseType(typeof(PagedResponseWrapper<GetProduct>), StatusCodes.Status200OK, "application/json")]
        public async Task<IActionResult> GetProductAsync(string code)
        {
            var product = await _productService.GetByCodeAsync(code);
            return Ok(product);
        }


        /// <summary>
        /// Get Operation 
        /// </summary>
        /// <returns>Products Array</returns>
        [HttpGet("by-barcode/{barcode}")]
        [ProducesResponseType(typeof(PagedResponseWrapper<GetProduct>), StatusCodes.Status200OK, "application/json")]
        public async Task<IActionResult> GetProductByBarcodeAsync(string barcode)
        {
            var product = await _productService.GetByBarcodeAsync(barcode);
            return Ok(product);
        }


        /// <summary>
        /// Get Operation 
        /// </summary>
        /// <returns>Products Array</returns>
        [HttpPost()]
        [ProducesResponseType(typeof(PagedResponseWrapper<GetProduct>), StatusCodes.Status200OK, "application/json")]
        public async Task<IActionResult> AddProductAsync( [FromBody] PetitionHandler<CreateProduct> petition)
        {
            //petition.Data.Code = code;
            var createdProduct = await _productService.AddAsync(petition.Data);
            return Ok(createdProduct);
        }

        /// <summary>
        /// Get Operation 
        /// </summary>
        /// <returns>Products Array</returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(PagedResponseWrapper<GetProduct>), StatusCodes.Status200OK, "application/json")]
        public async Task<IActionResult> UpdateProductAsync(int id, [FromBody] PetitionHandler<UpdateProduct> petition)
        {
            var createdProduct = await _productService.UpdateAsync(id, petition.Data);
            return Ok(createdProduct);
        }

        /// <summary>
        /// Get Operation 
        /// </summary>
        /// <returns>Products Array</returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(PagedResponseWrapper<GetProduct>), StatusCodes.Status200OK, "application/json")]
        public async Task<IActionResult> DeleteProductAsync(int id)
        {
            var createdProduct = await _productService.DeleteAsync(id);
            return Ok(createdProduct);
        }
    }
}
