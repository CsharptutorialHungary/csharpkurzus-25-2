using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using MissingPetFinder.Data;
using MissingPetFinder.DTOs;
using MissingPetFinder.Models;
using MissingPetFinder.Services.Interfaces;

namespace MissingPetFinder.Services.Implementations;

public class AuthService(AppDbContext context) : IAuthService
{
    public async Task<User?> LoginAsync(LoginDTO loginDto)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);
        if (user == null)
            throw new UnauthorizedAccessException("Invalid credentials.");

        var parts = user.PasswordHash.Split(':');
        var salt = Convert.FromBase64String(parts[0]);
        var storedHash = Convert.FromBase64String(parts[1]);

        var hash = HashPassword(loginDto.Password, salt);

        if (!hash.SequenceEqual(storedHash))
            throw new UnauthorizedAccessException("Invalid credentials.");

        return user;
    }

    public async Task<User?> RegisterAsync(RegisterDTO registerDto)
    {
        var existingUser = await context.Users.FirstOrDefaultAsync(u => u.Email == registerDto.Email);
        if (existingUser != null)
            throw new InvalidOperationException("User with this email already exists.");

        var salt = RandomNumberGenerator.GetBytes(10);
        var hash = HashPassword(registerDto.Password, salt);

        var user = new User
        {
            Username = registerDto.Username,
            Email = registerDto.Email,
            PasswordHash = $"{Convert.ToBase64String(salt)}:{Convert.ToBase64String(hash)}"
        };

        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();

        return user;
    }

    private static byte[] HashPassword(string password, byte[] salt)
    {
        return KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 32
        );
    }
}