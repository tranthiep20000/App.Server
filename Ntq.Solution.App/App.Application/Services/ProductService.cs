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
    public class ProductService : BaseService<Product, int>, IProductService
    {
        private readonly IShopRepository _shopRepository;

        public ProductService(IProductRepository productRepository, IShopRepository shopRepository) : base(productRepository)
        {
            _shopRepository = shopRepository;
        }

        /// <summary>
        /// Validate
        /// </summary>
        /// <param name="product">Product</param>
        /// <param name="id">Id</param>
        /// <returns>int</returns>
        /// CreatedBy: ThiepTT(07/03/2023)
        protected override async Task<OperationResult<int>> Validate(Product product, int id)
        {
            var result = new OperationResult<int>();

            // 1. productName is null
            if (string.IsNullOrWhiteSpace(product.ProductName))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.PRODUCTBYNAMENOTEMPTY);

                return result;
            }

            // 2. check length productname
            if (product.ProductName.Length < ConfigErrorMessageService.LENGTHMINCHARACTEROFPRODUCTNAME
                || product.ProductName.Length > ConfigErrorMessageService.LENGTHMAXCHARACTEROFPRODUCTNAME)
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.PRODUCTBYCHARACTER);

                return result;
            }

            // 3. slug is null
            if (string.IsNullOrWhiteSpace(product.Slug))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.PRODUCTBYSLUGNOTEMPTY);

                return result;
            }

            // 4. check length slug
            if (product.Slug.Length < ConfigErrorMessageService.LENGTHMINCHARACTEROFSLUG
                || product.Slug.Length > ConfigErrorMessageService.LENGTHMAXCHARACTEROFSLUG)
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.PRODUCTBYSLUGCHARACTER);

                return result;
            }

            // get shopById
            var shopById = await _shopRepository.GetById(product.ShopId);

            // 5. check shopByid is null
            if (shopById.Data is null)
            {
                result.AddError(ErrorCode.NotFound, string.Format(ConfigErrorMessageService.PRODUCTBYSHOPNOTFOUND, product.ShopId));

                return result;
            }

            // 6. shopId is null
            if (string.IsNullOrEmpty(product.ShopId.ToString()))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.PRODUCTBYSHOPNOTEMPTY);

                return result;
            }

            // 7. price is null
            if (string.IsNullOrEmpty(product.Price.ToString()))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.PRODUCTBYPRICENOTEMPTY);

                return result;
            }

            // 8. upload is null
            if (string.IsNullOrWhiteSpace(product.Upload))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.PRODUCTBYUPLOADNOTEMPTY);

                return result;
            }

            return result;
        }
    }
}