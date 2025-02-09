using Microsoft.Net.Http.Headers;

namespace Accounts.Extensions;

public static class AuthExtensions
{
    public static string? GetJwtToken(this IHeaderDictionary? headers)
    {
        if (headers == null) return null;
        
        var str = headers[HeaderNames.Authorization].ToString();
        return !string.IsNullOrEmpty(str) && str.StartsWith("Bearer", StringComparison.InvariantCulture) ? str.Remove(0, "Bearer".Length).TrimStart() : null;
    }

    public static string? GetTenant(this IHeaderDictionary headers)
    {
        return headers["Tenant-Token"];
    }
}