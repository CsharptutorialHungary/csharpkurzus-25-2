using System.Text.Json.Serialization;

namespace RentalApp.Core.Models;

internal interface IEquipment : IRecord, IEquatable<IEquipment>, IComparable<IEquipment>
{
    string Barcode { get; init; }
    IReadOnlyList<Sport> Sports { get; init; }

    [JsonPropertyName("Equipment type")]
    EquipmentType EquipmentType { get; init; }
    string? Brand { get; init; }
    string? Description { get; init; }

}


