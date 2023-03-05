using App.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace App.Domain.Entities
{
    /// <summary>
    /// Information of Product
    /// CreaetedBy: Thiep(27/02/2023)
    /// </summary>
    public class Product : BaseEntity
    {
        /// <summary>
        /// ProductId
        /// </summary>
        [Key]
        public int ProductId { get; set; }

        /// <summary>
        /// ProductName
        /// </summary>
        public string ProductName { get; set; } = string.Empty;

        /// <summary>
        /// Slug
        /// </summary>
        public string Slug { get; set; } = string.Empty;

        /// <summary>
        /// ShopId
        /// </summary>
        public int ShopId { get; set; }

        /// <summary>
        /// ProductDetail
        /// </summary>
        public string? ProductDetail { get; set; } = string.Empty;

        /// <summary>
        /// Price
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Trending
        /// </summary>
        public TrendingEnum? Trending { get; set; } = TrendingEnum.Low;

        /// <summary>
        /// Upload
        /// </summary>
        public string Upload { get; set; } = string.Empty;
    }
}