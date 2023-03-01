namespace App.Api.Registrars
{
    /// <summary>
    /// Information of IWebApplicationBuilderRegistrar
    /// CreatedBy: ThiepTT(27/02/2023)
    /// </summary>
    public interface IWebApplicationBuilderRegistrar : IRegistrar
    {
        public void RegisterServices(WebApplicationBuilder builder);
    }
}
