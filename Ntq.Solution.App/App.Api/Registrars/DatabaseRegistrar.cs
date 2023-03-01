using Microsoft.EntityFrameworkCore;
using App.DAL.Data;

namespace App.Api.Registrars
{
    /// <summary>
    /// Information of DatabaseRegistrar
    /// CreatedBy: ThiepTT(27/02/2023)
    /// </summary>
    public class DatabaseRegistrar : IWebApplicationBuilderRegistrar
    {
        /// <summary>
        /// RegisterServices
        /// </summary>
        /// <param name="builder">WebApplicationBuilder</param>
        /// CreatedBy: ThiepTT(27/02/2023)
        public void RegisterServices(WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("Default");

            builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString,
                options => options.EnableRetryOnFailure()));
        }
    }
}
