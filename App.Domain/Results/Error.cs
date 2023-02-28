namespace App.Domain.Entities.Results
{
    /// <summary>
    /// Information of Error
    /// CreatedBy: ThiepTT(27/02/2023)
    /// </summary>
    public class Error
    {
        /// <summary>
        /// Code
        /// </summary>
        public ErrorCode Code { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; } = string.Empty;
    }
}