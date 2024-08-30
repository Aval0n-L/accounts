using System.Text;
using Accounts.Configuration;
using Accounts.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Accounts.Services;

public class ValidationParametersProvider : IValidationParametersProvider
{
    private readonly JwtSettingsOptions _jwtSettingsOptions;

    /// <summary>
    /// Base constructor
    /// </summary>
    public ValidationParametersProvider(IOptions<JwtSettingsOptions> jwtSettingsOptions)
    {
        _jwtSettingsOptions = jwtSettingsOptions.Value;
    }

    public Task<TokenValidationParameters> GetTokenParametersAsync()
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettingsOptions.SecretKey)),

            ValidateIssuer = true,
            ValidIssuer = _jwtSettingsOptions.Issuer,

            ValidateAudience = true,
            ValidAudience = _jwtSettingsOptions.Audience,

            ClockSkew = TimeSpan.Zero,
            ValidateLifetime = true
        };

        return Task.FromResult(tokenValidationParameters);
    }
}