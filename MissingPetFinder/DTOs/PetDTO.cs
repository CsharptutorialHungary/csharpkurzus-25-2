using System.ComponentModel.DataAnnotations;

namespace MissingPetFinder.DTOs;

public record PetDTO
{
    [Required]
    [MaxLength(100)]
    public required string Name { get; set; }

    [Required]
    [MaxLength(80)]
    public required string Type { get; set; }

    [Required]
    [MaxLength(80)]
    public required string Breed { get; set; }

    [Required]
    [MaxLength(50)]
    public required string Color { get; set; }

    [Required]
    [MaxLength(200)]
    public required string LocationLastSeen { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public required DateTime DateLastSeen { get; set; }

    [Url]
    [MaxLength(300)]
    public string? ImageUrl { get; set; }
};