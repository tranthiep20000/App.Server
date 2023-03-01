namespace App.Domain.Options
{
    /// <summary>
    /// Information of JwtSettings
    /// CreatedBy: ThiepTT(27/02/2023)
    /// </summary>
    public class JwtSettings
    {
        /// <summary>
        /// SigningKey
        /// </summary>
        public string SigningKey { get; set; } = string.Empty;

        /// <summary>
        /// Issuer
        /// </summary>
        public string Issuer { get; set; } = string.Empty;

        /// <summary>
        /// Audiences
        /// </summary>
        public string[] Audiences { get; set; } = default!;
    }
}