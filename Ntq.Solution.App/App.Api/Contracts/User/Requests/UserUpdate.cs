using App.Domain.Enum;

namespace App.Api.Contracts.User.Requests
{
    /// <summary>
    /// Information of UserUpdate
    /// CreatedBy: ThiepTT(27/02/2023)
    /// </summary>
    public class UserUpdate
    {
        /// <summary>
        /// UserName
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Type
        /// </summary>
        public TypeEnum Type { get; set; } = TypeEnum.User;
    }
}
