using Accounts.Extensions;
using Accounts.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Accounts.Controllers;

/// <summary>
/// Controller for work with Authentication
/// </summary>
[Route("api/account")]
[Produces("application/json")]
public class AuthenticateController : Controller
{
    private readonly IAuthenticationService _authenticationService;
    private readonly ILogger<AuthenticateController> _logger;

    /// <summary>
    /// Base constructor
    /// </summary>
    public AuthenticateController(
        IAuthenticationService authenticationService,
        ILogger<AuthenticateController> logger)
    {
        _authenticationService = authenticationService;
        _logger = logger;
    }

    /// <summary>
    /// Authenticate request
    /// </summary>
    [HttpGet]
    [Route("authenticate")]
    public async Task<IActionResult> Authenticate()
    {
        try
        {
            var jwtToken = Request.Headers.GetJwtToken();
            var tenantToken = Request.Headers.GetEwbTenant();

            if (!string.IsNullOrEmpty(jwtToken))
            {
                var principal = await _authenticationService.ValidateToken(jwtToken).ConfigureAwait(false);
                return Ok(new { message = "Token is valid", userName = principal.Identity?.Name });
            }
            else if (!string.IsNullOrEmpty(tenantToken))
            {
                return Ok(new { message = "Tenant Token is valid" });
            }

        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
        }

        return Unauthorized(new { message = "No valid token provided" });
    }

    /// <summary>
    /// User registration
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    //[HttpPost]
    //[Route("register")]
    //public IActionResult Register([FromBody] RegisterModel model)
    //{
    //    // Логика для создания пользователя
    //    // Например, проверка существования пользователя, хеширование пароля и сохранение в БД

    //    return Ok(new { message = "User registered successfully" });
    //}
}