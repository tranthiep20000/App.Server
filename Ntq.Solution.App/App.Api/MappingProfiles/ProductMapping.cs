using App.Api.Contracts.Product.Requests;
using App.Domain.Entities;
using AutoMapper;

namespace App.Api.MappingProfiles
{
    /// <summary>
    /// Information of ProductMapping
    /// CreatedBy: ThiepTT(27/02/2023)
    /// </summary>
    public class ProductMapping : Profile
    {
        /// <summary>
        /// ProductMapping Contructor
        /// </summary>
        public ProductMapping()
        {
            CreateMap<ProductCreate, Product>();
            CreateMap<ProductUpdate, Product>();
        }
    }
}