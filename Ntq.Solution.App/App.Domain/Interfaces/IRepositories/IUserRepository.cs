using App.Domain.Entities;
using App.Domain.Entities.Results;
using App.Domain.Enum;
using App.Domain.Options;

namespace App.Domain.Interfaces.IRepositories
{
    /// <summary>
    /// Information of IUserRepository
    /// CreaetedBy: Thiep(27/02/2023)
    /// </summary>
    public interface IUserRepository : IBaseRepository<User, int>
    {
        /// <summary>
        /// GetAllPaging
        /// </summary>
        /// <param name="valueFiler">ValueFilter</param>
        /// <param name="createAt">CreateAt</param>
        /// <param name="typeEnum">TypeEnum</param>
        /// <param name="deleteEnum">DeleteEnum</param>
        /// <param name="pageNumber">PageNumber</param>
        /// <param name="pageSize">PageSize</param>
        /// <returns>List User</returns>
        /// CreaetedBy: Thiep(27/02/2023)
        public Task<OperationResult<PagingResult<User>>> GetAllPaging(string? valueFiler, DateTime? createAt, TypeEnum? typeEnum,
            DeleteEnum? deleteEnum, int pageNumber, int pageSize);

        /// <summary>
        /// GetByUserName
        /// </summary>
        /// <param name="userName">UserName</param>
        /// <returns>User</returns>
        /// CreatedBy: ThiepTT(27/02/2023)
        public Task<OperationResult<User>> GetByUserName(string userName);

        /// <summary>
        /// GetByEmail
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns>User</returns>
        /// CreatedBy: ThiepTT(27/02/2023)
        public Task<OperationResult<User>> GetByEmail(string email);

        /// <summary>
        /// GetUserByEmailPassword
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="password">Password</param>
        /// <returns>string</returns>
        /// CreatedBy: ThiepTT(28/02/2023)
        public Task<OperationResult<string>> GetUserByEmailPassword(string email, string password);
    }
}