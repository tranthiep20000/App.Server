namespace App.Api.Contracts.Common
{
    /// <summary>
    /// Information of ErrorResponse
    /// CreatedBy: ThiepTT(02/03/2023)
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// StatusCode
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// StatusPhrase
        /// </summary>
        public string StatusPhrase { get; set; } = string.Empty;

        /// <summary>
        /// Errors
        /// </summary>
        public List<string> Errors { get; } = new List<string>();

        /// <summary>
        /// TimeStamp
        /// </summary>
        public DateTime TimeStamp { get; set; }
    }
}