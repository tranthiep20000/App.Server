using App.Api.Contracts.Shop.Requests;
using App.Domain.Entities;
using AutoMapper;

namespace App.Api.MappingProfiles
{
    /// <summary>
    /// Information of ShopMapping
    /// CreatedBy: ThiepTT(27/02/2022)
    /// </summary>
    public class ShopMapping : Profile
    {
        /// <summary>
        /// ShopMapping Contructor
        /// </summary>
        public ShopMapping()
        {
            CreateMap<ShopCreate, Shop>();
            CreateMap<ShopUpdate, Shop>();
        }
    }
}