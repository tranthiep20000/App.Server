using App.Domain.Entities.Results;

namespace App.Domain.Interfaces.IServices
{
    /// <summary>
    /// Infomation of IBaseService
    /// </summary>
    /// <typeparam name="T">T</typeparam>
    /// CreaetedBy: ThiepTT(27/02/2023)
    public interface IBaseService<T>
    {
        /// <summary>
        /// Create a record
        /// </summary>
        /// <param name="t">T</param>
        /// <returns>Number record create success</returns>
        /// CreaetedBy: ThiepTT(27/02/2023)
        public Task<OperationResult<int>> Create(T t);

        /// <summary>
        /// Update a record 
        /// </summary>
        /// <param name="t">T</param>
        /// <param name="id">Id</param>
        /// <returns>Number record update success</returns>
        /// CreaetedBy: ThiepTT(27/02/2023)
        public Task<OperationResult<int>> Update(T t, int id);

        /// <summary>
        /// Delete a record 
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Number record delete success</returns>
        /// CreaetedBy: ThiepTT(27/02/2023)
        public Task<OperationResult<int>> Delete(int id);
    }
}