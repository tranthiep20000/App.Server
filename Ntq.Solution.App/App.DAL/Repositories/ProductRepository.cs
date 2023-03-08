using App.DAL.Data;
using App.DAL.Repositories.Commom;
using App.Domain.Entities;
using App.Domain.Entities.Results;
using App.Domain.Enum;
using App.Domain.Interfaces.IRepositories;
using App.Domain.Options;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.Repositories
{
    /// <summary>
    /// Information of ProductRepository
    /// CreatedBy: ThiepTT(27/02/2023)
    /// </summary>
    public class ProductRepository : BaseRepository<Product, int>, IProductRepository
    {
        public ProductRepository(DataContext dataContext) : base(dataContext)
        {
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
        public async Task<OperationResult<PagingResult<Product>>> GetAllPaging(string? valueFiler, TrendingEnum? trendingEnum,
            int pageNumber, int pageSize)
        {
            var result = new OperationResult<PagingResult<Product>>();

            if (pageNumber <= 0)
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageRepository.PAGENUMBER);

                return result;
            }
            if (pageSize <= 0)
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageRepository.PAGESIZE);

                return result;
            }

            try
            {
                var products = await _dataContext.Products.Where(p => p.DeleteAt == DeleteEnum.No).ToListAsync();

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

                var pagingResult = new PagingResult<Product>()
                {
                    ToTalPage = toTalPage,
                    ToTalRecord = toTalRecord,
                    Data = productsPaging
                };

                result.Data = pagingResult;
            }
            catch (Exception ex)
            {
                result.AddError(ErrorCode.ServerError, $"{ex.Message}");
            }

            return result;
        }
    }
}