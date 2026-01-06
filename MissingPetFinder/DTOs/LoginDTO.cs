using System.ComponentModel.DataAnnotations;

namespace MissingPetFinder.DTOs;

public record LoginDTO
(
    [Required] string Email,
    [Required] string Password
);