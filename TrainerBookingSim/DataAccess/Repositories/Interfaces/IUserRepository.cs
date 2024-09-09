using DataAccess.Models;

namespace DataAccess.Repositories.Interfaces;

public interface IUserRepository
{
    Task<User> GetByUsernameAsync(string username);
    Task<bool> UsernameExistsAsync(string username);
    Task<User> CreateUserAsync(User user, string password);
    Task AddUserToRoleAsync(User user, string roleName);
}