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
    public class ShopService : BaseService<Shop, int>, IShopService
    {
        public ShopService(IShopRepository shopRepository) : base(shopRepository)
        {
        }

        /// <summary>
        /// Validate
        /// </summary>
        /// <param name="shop">Shop</param>
        /// <param name="id">Id</param>
        /// <returns>int</returns>
        /// CreatedBy: ThiepTT(07/03/2023)
        protected override async Task<OperationResult<int>> Validate(Shop shop, int id)
        {
            var result = new OperationResult<int>();

            // 1. shopName is null
            if (string.IsNullOrWhiteSpace(shop.ShopName))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.SHOPBYNAMENOTEMPTY);

                return result;
            }

            return result;
        }
    }
}