using System.ComponentModel.DataAnnotations;

namespace MissingPetFinder.DTOs;

using System.ComponentModel.DataAnnotations;

public record RegisterDTO
{
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public required string Username { get; set; }

    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,}$",
        ErrorMessage = "Password must be at least 6 chars, include upper, lower, number")]
    public required string Password { get; set; }

    [Required]
    [Compare("Password", ErrorMessage = "Passwords don't match")]
    public required string ConfirmPassword { get; set; }
}
