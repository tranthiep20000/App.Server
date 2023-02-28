using App.Application.Services.Commom;
using App.Domain.Entities;
using App.Domain.Entities.Results;
using App.Domain.Interfaces.IRepositories;
using App.Domain.Interfaces.IServices;

namespace App.Application.Services
{
    /// <summary>
    /// Information of ProductService
    /// CreatedBy: Thiep(27/02/2023)
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IShopRepository _shopRepository;

        public ProductService(IProductRepository productRepository, IShopRepository shopRepository)
        {
            _productRepository = productRepository;
            _shopRepository = shopRepository;
        }

        /// <summary>
        /// Create a record
        /// </summary>
        /// <param name="product">Product</param>
        /// <returns>Number record create success</returns>
        /// CreatedBy: Thiep(27/02/2023)
        public async Task<OperationResult<int>> Create(Product product)
        {
            var result = new OperationResult<int>();

            // 1. productName is null
            if (string.IsNullOrWhiteSpace(product.ProductName))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.ProductByNameNotEmpty);

                return result;
            }

            // 2. check length productname
            if (product.ProductName.Length < ConfigErrorMessageService.LengthMinCharacterOfProductName
                || product.ProductName.Length > ConfigErrorMessageService.LengthMaxCharacterOfProductName)
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.ProductByCharacter);

                return result;
            }

            // 3. slug is null
            if (string.IsNullOrWhiteSpace(product.Slug))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.ProductBySlugNotEmpty);

                return result;
            }

            // 4. check length slug
            if (product.Slug.Length < ConfigErrorMessageService.LengthMinCharacterOfSlug
                || product.Slug.Length > ConfigErrorMessageService.LengthMaxCharacterOfSlug)
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.ProductBySlugCharacter);

                return result;
            }

            // get shopById
            var shopById = await _shopRepository.GetById(product.ShopId);

            // check shopByid is null
            if (shopById.Data is null)
            {
                result.AddError(ErrorCode.NotFound, string.Format(ConfigErrorMessageService.ProductByShopNotFound, product.ShopId));

                return result;
            }    

            // 5. shopId is null
            if (string.IsNullOrEmpty(product.ShopId.ToString()))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.ProductByShopNotEmpty);

                return result;
            }

            // 6. price is null
            if (string.IsNullOrEmpty(product.Price.ToString()))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.ProductByPriceNotEmpty);

                return result;
            }

            // 7. upload is null
            if (product.Upload is null)
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.ProductByUploadNotEmpty);

                return result;
            }

            try
            {
                result = await _productRepository.Create(product);
            }
            catch (Exception ex)
            {
                result.AddError(ErrorCode.ServerError, $"{ex.Message}");
            }

            return result;
        }

        /// <summary>
        /// Delete a record
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Number record delete success</returns>
        /// CreatedBy: Thiep(27/02/2023)
        public async Task<OperationResult<int>> Delete(int id)
        {
            var result = new OperationResult<int>();

            try
            {
                result = await _productRepository.Delete(id);
            }
            catch (Exception ex)
            {
                result.AddError(ErrorCode.ServerError, $"{ex.Message}");
            }

            return result;
        }

        /// <summary>
        /// Update a record
        /// </summary>
        /// <param name="product">Product</param>
        /// <param name="id">Id</param>
        /// <returns>Number record update success</returns>
        /// CreatedBy: Thiep(27/02/2023)
        public async Task<OperationResult<int>> Update(Product product, int id)
        {
            var result = new OperationResult<int>();

            // 1. productName is null
            if (string.IsNullOrWhiteSpace(product.ProductName))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.ProductByNameNotEmpty);

                return result;
            }

            // 2. check length productname
            if (product.ProductName.Length < ConfigErrorMessageService.LengthMinCharacterOfProductName
                || product.ProductName.Length > ConfigErrorMessageService.LengthMaxCharacterOfProductName)
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.ProductByCharacter);

                return result;
            }

            // 3. slug is null
            if (string.IsNullOrWhiteSpace(product.Slug))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.ProductBySlugNotEmpty);

                return result;
            }

            // 4. check length slug
            if (product.Slug.Length < ConfigErrorMessageService.LengthMinCharacterOfSlug
                || product.Slug.Length > ConfigErrorMessageService.LengthMaxCharacterOfSlug)
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.ProductBySlugCharacter);

                return result;
            }

            // 5. shopId is null
            if (string.IsNullOrEmpty(product.ShopId.ToString()))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.ProductByShopNotEmpty);

                return result;
            }

            // 6. price is null
            if (string.IsNullOrEmpty(product.Price.ToString()))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.ProductByPriceNotEmpty);

                return result;
            }

            // 7. upload is null
            if (product.Upload is null)
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.ProductByUploadNotEmpty);

                return result;
            }

            try
            {
                result = await _productRepository.Update(product, id);
            }
            catch (Exception ex)
            {
                result.AddError(ErrorCode.ServerError, $"{ex.Message}");
            }

            return result;
        }
    }
}