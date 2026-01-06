using MissingPetFinder.DTOs;
using MissingPetFinder.Models;

namespace MissingPetFinder.Services.Interfaces;

public interface IAuthService
{
    Task<User?> RegisterAsync(RegisterDTO registerDto);
    Task<User?> LoginAsync(LoginDTO loginDto);
}