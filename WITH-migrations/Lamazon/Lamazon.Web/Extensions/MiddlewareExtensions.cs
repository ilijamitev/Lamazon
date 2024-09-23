using Lamazon.Web.Middlewares;

namespace Lamazon.Web.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseDebugIpAddressMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<DebugIpAddressMiddleware>();
        }
    }
}
