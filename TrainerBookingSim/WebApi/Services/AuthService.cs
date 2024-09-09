using AutoMapper;
using BusinessLogic.Interface;
using DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using WebApi.DTOs;
using WebApi.JWT;

namespace WebApi.Services;

public class AuthService : IAuthService
{   
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IJWTService _jwtService;

    public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, IJWTService jwtService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtService = jwtService;
    }
    
    public async Task<string> RegisterAsync(RegisterRequest request)
    {
        if (string.IsNullOrEmpty(request.FirstName))
            return "First name is required";
    
        if (string.IsNullOrEmpty(request.LastName))
            return "Last name is required";
    
        if (await _userManager.FindByNameAsync(request.Username) != null)
            return "Username already exists";
        
        var user = new User
        {
            UserName = request.Username,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
            return string.Join(", ", result.Errors.Select(e => e.Description));

        if (!string.IsNullOrEmpty(request.RoleName))
        {
            await _userManager.AddToRoleAsync(user, request.RoleName);
        }

        return "User registered successfully";
    }


    public async Task<string> LoginAsync(LoginRequest request)
    {
        var user = await _userManager.FindByNameAsync(request.Username);

        if (user == null)
            return "Invalid credentials";

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

        if (!result.Succeeded)
            return "Invalid credentials";

        var token = _jwtService.GenerateToken(user);

        return token;
    }
}