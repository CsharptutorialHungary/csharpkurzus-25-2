using System.Text.Json.Serialization;

namespace RentalApp.Core.Models;

internal class Equipment : IEquipment
{

    private readonly string _barcode = default!;
    public required string Barcode
    {
        get => _barcode;    
        init
        {
            if(string.IsNullOrEmpty(value))
            {
                throw new InvalidOperationException("Barcode cannot be null or empty.");
            }
            _barcode = value;
        }
    }

    private readonly IReadOnlyList<Sport> _sports = default!;
    public required IReadOnlyList<Sport> Sports
    {
        get => _sports;
        init
        {
            if (value is null || value.Count == 0)
            {
                throw new InvalidOperationException("Sports cannot be null or empty list.");
            }
            _sports = value;
        }
    }

    [JsonPropertyName("Equipment type")]
    public required EquipmentType EquipmentType { get; init; }
    public string? Brand {  get; init; }
    public string? Description { get; init; }

    [JsonIgnore]
    public string Identifier => _barcode;
    
    public int CompareTo(IEquipment? other)
    {
        if (other is null)
        {
            return 1;
        }
        return Barcode.CompareTo(other.Barcode);
    }

    public bool Equals(IEquipment? other)
    {
        return other is not null && Barcode == other.Barcode;
    }

    public override bool Equals(object? obj)
    {
        return obj is not null && obj is IEquipment equipment && Barcode == equipment.Barcode;
    }


    public override int GetHashCode()
    {
        return Barcode.GetHashCode();
    }
    

    public override string ToString()
    {
        return $"Barcode: {Barcode}, Sports: {(Sports is not null ? string.Join(", ", Sports) : "")}, EquipmentType: {EquipmentType}, Description: {Description}";
    }
}
