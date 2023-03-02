using App.Domain.Entities.Results;
using App.Domain.Interfaces.IRepositories;

namespace App.Application.Services
{
    /// <summary>
    /// BaseService
    /// </summary>
    /// <typeparam name="T">T</typeparam>
    /// CreatedBy: ThiepTT(02/03/2023)
    public class BaseService<T>
    {
        protected readonly IBaseRepository<T> _baseRepository;

        public BaseService(IBaseRepository<T> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        /// <summary>
        /// Delete a record
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Number record delete success</returns>
        /// CreatedBy: ThiepTT(02/03/2023)
        public async Task<OperationResult<int>> Delete(int id)
        {
			var result = new OperationResult<int>();

			try
			{
                result = await _baseRepository.Delete(id);
			}
			catch (Exception ex)
			{
                result.AddError(ErrorCode.ServerError, $"{ex.Message}");
            }

           return result;   
        }
    }
}