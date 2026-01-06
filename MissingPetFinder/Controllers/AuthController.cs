using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MissingPetFinder.DTOs;
using MissingPetFinder.Models;
using MissingPetFinder.Services.Interfaces;

namespace MissingPetFinder.Controllers;

[Route("Auth")]
public class AuthController(IAuthService authService) : Controller
{
    [HttpGet("Register")]
    public IActionResult Register() => View();

    [HttpPost("Register")]
    public async Task<IActionResult> Register(RegisterDTO registerDto)
    {
        if (!ModelState.IsValid)
            return View(registerDto);

        try
        {
            var user = await authService.RegisterAsync(registerDto);
            await SignInUser(user!);
            return RedirectToAction("Index", "Pets");
        }
        catch (InvalidOperationException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(registerDto);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, "Unexpected error occurred. Please try again later.");
            return View(registerDto);
        }
    }

    [HttpGet("Login")]
    public IActionResult Login() => View();

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginDTO loginDto)
    {
        if (!ModelState.IsValid)
            return View(loginDto);

        try
        {
            var user = await authService.LoginAsync(loginDto);
            await SignInUser(user!);
            return RedirectToAction("Index", "Pets");
        }
        catch (InvalidOperationException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(loginDto);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, "Unexpected error occurred. Please try again later.");
            return View(loginDto);
        }
    }

    [HttpPost("Logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Pets");
    }

    private async Task SignInUser(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username)
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
    }
}