namespace MissingPetFinder.Models;

public class Pet
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Type { get; set; }
    public required string Breed { get; set; }
    public required string Color { get; set; }
    public required string LocationLastSeen { get; set; }
    public required DateTime DateLastSeen { get; set; }
    public DateTime DateReported { get; set; } = DateTime.UtcNow;
    public bool IsFound { get; set; } = false;
    public string? ImageUrl { get; set; }
    public required string ContactEmail { get; set; }

    public int UserId { get; set; }
    public User? User { get; set; }
}