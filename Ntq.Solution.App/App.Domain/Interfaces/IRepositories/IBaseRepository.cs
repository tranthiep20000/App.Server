using App.Domain.Entities.Results;

namespace App.Domain.Interfaces.IRepositories
{
    /// <summary>
    /// Information of IBaseRepository
    /// </summary>
    /// <typeparam name="T">T</typeparam>
    /// <typeparam name="K">K</typeparam>
    /// CreaetedBy: Thiep(27/02/2023)
    /// </summary>
    public interface IBaseRepository<T, K>
    {
        /// <summary>
        /// Get all
        /// </summary>
        /// <returns>List T</returns>
        /// CreaetedBy: Thiep(27/02/2023)
        public Task<OperationResult<IEnumerable<T>>> GetAll();

        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="k">K</param>
        /// <returns>T</returns>
        /// CreaetedBy: Thiep(27/02/2023)
        public Task<OperationResult<T>> GetById(K k);

        /// <summary>
        /// Create a record
        /// </summary>
        /// <param name="t">T</param>
        /// <returns>Number record create success</returns>
        /// CreaetedBy: Thiep(27/02/2023)
        public Task<OperationResult<K>> Create(T t);

        /// <summary>
        /// Update a record
        /// </summary>
        /// <param name="t">T</param>
        /// <param name="k">K</param>
        /// <returns>Number record update success</returns>
        /// CreaetedBy: Thiep(27/02/2023)
        public Task<OperationResult<K>> Update(T t, K k);

        /// <summary>
        /// Delete a record
        /// </summary>
        /// <param name="k">K</param>
        /// <returns>Number record delete success</returns>
        /// CreaetedBy: Thiep(27/02/2023)
        public Task<OperationResult<K>> Delete(K k);
    }
}