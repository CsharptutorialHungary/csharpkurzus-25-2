using System.Text.Json.Serialization;

namespace RentalApp.Core.Models;

internal class Reservation : IReservation
{

    private string _reservationID;

    public void Autoincrement(string reservationID) => _reservationID = reservationID;

    public string Identifier => _reservationID;

    public string GetForeignIdentifier<ICustomer>() => CustomerEmail;

    public IEnumerable<string> GetForeignIdentifiers<TRecord>() => Barcodes;

    [JsonPropertyName("Reservation ID")]
    public string ReservationID{ get => _reservationID; init => _reservationID = value; }

    [JsonPropertyName("Customer email")]
    public required string CustomerEmail { get; init; }

    [JsonPropertyName("Reservation period")]
    public required (DateOnly start, DateOnly end) ReservationPeriod { get; init; }

    [JsonIgnore]
    public int LengthInDays => ReservationPeriod.end.DayNumber - ReservationPeriod.end.DayNumber;

    public required IReadOnlyList<string> Barcodes { get; init; }
    
}
