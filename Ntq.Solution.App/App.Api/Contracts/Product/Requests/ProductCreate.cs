using App.Domain.Enum;

namespace App.Api.Contracts.Product.Requests
{
    /// <summary>
    /// Information of ProductCreate
    /// CreatedBy: ThiepTT(27/02/2023)
    /// </summary>
    public class ProductCreate
    {
        /// <summary>
        /// ProductName
        /// </summary>
        public string? ProductName { get; set; } = string.Empty;

        /// <summary>
        /// Slug
        /// </summary>
        public string? Slug { get; set; } = string.Empty;

        /// <summary>
        /// ShopId
        /// </summary>
        public int? ShopId { get; set; }

        /// <summary>
        /// ProductDetail
        /// </summary>
        public string? ProductDetail { get; set; } = string.Empty;

        /// <summary>
        /// Price
        /// </summary>
        public double? Price { get; set; }

        /// <summary>
        /// Trending
        /// </summary>
        public TrendingEnum? Trending { get; set; } = TrendingEnum.Low;

        /// <summary>
        /// File
        /// </summary>
        public IFormFile? File { get; set; } = default!;
    }
}
