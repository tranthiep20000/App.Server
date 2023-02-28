using App.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace App.Domain.Entities
{
    /// <summary>
    /// Information of User
    /// CreaetedBy: Thiep(27/02/2023)
    /// </summary>
    public class User : BaseEntity
    {
        /// <summary>
        /// UserId
        /// </summary>
        [Key]
        public int UserId { get; set; }

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