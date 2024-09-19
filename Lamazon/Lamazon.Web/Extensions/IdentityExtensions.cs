using Lamazon.Domain.Constants;
using System.Linq;
using System.Security.Claims;

namespace Lamazon.Web.Extensions
{
    public static class IdentityExtensions
    {
        public static bool IsAdmin(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.IsInRole(Roles.Admin);
        }

        public static string DisplayName(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value ?? string.Empty;
        }

        public static int GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            var primarySidValue = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.PrimarySid).Value;
            int userId;
            int.TryParse(primarySidValue, out userId);
            return userId;
        }
    }
}
