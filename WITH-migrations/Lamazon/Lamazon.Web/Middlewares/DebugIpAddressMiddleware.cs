using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Lamazon.Web.Middlewares
{
    public class DebugIpAddressMiddleware
    {
        private readonly RequestDelegate _next;
        private static readonly HttpClient _httpClient = new HttpClient();

        public DebugIpAddressMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var publicIp = await GetPublicIPAddressAsync();
            if (publicIp != null)
            {
                httpContext.Connection.RemoteIpAddress = publicIp;
            }

            await _next(httpContext);
        }

        private async Task<IPAddress?> GetPublicIPAddressAsync()
        {
            try
            {
                var response = await _httpClient.GetStringAsync("http://checkip.dyndns.org/");

                int first = response.IndexOf("Address: ") + 9;
                int last = response.LastIndexOf("</body>");
                string address = response.Substring(first, last - first);

                return IPAddress.Parse(address);
            }
            catch
            {
                return null;
            }
        }
    }

}
