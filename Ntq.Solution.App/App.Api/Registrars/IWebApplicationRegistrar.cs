namespace App.Api.Registrars
{
    /// <summary>
    /// Information of IWebApplicationRegistrar
    /// CreatedBy: ThiepTT(27/02/2023)
    /// </summary>
    public interface IWebApplicationRegistrar : IRegistrar
    {
        public void RegisterPipelineConponents(WebApplication app);
    }
}
