using System.Security.Claims;

namespace Accounts.Services.Interfaces;

public interface IAuthenticationService
{
    Task<ClaimsPrincipal> ValidateToken(string jwt);
}