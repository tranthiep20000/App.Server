using App.Domain.Entities.Results;
using App.Domain.Interfaces.IRepositories;
using App.Domain.Interfaces.IServices;

namespace App.Application.Services
{
    /// <summary>
    /// Information of BaseService
    /// </summary>
    /// <typeparam name="T">T</typeparam>
    /// <typeparam name="K">K</typeparam>
    /// CreatedBy: ThiepTT(02/03/2023)
    public abstract class BaseService<T, K> : IBaseService<T, K> where T : class
    {
        protected readonly IBaseRepository<T, K> _baseRepository;

        public BaseService(IBaseRepository<T, K> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        /// <summary>
        /// Delete a record
        /// </summary>
        /// <param name="K">Id</param>
        /// <returns>Number record delete success</returns>
        /// CreatedBy: ThiepTT(02/03/2023)
        public async Task<OperationResult<K>> Delete(K id)
        {
            var result = new OperationResult<K>();

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
        public async Task<OperationResult<K>> Create(T t)
        {
            var result = new OperationResult<K>();

            // Validate entity before saving
            var validation = await Validate(t, (K)Convert.ChangeType(0, typeof(K)));

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
        public async Task<OperationResult<K>> Update(T t, K id)
        {
            var result = new OperationResult<K>();

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
        protected virtual async Task<OperationResult<K>> Validate(T t, K id)
        {
            var result = new OperationResult<K>();

            return result;
        }
    }
}