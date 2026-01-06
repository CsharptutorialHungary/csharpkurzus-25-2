using System.ComponentModel.DataAnnotations;

namespace MissingPetFinder.DTOs;

public class UpdatePetDTO
{
    [MaxLength(100)]
    public string? Name { get; set; }

    [MaxLength(80)]
    public string? Type { get; set; }

    [MaxLength(80)]
    public string? Breed { get; set; }

    [MaxLength(50)]
    public string? Color { get; set; }
    [MaxLength(200)]
    public string? LocationLastSeen { get; set; }

    [DataType(DataType.Date)]
    public DateTime? DateLastSeen { get; set; }

    [Url]
    [MaxLength(300)]
    public string? ImageUrl { get; set; }
}