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
    /// Information of ProductRepository
    /// CreatedBy: ThiepTT(27/02/2023)
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _dataContext;

        public ProductRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        /// <summary>
        /// Create a record
        /// </summary>
        /// <param name="product">Product</param>
        /// <returns>Number record create success</returns>
        /// CreatedBy: ThiepTT(27/02/2023)
        public async Task<OperationResult<int>> Create(Product product)
        {
            var result = new OperationResult<int>();

            var strategy = _dataContext.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                using (var transaction = await _dataContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        var productNew = new Product()
                        {
                            ProductName = product.ProductName,
                            Slug = product.Slug,
                            ShopId = product.ShopId,
                            ProductDetail = product.ProductDetail,
                            Price = product.Price,
                            Trending = product.Trending,
                            Upload = product.Upload,
                            CreateAt = DateTime.Now
                        };

                        _dataContext.Products.Add(productNew);
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

            var productById = await _dataContext.Products.FirstOrDefaultAsync(p => p.ProductId == id);

            // Check productById is null
            if (productById is null)
            {
                result.AddError(ErrorCode.NotFound, string.Format(ConfigErrorMessageRepository.ProductByIdNotFound, id));

                return result;
            }

            var strategy = _dataContext.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                using (var transaction = await _dataContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        productById.DeleteAt = DeleteEnum.Yes;
                        productById.UpdateAt = DateTime.Now;

                        _dataContext.Products.Update(productById);
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
        /// <returns>List product</returns>
        /// CreatedBy: ThiepTT(27/02/2023)
        public async Task<OperationResult<IEnumerable<Product>>> GetAll()
        {
            var result = new OperationResult<IEnumerable<Product>>();

            try
            {
                var products = await _dataContext.Products.ToListAsync();

                result.Data = products;
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
        /// <returns>Product</returns>
        /// CreatedBy: ThiepTT(27/02/2023)
        public async Task<OperationResult<Product>> GetById(int id)
        {
            var result = new OperationResult<Product>();

            try
            {
                var product = await _dataContext.Products.FirstOrDefaultAsync(p => p.ProductId == id);

                // check product is null
                if (product is null)
                {
                    result.AddError(ErrorCode.NotFound, string.Format(ConfigErrorMessageRepository.ProductByIdNotFound, id));

                    return result;
                }

                result.Data = product;
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
        /// CreatedBy: ThiepTT(27/02/2023)
        public async Task<OperationResult<int>> Update(Product product, int id)
        {
            var result = new OperationResult<int>();

            var productById = await _dataContext.Products.FirstOrDefaultAsync(p => p.ProductId == id);

            // Check productById is null
            if (productById is null)
            {
                result.AddError(ErrorCode.NotFound, string.Format(ConfigErrorMessageRepository.ProductByIdNotFound, id));

                return result;
            }

            var strategy = _dataContext.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                using (var transaction = await _dataContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        productById.ProductName = product.ProductName;
                        productById.Slug = product.Slug;
                        productById.ShopId = product.ShopId;
                        productById.ProductDetail = product.ProductDetail;
                        productById.Price = product.Price;
                        productById.Trending = product.Trending;
                        productById.Upload = product.Upload;
                        productById.UpdateAt = DateTime.Now;

                        _dataContext.Products.Update(productById);
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
        /// Get all paging
        /// </summary>
        /// <param name="valueFiler">ValueFilter</param>
        /// <param name="trendingEnum">TrendingEnum</param>
        /// <param name="pageNumber">PageNumber</param>
        /// <param name="pageSize">PageSize</param>
        /// <returns>List product</returns>
        /// CreatedBy: ThiepTT(27/02/2023)
        public async Task<OperationResult<IEnumerable<Product>>> GetAllPaging(string? valueFiler, TrendingEnum? trendingEnum,
            int pageNumber, int pageSize)
        {
            var result = new OperationResult<IEnumerable<Product>>();

            if (pageNumber <= 0)
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageRepository.pageNumber);

                return result;
            }
            if (pageSize <= 0)
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageRepository.PageSize);

                return result;
            }

            try
            {
                var products = await _dataContext.Products.ToListAsync();

                // check valueFilter is not null
                if (!string.IsNullOrWhiteSpace(valueFiler))
                {
                    products = products.Where(p => p.ProductName.ToLower().Trim().Contains(valueFiler.ToLower().Trim())).ToList();
                }

                // check trendingEnum is not null
                if (trendingEnum is not null)
                {
                    products = products.Where(p => p.Trending == trendingEnum).ToList();
                }

                var productsPaging = products
                   .Skip((pageNumber - 1) * pageSize)
                   .Take(pageSize)
                   .ToList();

                var toTalRecord = products.Count();
                var toTalPage = (toTalRecord % pageSize) == 0 ? ((int)toTalRecord / (int)pageSize) : ((int)toTalRecord / (int)pageSize + 1);

                result.Data = productsPaging;
            }
            catch (Exception ex)
            {
                result.AddError(ErrorCode.ServerError, $"{ex.Message}");
            }

            return result;
        }
    }
}