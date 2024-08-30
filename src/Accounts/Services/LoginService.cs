using Accounts.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Accounts.Configuration;
using Microsoft.Extensions.Options;

namespace Accounts.Services;

public class LoginService : ILoginService
{
    private readonly JwtSettingsOptions _jwtSettingsOptions;
    private readonly ILogger<LoginService> _logger;

    /// <summary>
    /// Base constructor
    /// </summary>
    public LoginService(
        IOptions<JwtSettingsOptions> jwtSettingsOptions, 
        ILogger<LoginService> logger)
    {
        _jwtSettingsOptions = jwtSettingsOptions.Value;
        _logger = logger;
    }

    public string GenerateJwtToken(string userName)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_jwtSettingsOptions.SecretKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, userName)
            }),
            Expires = DateTime.UtcNow.AddMinutes(_jwtSettingsOptions.ExpiresInMinutes),
            Issuer = _jwtSettingsOptions.Issuer,
            Audience = _jwtSettingsOptions.Audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        _logger.LogInformation("Token created");

        return tokenHandler.WriteToken(token);
    }
}