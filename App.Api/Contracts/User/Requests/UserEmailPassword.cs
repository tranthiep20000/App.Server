namespace App.Api.Contracts.User.Request
{
    /// <summary>
    /// Information of UserEmailPassword
    /// CreatedBy: ThiepTT(28/02/2023)
    /// </summary>
    public class UserEmailPassword
    {
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }
}
