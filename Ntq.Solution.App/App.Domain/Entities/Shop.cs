using System.ComponentModel.DataAnnotations;

namespace App.Domain.Entities
{
    /// <summary>
    /// Information of Shop
    /// CreaetedBy: Thiep(27/02/2023)
    /// </summary>
    public class Shop : BaseEntity
    {
        /// <summary>
        /// ShopId
        /// </summary>
        [Key]
        public int ShopId { get; set; }

        /// <summary>
        /// ShopName
        /// </summary>
        public string ShopName { get; set; } = string.Empty;
    }
}