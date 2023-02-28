using App.DAL.Data;
using App.DAL.Repositories.Commom;
using App.Domain.Entities;
using App.Domain.Entities.Results;
using App.Domain.Enum;
using App.Domain.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace App.DAL.Repositories
{
    /// <summary>
    /// Information of ShopRepository
    /// CreatedBy: ThiepTT(27/02/2023)
    /// </summary>
    public class ShopRepository : IShopRepository
    {
        private readonly DataContext _dataContext;

        public ShopRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        /// <summary>
        /// Create a record
        /// </summary>
        /// <param name="shop">Shop</param>
        /// <returns>Number record create success</returns>
        /// CreatedBy: ThiepTT(27/02/2023)
        public async Task<OperationResult<int>> Create(Shop shop)
        {
            var result = new OperationResult<int>();

            var strategy = _dataContext.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                using (var transaction = await _dataContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        var shopNew = new Shop()
                        {
                            ShopName = shop.ShopName,
                            CreateAt = DateTime.Now
                        };

                        _dataContext.Shops.Add(shopNew);
                        await _dataContext.SaveChangesAsync();
                        await transaction.CommitAsync();

                        result.Data = 1;
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        result.AddError(ErrorCode.ServerError, $"{ex.Message}");
                    }
                }
            });

            return result;
        }

        /// <summary>
        /// Delete a record
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Number record delete success</returns>
        /// CreatedBy: ThiepTT(27/02/2023)
        public async Task<OperationResult<int>> Delete(int id)
        {
            var result = new OperationResult<int>();

            var shopById = await _dataContext.Shops.FirstOrDefaultAsync(s => s.ShopId == id);

            // Check shopById is null
            if (shopById is null)
            {
                result.AddError(ErrorCode.NotFound, string.Format(ConfigErrorMessageRepository.ShopByIdNotFound, id));

                return result;
            }

            var strategy = _dataContext.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                using (var transaction = await _dataContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        shopById.DeleteAt = DeleteEnum.Yes;
                        shopById.UpdateAt = DateTime.Now;

                        _dataContext.Shops.Update(shopById);
                        await _dataContext.SaveChangesAsync();
                        await transaction.CommitAsync();

                        result.Data = 1;
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        result.AddError(ErrorCode.ServerError, $"{ex.Message}");
                    }
                }
            });

            return result;
        }

        /// <summary>
        /// Get all
        /// </summary>
        /// <returns>List shop</returns>
        /// CreatedBy: ThiepTT(27/02/2023)
        public async Task<OperationResult<IEnumerable<Shop>>> GetAll()
        {
            var result = new OperationResult<IEnumerable<Shop>>();

            try
            {
                var shops = await _dataContext.Shops.ToListAsync();

                result.Data = shops;
            }
            catch (Exception ex)
            {
                result.AddError(ErrorCode.ServerError, $"{ex.Message}");
            }

            return result;
        }

        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Shop</returns>
        /// CreatedBy: ThiepTT(27/02/2023)
        public async Task<OperationResult<Shop>> GetById(int id)
        {
            var result = new OperationResult<Shop>();

            try
            {
                var shop = await _dataContext.Shops.FirstOrDefaultAsync(s => s.ShopId == id);

                // check shop is null
                if (shop is null)
                {
                    result.AddError(ErrorCode.NotFound, string.Format(ConfigErrorMessageRepository.ShopByIdNotFound, id));

                    return result;
                }

                result.Data = shop;
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
        /// CreatedBy: ThiepTT(27/02/2023)
        public async Task<OperationResult<int>> Update(Shop shop, int id)
        {
            var result = new OperationResult<int>();

            var shopById = await _dataContext.Shops.FirstOrDefaultAsync(s => s.ShopId == id);

            // Check shopById is null
            if (shopById is null)
            {
                result.AddError(ErrorCode.NotFound, string.Format(ConfigErrorMessageRepository.ShopByIdNotFound, id));

                return result;
            }

            var strategy = _dataContext.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                using (var transaction = await _dataContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        shopById.ShopName = shop.ShopName;
                        shopById.UpdateAt = DateTime.Now;

                        _dataContext.Shops.Update(shopById);
                        await _dataContext.SaveChangesAsync();
                        await transaction.CommitAsync();

                        result.Data = 1;
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        result.AddError(ErrorCode.ServerError, $"{ex.Message}");
                    }
                }
            });

            return result;
        }
    }
}