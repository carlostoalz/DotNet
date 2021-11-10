using Middlewares;

namespace Extensions
{
    internal static class MiddlewareExtension
    {
        internal static IApplicationBuilder UseUserMiddlewares(this IApplicationBuilder app)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));

            app.UseMiddleware<ErrorMiddleware>();
            return app;
        }
    }
}
