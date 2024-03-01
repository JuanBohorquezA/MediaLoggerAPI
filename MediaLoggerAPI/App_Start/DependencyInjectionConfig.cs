
using MediaLogger.Aplication.BL;
using MediaLogger.Application.BL;
using MediaLogger.Application.Validation;
using MediaLogger.Domain.Interfaces.Application;
using MediaLogger.Domain.Interfaces.Application.Validations;
using MediaLogger.Domain.Interfaces.Persistence;
using MediaLogger.Persistence.SQLServer;
using MediaLogger.Persistence.SQLServer.Business;

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

            services.AddScoped(typeof(IAuthBL), typeof(AuthBL));
            services.AddScoped(typeof(IPayPadBL), typeof(PayPadBL));
            services.AddScoped(typeof(ILogBL), typeof(LogBL));
            #endregion

            #region Validations

            services.AddScoped(typeof(IPayPadValidation), typeof(PayPadValidation));
            services.AddScoped(typeof(ITokenBL), typeof(Token));
            #endregion


            #region Repository
            services.AddScoped(typeof(IPayPadRepository), typeof(PayPadRepository));
            services.AddScoped(typeof(ILogRepository), typeof(LogRepository));
            #endregion
        }
    }
}
