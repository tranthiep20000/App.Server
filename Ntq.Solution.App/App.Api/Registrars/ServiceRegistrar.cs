using App.Application.Services;
using App.Domain.Interfaces.IServices;
using VnEdu.Core.Services;

namespace App.Api.Registrars
{
    /// <summary>
    /// Information of RepositoryRegistrar
    /// CreatedBy: ThiepTT(27/02/2023)
    /// </summary>
    public class ServiceRegistrar : IWebApplicationBuilderRegistrar
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IShopService, ShopService>();
            builder.Services.AddScoped<IdentityService>();
        }
    }
}
