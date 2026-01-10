namespace RentalApp.Core.Models;

internal interface IReservation : IRecord, INeedsAutoincrement, IHasForeignIdentifier<ICustomer>, IHasForeignIdentifiers<IEquipment>
{
    string ReservationID { get; init; }
    string CustomerEmail { get; init; }
    (DateOnly start, DateOnly end) ReservationPeriod {  get; init; }
    int LengthInDays { get; }
    IReadOnlyList<string> Barcodes { get; init; }
}
