using App.Api.Contracts.Product.Requests;
using App.Api.Filters;
using App.Api.Options;
using App.Domain.Entities;
using App.Domain.Enum;
using App.Domain.Interfaces.IRepositories;
using App.Domain.Interfaces.IServices;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace App.Api.Controllers
{
    /// <summary>
    /// Information of ProductsController
    /// CreatedBy: ThiepTT(27/02/2023)
    /// </summary>
    [Route(ApiRouter.ROUTER)]
    [ApiController]
    [AppExceptionAttibute]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductsController : BaseController
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Products Controller
        /// </summary>
        /// <param name="productRepository">productRepository</param>
        /// <param name="productService">productService</param>
        /// <param name="mapper">mapper</param>
        public ProductsController(IProductRepository productRepository, IProductService productService, IMapper mapper)
        {
            _productRepository = productRepository;
            _productService = productService;
            _mapper = mapper;
        }

         /// <summary>
         /// Get all product
         /// </summary>
         /// <returns>IActionResult</returns>
         /// CreatedBy: ThiepTT(27/02/2023)
         [HttpGet]
         public async Task<IActionResult> GetAllProduct()
         {
             var result = await _productRepository.GetAll();

             return (result.IsError) ? HandlerErrorResponse(result.ErrorData) : Ok(result);
         }

        /// <summary>
        /// Get all paging product
        /// </summary>
        /// <param name="valueFilter">ValueFilter</param>
        /// <param name="trendingEnum">TrendingEnum</param>
        /// <param name="pageNumber">PageNumber</param>
        /// <param name="pageSize">PageSize</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: ThiepTT(27/02/2023)
        [HttpGet]
        [Route(ApiRouter.Product.GETALLPAGINGPRODUCT)]
        public async Task<IActionResult> GetAllPagingProduct(string? valueFilter, TrendingEnum? trendingEnum, int pageNumber,
            int pageSize = 15)
        {
            var result = await _productRepository.GetAllPaging(valueFilter, trendingEnum, pageNumber, pageSize);

            return (result.IsError) ? HandlerErrorResponse(result.ErrorData) : Ok(result);
        }

        /// <summary>
        /// Get product by id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: ThiepTT(27/02/2023)
        [HttpGet]
        [Route(ApiRouter.ID)]
        public async Task<IActionResult> GetProductById([Required] int id)
        {
            var result = await _productRepository.GetById(id);

            return (result.IsError) ? HandlerErrorResponse(result.ErrorData) : Ok(result);
        }

        /// <summary>
        /// Create product
        /// </summary>
        /// <param name="productCreate">ProductCreate</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: ThiepTT(27/02/2023)
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromForm] ProductCreate productCreate)
        {
            var product = _mapper.Map<Product>(productCreate);

            if (productCreate.File != null && productCreate.File.Length > 0)
            {
                product.Upload = await ConvertFormFileToString(productCreate.File);
            }

            var result = await _productService.Create(product);

            return (result.IsError) ? HandlerErrorResponse(result.ErrorData) : Ok(result);
        }

        /// <summary>
        /// Update product
        /// </summary>
        /// <param name="productUpdate">ProductUpdate</param>
        /// <param name="id">id</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: ThiepTT(27/02/2023)
        [HttpPut]
        [Route(ApiRouter.ID)]
        public async Task<IActionResult> UpdateProduct([FromForm] ProductUpdate productUpdate, [Required] int id)
        {
            var product = _mapper.Map<Product>(productUpdate);

            if (productUpdate.File != null && productUpdate.File.Length > 0)
            {
                product.Upload = await ConvertFormFileToString(productUpdate.File);
            }

            var result = await _productService.Update(product, id);

            return (result.IsError) ? HandlerErrorResponse(result.ErrorData) : Ok(result);
        }

        /// <summary>
        /// Delete product
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: ThiepTT(27/02/2023)
        [HttpDelete]
        [Route(ApiRouter.ID)]
        public async Task<IActionResult> DeleteProduct([Required] int id)
        {
            var result = await _productService.Delete(id);

            return (result.IsError) ? HandlerErrorResponse(result.ErrorData) : Ok(result);
        }
    }
}