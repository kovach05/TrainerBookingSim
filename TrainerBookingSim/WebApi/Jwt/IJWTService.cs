using DataAccess.Models;

namespace WebApi.JWT;

public interface IJWTService
{
    string GenerateToken(User user);
}