using Lamazon.ViewModels.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Lamazon.Web.Helpers
{
    public static class AuthHelper
    {
        public static async Task SignInUser(UserViewModel userViewModel, HttpContext httpContext)
        {
            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.NameIdentifier, userViewModel.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Email, userViewModel.Email));
            claims.Add(new Claim(ClaimTypes.Name, userViewModel.FullName));
            claims.Add(new Claim(ClaimTypes.Role, userViewModel.Role.Key));
            claims.Add(new Claim(ClaimTypes.PrimarySid, userViewModel.Id.ToString()));

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                new AuthenticationProperties
                {
                    IsPersistent = true
                });
        }

        public static async Task SignoutUser(HttpContext httpContext)
        {
            await httpContext.SignOutAsync();
        }
    }
}
