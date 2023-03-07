namespace App.Api.Registrars
{
    /// <summary>
    /// Information of IWebApplicationBuilderRegistrar
    /// CreatedBy: ThiepTT(27/02/2023)
    /// </summary>
    public interface IWebApplicationBuilderRegistrar : IRegistrar
    {
        /// <summary>
        /// RegisterServices
        /// </summary>
        /// <param name="builder">WebApplicationBuilder</param>
        /// CreatedBy: ThiepTT(27/02/2023)
        public void RegisterServices(WebApplicationBuilder builder);
    }
}
