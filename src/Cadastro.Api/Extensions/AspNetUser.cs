using Cadastro.Core.Interfaces;
using System.Security.Claims;

namespace Cadastro.Api.Extensions
{
    public class AspNetUser : IAspNetUser
    {
        private readonly IHttpContextAccessor acessor;
        public string Name => acessor.HttpContext.User.Identity.Name;

        public AspNetUser(IHttpContextAccessor acessor)
        {
            this.acessor = acessor;
        }

        public Guid GetUserId()
        {
            return IsAuthenticated() ? Guid.Parse(acessor.HttpContext.User.GetUserId()) : Guid.Empty;
        }

        public string GetUserEmail()
        {
            return IsAuthenticated() ? acessor.HttpContext.User.GetUserEmail() : "";
        }

        public bool IsAuthenticated()
        {
            return acessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public bool IsinRole(string role)
        {
            return acessor.HttpContext.User.IsInRole(role);
        }

        public IEnumerable<Claim> GetClaimsIdentity()
        {
            return acessor.HttpContext.User.Claims;
        }
    }

    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentException(nameof(principal));
            }

            var claim = principal.FindFirst(ClaimTypes.NameIdentifier);
            return claim?.Value;
        }

        public static string GetUserEmail(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentException(nameof(principal));
            }

            var claim = principal.FindFirst(ClaimTypes.Email);
            return claim?.Value;
        }
    }
}
