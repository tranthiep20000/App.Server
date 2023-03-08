using App.Domain.Entities.Results;

namespace App.Domain.Interfaces.IServices
{
    /// <summary>
    /// Infomation of IBaseService
    /// </summary>
    /// <typeparam name="T">T</typeparam>
    /// <typeparam name="K">K</typeparam>
    /// CreaetedBy: ThiepTT(27/02/2023)
    public interface IBaseService<T, K>
    {
        /// <summary>
        /// Create a record
        /// </summary>
        /// <param name="t">T</param>
        /// <returns>Number record create success</returns>
        /// CreaetedBy: ThiepTT(27/02/2023)
        public Task<OperationResult<K>> Create(T t);

        /// <summary>
        /// Update a record 
        /// </summary>
        /// <param name="t">T</param>
        /// <param name="k">K</param>
        /// <returns>Number record update success</returns>
        /// CreaetedBy: ThiepTT(27/02/2023)
        public Task<OperationResult<K>> Update(T t, K k);

        /// <summary>
        /// Delete a record 
        /// </summary>
        /// <param name="k">K</param>
        /// <returns>Number record delete success</returns>
        /// CreaetedBy: ThiepTT(27/02/2023)
        public Task<OperationResult<K>> Delete(K k);
    }
}