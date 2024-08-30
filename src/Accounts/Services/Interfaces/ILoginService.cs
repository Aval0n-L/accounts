namespace Accounts.Services.Interfaces;

public interface ILoginService
{
    string GenerateJwtToken(string userName);
}