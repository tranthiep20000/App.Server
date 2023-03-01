namespace App.Domain.Entities.Results
{
    /// <summary>
    /// Information of OperationResult
    /// </summary>
    /// <typeparam name="T">T</typeparam>
    /// CreatedBy: ThiepTT(27/02/2023)
    public class OperationResult<T>
    {
        /// <summary>
        /// Data
        /// </summary>
        public T Data { get; set; } = default!;

        /// <summary>
        /// IsError
        /// </summary>
        public bool IsError { get; private set; }

        /// <summary>
        /// List Errors
        /// </summary>
        public Error ErrorData { get; private set; } = default!;

        /// <summary>
        /// AddError
        /// </summary>
        /// <param name="code">Code</param>
        /// <param name="message">Message</param>
        public void AddError(ErrorCode code, string message)
        {
            IsError = true;
            ErrorData = new Error() { Code = code, Message = $"{message}" };
        }
    }
}