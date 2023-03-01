using App.Domain.Entities.Results;

namespace App.Domain.Interfaces.IRepositories
{
    /// <summary>
    /// Information of IBaseRepository
    /// </summary>
    /// <typeparam name="T">T</typeparam>
    /// CreaetedBy: Thiep(27/02/2023)
    /// </summary>
    public interface IBaseRepository<T>
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
        /// <param name="id">Id</param>
        /// <returns>T</returns>
        /// CreaetedBy: Thiep(27/02/2023)
        public Task<OperationResult<T>> GetById(int id);

        /// <summary>
        /// Create a record
        /// </summary>
        /// <param name="t">T</param>
        /// <returns>Number record create success</returns>
        /// CreaetedBy: Thiep(27/02/2023)
        public Task<OperationResult<int>> Create(T t);

        /// <summary>
        /// Update a record
        /// </summary>
        /// <param name="t">T</param>
        /// <param name="id">Id</param>
        /// <returns>Number record update success</returns>
        /// CreaetedBy: Thiep(27/02/2023)
        public Task<OperationResult<int>> Update(T t, int id);

        /// <summary>
        /// Delete a record
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Number record delete success</returns>
        /// CreaetedBy: Thiep(27/02/2023)
        public Task<OperationResult<int>> Delete(int id);
    }
}