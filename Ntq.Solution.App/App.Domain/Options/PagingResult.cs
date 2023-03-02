namespace App.Domain.Options
{
    /// <summary>
    /// Information of PagingResult
    /// </summary>
    /// <typeparam name="T">T</typeparam>
    /// CreatedBy: ThiepTT(03/02/2023)
    public class PagingResult<T>
    {
        /// <summary>
        /// ToTalRecord
        /// </summary>
        public int ToTalRecord { get; set; }

        /// <summary>
        /// ToTalPage
        /// </summary>
        public int ToTalPage { get; set; }

        /// <summary>
        /// Data
        /// </summary>
        public IEnumerable<T> Data { get; set; } = Enumerable.Empty<T>();
    }
}