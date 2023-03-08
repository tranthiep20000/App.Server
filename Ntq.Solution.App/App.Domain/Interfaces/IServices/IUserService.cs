using App.Domain.Entities;
using App.Domain.Entities.Results;

namespace App.Domain.Interfaces.IServices
{
    /// <summary>
    /// Information of IUserService
    /// CreatedBy: ThiepTT(27/02/2023)
    /// </summary>
    public interface IUserService : IBaseService<User, int>
    {
        /// <summary>
        /// Login
        /// </summary>
        /// <param name="user">User</param>
        /// <returns>Token</returns>
        /// CreatedBy: ThiepTT(28/02/2023)
        public Task<OperationResult<string>> Login(User user);
    }
}