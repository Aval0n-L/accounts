using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Accounts.Services.Interfaces;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace Accounts.Services;

/// <inheritdoc />
public class AuthenticationService : IAuthenticationService
{
    private readonly IValidationParametersProvider _validationParametersProvider;

    /// <summary>
    /// Base constructor
    /// </summary>
    public AuthenticationService(IValidationParametersProvider validationParametersProvider)
    {
        _validationParametersProvider = validationParametersProvider;
    }

    public async Task<ClaimsPrincipal> ValidateToken(string jwt)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenValidationParameters = await _validationParametersProvider.GetTokenParametersAsync().ConfigureAwait(false);
        try
        {
            var principal = tokenHandler.ValidateToken(jwt, tokenValidationParameters, out SecurityToken validatedToken);


            if (!(validatedToken is JwtSecurityToken jwtToken) ||
                !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }
        catch (Exception ex)
        {
            throw new SecurityTokenValidationException("Invalid token", ex);
        }
    }
}