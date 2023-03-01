using App.Domain.Enum;

namespace App.Domain.Entities
{
    /// <summary>
    /// Information of BaseEntity
    /// CreatedBy: ThiepTT(27/02/2023)
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// CreatedAt
        /// </summary>
        public DateTime CreateAt { get; set; }

        /// <summary>
        /// UpdateAt
        /// </summary>
        public DateTime UpdateAt { get; set; }

        /// <summary>
        /// DeleteAt
        /// </summary>
        public DeleteEnum DeleteAt { get; set; } = DeleteEnum.No;
    }
}