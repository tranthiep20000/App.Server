using App.DAL.Data;
using App.DAL.Repositories.Commom;
using App.Domain.Entities;
using App.Domain.Entities.Results;
using App.Domain.Enum;
using App.Domain.Interfaces.IRepositories;
using App.Domain.Options;
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
    public class UserRepository : BaseRepository<User, int>, IUserRepository
    {
        private readonly IdentityService _identityService;

        public UserRepository(DataContext dataContext, IdentityService identityService) : base(dataContext)
        {
            _identityService = identityService;
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
        public async Task<OperationResult<PagingResult<User>>> GetAllPaging(string? valueFiler, DateTime? createAt, TypeEnum? typeEnum,
            DeleteEnum? deleteEnum, int pageNumber, int pageSize)
        {
            var result = new OperationResult<PagingResult<User>>();

            if (pageNumber <= 0)
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageRepository.PAGENUMBER);

                return result;
            }
            if (pageSize <= 0)
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageRepository.PAGESIZE);

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
                
                var pagingResult = new PagingResult<User>()
                {
                    ToTalPage = toTalPage,
                    ToTalRecord = toTalRecord,
                    Data = usersPaging
                };

                result.Data = pagingResult;
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
                    result.AddError(ErrorCode.NotFound, string.Format(ConfigErrorMessageRepository.USERBYEMAILNOTFOUND, email));

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
                    result.AddError(ErrorCode.NotFound, string.Format(ConfigErrorMessageRepository.USERBYNAMENOTFOUND, userName));

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
                    result.AddError(ErrorCode.NotFound, ConfigErrorMessageRepository.USERBYNOTLOGIN);

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