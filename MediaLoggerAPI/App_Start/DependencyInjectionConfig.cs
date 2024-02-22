
using MediaLogger.Aplication.BL;
using MediaLogger.Application.BL;
using MediaLogger.Application.Validation;
using MediaLogger.Persistence.SQLServer;
using MediaLogger.Persistence.SQLServer.Business;
using MediaLoggerAPI.Middleware;

namespace MediaLoggerAPI.App_Start
{
    internal static class DependencyInjectionConfig
    {
        /// <summary>
        /// Add dependencies injection configuration
        /// </summary>
        /// <param name="services"></param>
        internal static void AddDependenciesInjectionConfig(this IServiceCollection services)
        {

            #region Application BL

            services.AddScoped(typeof(AuthBL));
            services.AddScoped(typeof(PayPadBL));
            services.AddScoped(typeof(LogBL));
            #endregion

            #region Validations

            services.AddScoped(typeof(PayPadValidation));
            services.AddScoped(typeof(AuthMiddleware));
            services.AddScoped(typeof(MediaMiddleware));
            services.AddScoped(typeof(Token));
            #endregion


            #region Repository
            services.AddScoped(typeof(UserRepository));
            services.AddScoped(typeof(PayPadRepository));
            services.AddScoped(typeof(ClientRepository));
            services.AddScoped(typeof(LogRepository));
            #endregion
        }
    }
}
