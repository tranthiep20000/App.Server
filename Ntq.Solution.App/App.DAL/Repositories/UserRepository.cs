using App.DAL.Data;
using App.DAL.Repositories.Commom;
using App.Domain.Entities;
using App.Domain.Entities.Results;
using App.Domain.Enum;
using App.Domain.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VnEdu.Core.Services;

namespace App.DAL.Repositories
{
    /// <summary>
    /// Information of UserRepository
    /// CreatedBy: ThiepTT(27/02/2023)
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _dataContext;
        private readonly IdentityService _identityService;

        public UserRepository(DataContext dataContext, IdentityService identityService)
        {
            _dataContext = dataContext;
            _identityService = identityService;
        }

        /// <summary>
        /// Create a record
        /// </summary>
        /// <param name="user">User</param>
        /// <returns>Number record create success</returns>
        /// CreatedBy: ThiepTT(27/02/2023)
        public async Task<OperationResult<int>> Create(User user)
        {
            var result = new OperationResult<int>();

            var strategy = _dataContext.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                using (var transaction = await _dataContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        var userNew = new User()
                        {
                            UserName = user.UserName,
                            Email = user.Email,
                            Password = Convert.ToBase64String(Encoding.ASCII.GetBytes(user.Password)),
                            Type = user.Type,
                            CreateAt = DateTime.Now,
                        };

                        _dataContext.Users.Add(userNew);
                        await _dataContext.SaveChangesAsync();
                        await transaction.CommitAsync();

                        result.Data = 1;
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
        /// CreatedBy: ThiepTT(27/02/2023)
        public async Task<OperationResult<int>> Delete(int id)
        {
            var result = new OperationResult<int>();

            var userById = await _dataContext.Users.FirstOrDefaultAsync(u => u.UserId == id);

            // Check userById is null
            if (userById is null)
            {
                result.AddError(ErrorCode.NotFound, string.Format(ConfigErrorMessageRepository.UserByIdNotFound, id));

                return result;
            }

            var strategy = _dataContext.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                using (var transaction = await _dataContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        userById.DeleteAt = DeleteEnum.Yes;
                        userById.UpdateAt = DateTime.Now;

                        _dataContext.Users.Update(userById);
                        await _dataContext.SaveChangesAsync();
                        await transaction.CommitAsync();

                        result.Data = 1;
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
        /// <returns>List user</returns>
        /// CreatedBy: ThiepTT(27/02/2023)
        public async Task<OperationResult<IEnumerable<User>>> GetAll()
        {
            var result = new OperationResult<IEnumerable<User>>();

            try
            {
                var users = await _dataContext.Users.ToListAsync();

                result.Data = users;
            }
            catch (Exception ex)
            {
                result.AddError(ErrorCode.ServerError, $"{ex.Message}");
            }

            return result;
        }

        /// <summary>
        /// Get all paging
        /// </summary>
        /// <param name="valueFiler">ValueFilter</param>
        /// <param name="createAt">CreateAt</param>
        /// <param name="typeEnum">TypeEnum</param>
        /// <param name="deleteEnum">DeleteEnum</param>
        /// <param name="pageNumber">PageNumber</param>
        /// <param name="pageSize">PageSize</param>
        /// <returns>List user</returns>
        /// CreatedBy: ThiepTT(27/02/2023)
        public async Task<OperationResult<IEnumerable<User>>> GetAllPaging(string? valueFiler, DateTime? createAt, TypeEnum? typeEnum,
            DeleteEnum? deleteEnum, int pageNumber, int pageSize)
        {
            var result = new OperationResult<IEnumerable<User>>();

            if (pageNumber <= 0)
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageRepository.pageNumber);

                return result;
            }
            if (pageSize <= 0)
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageRepository.PageSize);

                return result;
            }

            try
            {
                var users = await _dataContext.Users.ToListAsync();

                // check valueFilter is not null
                if (!string.IsNullOrWhiteSpace(valueFiler))
                {
                    users = users.Where(u => u.UserName.ToLower().Trim().Contains(valueFiler.ToLower().Trim())).ToList();
                }

                // check createAt is not null
                if (createAt is not null)
                {
                    users = users.Where(u => u.CreateAt.Date.CompareTo(createAt) == 0).ToList();
                }

                // check type is not null
                if (typeEnum is not null)
                {
                    users = users.Where(u => u.Type == typeEnum).ToList();
                }

                // check deleteAt is not null
                if (deleteEnum is not null)
                {
                    users = users.Where(u => u.DeleteAt == deleteEnum).ToList();
                }

                var usersPaging = users
                   .Skip((pageNumber - 1) * pageSize)
                   .Take(pageSize)
                   .ToList();

                var toTalRecord = users.Count();
                var toTalPage = (toTalRecord % pageSize) == 0 ? ((int)toTalRecord / (int)pageSize) : ((int)toTalRecord / (int)pageSize + 1);

                result.Data = usersPaging;
            }
            catch (Exception ex)
            {
                result.AddError(ErrorCode.ServerError, $"{ex.Message}");
            }

            return result;
        }

        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>User</returns>
        /// CreatedBy: ThiepTT(27/02/2023)
        public async Task<OperationResult<User>> GetById(int id)
        {
            var result = new OperationResult<User>();

            try
            {
                var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.UserId == id);

                // check user is null
                if (user is null)
                {
                    result.AddError(ErrorCode.NotFound, string.Format(ConfigErrorMessageRepository.UserByIdNotFound, id));

                    return result;
                }

                result.Data = user;
            }
            catch (Exception ex)
            {
                result.AddError(ErrorCode.ServerError, $"{ex.Message}");
            }

            return result;
        }

        /// <summary>
        /// Get by email
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns>User</returns>
        /// CreatedBy: ThiepTT(27/02/2023)
        public async Task<OperationResult<User>> GetByEmail(string email)
        {
            var result = new OperationResult<User>();

            try
            {
                var user = await _dataContext.Users
                    .FirstOrDefaultAsync(u => u.Email.ToLower().Trim().Equals(email.ToLower().Trim()));

                // check user is null
                if (user is null)
                {
                    result.AddError(ErrorCode.NotFound, string.Format(ConfigErrorMessageRepository.UserByEmailNotFound, email));

                    return result;
                }

                result.Data = user;
            }
            catch (Exception ex)
            {
                result.AddError(ErrorCode.ServerError, $"{ex.Message}");
            }

            return result;
        }

        /// <summary>
        /// Get by username
        /// </summary>
        /// <param name="userName">UserName</param>
        /// <returns>User</returns>
        /// CreatedBy: ThiepTT(27/02/2023)
        public async Task<OperationResult<User>> GetByUserName(string userName)
        {
            var result = new OperationResult<User>();

            try
            {
                var user = await _dataContext.Users
                    .FirstOrDefaultAsync(u => u.UserName.ToLower().Trim().Equals(userName.ToLower().Trim()));

                // check user is null
                if (user is null)
                {
                    result.AddError(ErrorCode.NotFound, string.Format(ConfigErrorMessageRepository.UserByNameNotFound, userName));

                    return result;
                }

                result.Data = user;
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
        /// <param name="user">User</param>
        /// <param name="id">Id</param>
        /// <returns>Number record update success</returns>
        /// CreatedBy: ThiepTT(27/02/2023)
        public async Task<OperationResult<int>> Update(User user, int id)
        {
            var result = new OperationResult<int>();

            var userById = await _dataContext.Users.FirstOrDefaultAsync(u => u.UserId == id);

            // Check userById is null
            if (userById is null)
            {
                result.AddError(ErrorCode.NotFound, string.Format(ConfigErrorMessageRepository.UserByIdNotFound, id));

                return result;
            }
            var strategy = _dataContext.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                using (var transaction = await _dataContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        userById.UserName = user.UserName;
                        userById.Email = user.Email;
                        userById.Password = Convert.ToBase64String(Encoding.ASCII.GetBytes(user.Password));
                        userById.UpdateAt = DateTime.Now;

                        _dataContext.Users.Update(userById);
                        await _dataContext.SaveChangesAsync();
                        await transaction.CommitAsync();

                        result.Data = 1;
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
        /// Get user by email password
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="password">Password</param>
        /// <returns>Token</returns>
        /// CreatedBy: ThiepTT(28/02/2023)
        public async Task<OperationResult<string>> GetUserByEmailPassword(string email, string password)
        {
            var result = new OperationResult<string>();

            try
            {
                var userByEmailPassword = await _dataContext.Users.FirstOrDefaultAsync(u =>
                    u.Email.ToLower().Trim().Equals(email.ToLower().Trim())
                    && u.Password.ToLower().Trim().Equals(Convert.ToBase64String(Encoding.ASCII.GetBytes(password)).ToLower().Trim()));

                // Check userByEmailPassword is null
                if (userByEmailPassword is null)
                {
                    result.AddError(ErrorCode.NotFound, ConfigErrorMessageRepository.UserByNotLogin);

                    return result;
                }

                result.Data = GetJwtString(userByEmailPassword);
            }
            catch (Exception ex)
            {
                result.AddError(ErrorCode.ServerError, $"{ex.Message}");
            }

            return result;
        }

        /// <summary>
        /// Get jwt string
        /// </summary>
        /// <param name="user">User</param>
        /// <returns>Token</returns>
        /// CreatedBy: ThiepTT(28/02/2023)
        private string GetJwtString(User user)
        {
            var claimIndetity = new ClaimsIdentity(new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("UserId", user.UserId.ToString()),
                new Claim("UserName", user.UserName),
                new Claim("Email", user.Email)
            });

            var token = _identityService.CreateSecurityToken(claimIndetity);
            return _identityService.WriteToken(token);
        }
    }
}