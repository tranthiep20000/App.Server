using App.Api.Filters;
using App.Api.Options;
using Microsoft.OpenApi.Models;

namespace App.Api.Registrars
{
    /// <summary>
    /// Information of WebApplicationBuilderRegistrar
    /// CreatedBy: ThiepTT(27/02/2023)
    /// </summary>
    public class WebApplicationBuilderRegistrar : IWebApplicationBuilderRegistrar
    {
        /// <summary>
        /// RegisterServices
        /// </summary>
        /// <param name="builder">WebApplicationBuilder</param>
        /// CreatedBy: ThiepTT(27/02/2023)
        public void RegisterServices(WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();

            builder.Services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo { Title = "App Api", Version = "v1" });
                var filePath = Path.Combine(System.AppContext.BaseDirectory, "App.Api.xml");
                config.IncludeXmlComments(filePath);
            });

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen();

            builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

            builder.Services.ConfigureOptions<ConfiguraSwagerOption>();

            builder.Services.AddAutoMapper(typeof(Program));

            builder.Services.AddMvc(options => options.Filters.Add(new AppExceptionAttibute()));
        }
    }
}