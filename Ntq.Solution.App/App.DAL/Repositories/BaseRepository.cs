using App.DAL.Data;
using App.DAL.Repositories.Commom;
using App.Domain.Entities.Results;
using App.Domain.Enum;
using App.Domain.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace App.DAL.Repositories
{
    /// <summary>
    /// Information of BaseRepository
    /// </summary>
    /// <typeparam name="T">T</typeparam>
    /// CreatedBy: ThiepTT(02/03/2023)
    public class BaseRepository<T, K> : IBaseRepository<T, K> where T : class
    {
        protected readonly DataContext _dataContext;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
            _dbSet = _dataContext.Set<T>();
        }

        /// <summary>
        /// Create a record
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>Number record create success</returns>
        /// CreatedBy: ThiepTT(02/03/2023)
        public async Task<OperationResult<K>> Create(T entity)
        {
            var result = new OperationResult<K>();

            var strategy = _dataContext.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                using (var transaction = await _dataContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        var props = entity.GetType().GetProperties();

                        foreach (var prop in props)
                        {
                            prop.SetValue(entity, prop.GetValue(entity));

                            if (prop.Name == "CreateAt")
                            {
                                prop.SetValue(entity, DateTime.Now);
                            }

                            if (prop.Name == "Password")
                            {
                                prop.SetValue(entity, Convert.ToBase64String(Encoding.ASCII.GetBytes(prop.GetValue(entity).ToString())));
                            }
                        }

                        _dbSet.Add(entity);
                        var res = await _dataContext.SaveChangesAsync();
                        await transaction.CommitAsync();

                        result.Data = (K)Convert.ChangeType(res, typeof(K));
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
        /// CreatedBy: ThiepTT(02/03/2023)
        public async Task<OperationResult<K>> Delete(K id)
        {
            var result = new OperationResult<K>();

            var entity = await _dbSet.FindAsync(id);

            // check entity is null
            if (entity is null)
            {
                result.AddError(ErrorCode.NotFound, string.Format(ConfigErrorMessageRepository.ENTITYBYIDNOTFOUND, id));

                return result;
            }

            var strategy = _dataContext.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                using (var transaction = await _dataContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        var props = entity.GetType().GetProperties();

                        foreach (var prop in props)
                        {
                            if (prop.Name == "DeleteAt")
                            {
                                prop.SetValue(entity, DeleteEnum.Yes);
                            }

                            if (prop.Name == "UpdateAt")
                            {
                                prop.SetValue(entity, DateTime.Now);
                            }
                        }

                        _dbSet.Update(entity);
                        var res = await _dataContext.SaveChangesAsync();
                        await transaction.CommitAsync();

                        result.Data = (K)Convert.ChangeType(res, typeof(K));
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
        /// <returns>List T</returns>
        /// CreatedBy: ThiepTT(02/03/2023)
        public async Task<OperationResult<IEnumerable<T>>> GetAll()
        {
            var result = new OperationResult<IEnumerable<T>>();

            try
            {
                result.Data = await _dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                result.AddError(ErrorCode.ServerError, $"{ex.Message}");
            }

            return result;
        }

        /// <summary>
        /// GetById
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>T</returns>
        /// CreatedBy: ThiepTT(02/03/2023)
        public async Task<OperationResult<T>> GetById(K id)
        {
            var result = new OperationResult<T>();

            try
            {
                var entity = await _dbSet.FindAsync(id);

                // check entity is null
                if (entity is null)
                {
                    result.AddError(ErrorCode.NotFound, string.Format(ConfigErrorMessageRepository.ENTITYBYIDNOTFOUND, id));

                    return result;
                }

                result.Data = entity;
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
        /// <param name="entity">Entity</param>
        /// <param name="id">Id</param>
        /// <returns>Number record update success</returns>
        /// CreatedBy: ThiepTT(02/03/2023)
        public async Task<OperationResult<K>> Update(T entity, K id)
        {
            var result = new OperationResult<K>();

            var entityById = await _dbSet.FindAsync(id);

            // check entityById is null
            if (entityById is null)
            {
                result.AddError(ErrorCode.NotFound, string.Format(ConfigErrorMessageRepository.ENTITYBYIDNOTFOUND, id));

                return result;
            }

            var strategy = _dataContext.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                using (var transaction = await _dataContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        var props = entityById.GetType().GetProperties();

                        foreach (var prop in props)
                        {
                            if (prop == props.First() || prop.Name == "CreateAt")
                            {
                                continue;
                            }

                            var newValue = prop.GetValue(entity);

                            if (prop.Name == "UpdateAt")
                            {
                                newValue = DateTime.Now;
                            }

                            if (prop.Name == "Password")
                            {
                                newValue = Convert.ToBase64String(Encoding.ASCII.GetBytes(newValue.ToString()));
                            }

                            prop.SetValue(entityById, newValue);
                        }

                        _dbSet.Update(entityById);
                        var res = await _dataContext.SaveChangesAsync();
                        await transaction.CommitAsync();

                        result.Data = (K)Convert.ChangeType(res, typeof(K));
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
    }
}