using App.Domain.Entities;
using App.Domain.Entities.Results;
using App.Domain.Enum;
using Microsoft.AspNetCore.Http;

namespace App.Domain.Interfaces.IRepositories
{
    /// <summary>
    /// Information of IProductRepository
    /// CreaetedBy: Thiep(27/02/2023)
    /// </summary>
    public interface IProductRepository : IBaseRepository<Product>
    {
        /// <summary>
        /// GetAllPaging
        /// </summary>
        /// <param name="valueFiler">ValueFilter</param>
        /// <param name="trendingEnum">TrendingEnum</param>
        /// <param name="pageNumber">PageNumber</param>
        /// <param name="pageSize">PageSize</param>
        /// <returns>List Product</returns>
        /// CreaetedBy: Thiep(27/02/2023)
        public Task<OperationResult<IEnumerable<Product>>> GetAllPaging(string? valueFiler, TrendingEnum? trendingEnum, int pageNumber,
            int pageSize);
    }
}