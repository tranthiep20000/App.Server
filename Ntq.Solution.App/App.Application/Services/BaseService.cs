using App.Domain.Entities.Results;
using App.Domain.Interfaces.IRepositories;
using App.Domain.Interfaces.IServices;

namespace App.Application.Services
{
    /// <summary>
    /// Information of BaseService
    /// </summary>
    /// <typeparam name="T">T</typeparam>
    /// CreatedBy: ThiepTT(02/03/2023)
    public abstract class BaseService<T> : IBaseService<T> where T : class
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

        /// <summary>
        /// Created a record
        /// </summary>
        /// <param name="t">T</param>
        /// <returns>Number record create success</returns>
        /// CreatedBy: ThiepTT(07/03/2023)
        public async Task<OperationResult<int>> Create(T t)
        {
            var result = new OperationResult<int>();

            // Validate entity before saving
            var validation = await Validate(t, 0);

            if (validation.IsError)
            {
                return validation;
            }

            try
            {
                result = await _baseRepository.Create(t);
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
        /// <param name="t">T</param>
        /// <param name="id">Id</param>
        /// <returns>Number record update success</returns>
        /// CreatedBy: ThiepTT(07/03/2023)
        public async Task<OperationResult<int>> Update(T t, int id)
        {
            var result = new OperationResult<int>();

            // Validate entity before saving
            var validation = await Validate(t, id);

            if (validation.IsError)
            {
                return validation;
            }

            try
            {
                result = await _baseRepository.Update(t, id);
            }
            catch (Exception ex)
            {
                result.AddError(ErrorCode.ServerError, $"{ex.Message}");
            }

            return result;
        }

        /// <summary>
        /// Validate
        /// </summary>
        /// <param name="t">T</param>
        /// <returns>Int</returns>
        /// CreatedBy: ThiepTT(07/03/2023)
        protected virtual async Task<OperationResult<int>> Validate(T t, int id)
        {
            var result = new OperationResult<int>();

            return result;
        }
    }
}