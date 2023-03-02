using App.Application.Services.Commom;
using App.Domain.Entities;
using App.Domain.Entities.Results;
using App.Domain.Interfaces.IRepositories;
using App.Domain.Interfaces.IServices;

namespace App.Application.Services
{
    /// <summary>
    /// Information of ShopService
    /// CreatedBy: ThiepTT(27/02/2023)
    /// </summary>
    public class ShopService : BaseService<Shop>, IShopService
    {
        private readonly IShopRepository _shopRepository;

        public ShopService(IShopRepository shopRepository) : base(shopRepository)
        {
            _shopRepository = shopRepository;
        }

        /// <summary>
        /// Create a record
        /// </summary>
        /// <param name="shop">Shop</param>
        /// <returns>Number record create success</returns>
        /// CreatedBy: ThiepTT(27/02/2022)
        public async Task<OperationResult<int>> Create(Shop shop)
        {
            var result = new OperationResult<int>();

            // 1. shopName is null
            if (string.IsNullOrWhiteSpace(shop.ShopName))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.SHOPBYNAMENOTEMPTY);

                return result;
            }

            try
            {
                result = await _shopRepository.Create(shop);
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
        /// <param name="shop">Shop</param>
        /// <param name="id">Id</param>
        /// <returns>Number record update success</returns>
        /// CreatedBy: ThiepTT(27/02/2022)
        public async Task<OperationResult<int>> Update(Shop shop, int id)
        {
            var result = new OperationResult<int>();

            // 1. shopName is null
            if (string.IsNullOrWhiteSpace(shop.ShopName))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.SHOPBYNAMENOTEMPTY);

                return result;
            }

            try
            {
                result = await _shopRepository.Update(shop, id);
            }
            catch (Exception ex)
            {
                result.AddError(ErrorCode.ServerError, $"{ex.Message}");
            }

            return result;
        }
    }
}