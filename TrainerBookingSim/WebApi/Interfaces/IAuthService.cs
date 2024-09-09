using WebApi.DTOs;

namespace BusinessLogic.Interface;

public interface IAuthService
{
    Task<string> RegisterAsync(RegisterRequest request);
    Task<string> LoginAsync(LoginRequest request);
}