using Accounts.Models;
using Accounts.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Accounts.Controllers;

/// <summary>
/// Controller for work with Login
/// </summary>
[Route("api/account")]
[Produces("application/json")]
public class LoginController : Controller
{
    private readonly ILoginService _loginService;
    private readonly ILogger<LoginController> _logger;

    /// <summary>
    /// Base constructor
    /// </summary>
    public LoginController(
        ILoginService loginService, 
        ILogger<LoginController> logger)
    {
        _loginService = loginService;
        _logger = logger;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginModel model)
    {
        string token;
        try
        {
            token = _loginService.GenerateJwtToken(model.UserName);

            return Content(token);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception.Message, exception);

            throw new Exception(exception.Message);
        }
    }

    //[HttpPost("logout")]
    //public IActionResult Logout()
    //{
    //    var jwtToken = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

    //    if (string.IsNullOrEmpty(jwtToken))
    //    {
    //        return Unauthorized(new { message = "No valid token provided" });
    //    }

    //    // Логика деактивации токена или логирования выхода пользователя
    //    return Ok(new { message = "User logged out successfully" });
    //}

}