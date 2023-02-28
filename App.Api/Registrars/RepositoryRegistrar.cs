﻿using App.DAL.Repositories;
using App.Domain.Interfaces.IRepositories;

namespace App.Api.Registrars
{
    /// <summary>
    /// Information of RepositoryRegistrar
    /// CreatedBy: ThiepTT(27/02/2023)
    /// </summary>
    public class RepositoryRegistrar : IWebApplicationBuilderRegistrar
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IShopRepository, ShopRepository>();
        }
    }
}