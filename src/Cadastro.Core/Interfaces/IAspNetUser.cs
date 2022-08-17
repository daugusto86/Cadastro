using System.Security.Claims;

namespace Cadastro.Core.Interfaces
{
    public interface IAspNetUser
    {
        string Name { get; }
        Guid GetUserId();
        string GetUserEmail();
        bool IsAuthenticated();
        bool IsinRole(string role);
        IEnumerable<Claim> GetClaimsIdentity();
    }
}
