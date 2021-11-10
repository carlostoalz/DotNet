using BE;
using Domain;
using Infraestructure;

namespace Extensions
{
    internal static class InjectorExtension
    {
        internal static void AddDepndencys(this IServiceCollection services)
        {
            services.AddSingleton<GlobalAppSettings>();

            #region Domain
            services.AddScoped<IUtilBL, UtilBL>();
            #endregion

            #region Ingraestructure
            services.AddScoped<IUtilRepository, UtilRepository>();
            #endregion
        }
    }
}
