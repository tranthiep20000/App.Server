namespace App.Api.Registrars
{
    /// <summary>
    /// Information of IWebApplicationRegistrar
    /// CreatedBy: ThiepTT(27/02/2023)
    /// </summary>
    public interface IWebApplicationRegistrar : IRegistrar
    {
        /// <summary>
        /// RegisterPipelineConponents
        /// </summary>
        /// <param name="app">WebApplication</param>
        /// CreatedBy: ThiepTT(27/02/2023)
        public void RegisterPipelineConponents(WebApplication app);
    }
}
