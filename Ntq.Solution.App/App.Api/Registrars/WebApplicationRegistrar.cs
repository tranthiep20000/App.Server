namespace App.Api.Registrars
{
    /// <summary>
    /// Information of WebApplicationRegistrar
    /// CreatedBy: ThiepTT(27/02/2023)
    /// </summary>
    public class WebApplicationRegistrar : IWebApplicationRegistrar
    {
        /// <summary>
        /// RegisterPipelineConponents
        /// </summary>
        /// <param name="app">WebApplication</param>
        /// CreatedBy: ThiepTT(27/02/2023)
        public void RegisterPipelineConponents(WebApplication app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "App V1");
                config.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseCors(options => options.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader());

            app.MapControllers();
        }
    }
}
