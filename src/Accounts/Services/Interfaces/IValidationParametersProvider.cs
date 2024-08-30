using Microsoft.IdentityModel.Tokens;

namespace Accounts.Services.Interfaces;

/// <summary>
/// The logic of getting validation parameters.
/// </summary>
public interface IValidationParametersProvider
{
    /// <summary>
    /// Get jwt-token parameters
    /// </summary>
    /// <returns>Provides configured <see cref="TokenValidationParameters"/></returns>
    Task<TokenValidationParameters> GetTokenParametersAsync();
}