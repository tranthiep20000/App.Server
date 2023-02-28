using App.Api.Contracts.User.Request;
using App.Api.Contracts.User.Requests;
using App.Domain.Entities;
using AutoMapper;

namespace CwkSocial.API.MappingProfiles
{
    /// <summary>
    /// Information of UserProfileMapping
    /// CreatedBy: ThiepTT(27/02/2022)
    /// </summary>
    public class UserProfileMapping : Profile
    {
        /// <summary>
        /// UserProfileMapping Contructor
        /// </summary>
        public UserProfileMapping()
        {
            CreateMap<UserCreate, User>();
            CreateMap<UserUpdate, User>();
            CreateMap<UserEmailPassword, User>();
        }
    }
}