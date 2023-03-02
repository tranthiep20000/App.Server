using App.Api.Registrars;

namespace App.Api.Extensions
{
    /// <summary>
    /// Infomation of RegistrarExtension
    /// CreatedBy: ThiepTT(27/02/2023)
    /// </summary>
    public static class RegistrarExtension
    {
        /// <summary>
        /// RegisterServices
        /// </summary>
        /// <param name="builder">WebApplicationBuilder</param>
        /// <param name="scanningType">Type</param>
        /// CreatedBy: ThiepTT(27/02/2023)
        public static void RegisterServices(this WebApplicationBuilder builder, Type scanningType)
        {
            var registrars = GetRegistrars<IWebApplicationBuilderRegistrar>(scanningType);

            foreach (var registrar in registrars)
            {
                registrar.RegisterServices(builder);
            }
        }

        /// <summary>
        /// RegisterPipelineConponents
        /// </summary>
        /// <param name="app">WebApplication</param>
        /// <param name="scanningType">Type</param>
        /// CreatedBy: ThiepTT(27/02/2023)
        public static void RegisterPipelineConponents(this WebApplication app, Type scanningType)
        {
            var registrars = GetRegistrars<IWebApplicationRegistrar>(scanningType);

            foreach (var registrar in registrars)
            {
                registrar.RegisterPipelineConponents(app);
            }
        }

        /// <summary>
        /// GetRegistrars
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="scanningType">Type</param>
        /// <returns>IEnumerable T</returns>
        /// CreatedBy: ThiepTT(27/02/2023)
        private static IEnumerable<T> GetRegistrars<T>(Type scanningType) where T : IRegistrar
        {
            return scanningType.Assembly.GetTypes()
                .Where(t => t.IsAssignableTo(typeof(T)) && !t.IsAbstract && !t.IsInterface)
                .Select(Activator.CreateInstance)
                .Cast<T>();
        }
    }
}
