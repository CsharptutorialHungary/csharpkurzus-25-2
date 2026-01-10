
using RentalApp.Core.Models;
using RentalApp.Core.Services;

namespace RentalApp.Core;

public class RentalAppFactory
{

    public static IRentalApp Create(string equipmentDBFile, string customerDBFile, string reservationDBFile)
    {
        IJsonParseManager jsonParseManager = new JsonParseManager();
        IDatabaseManager<IEquipment> equipmentDBManager = new DatabaseManager<IEquipment>(equipmentDBFile, jsonParseManager);
        IDatabaseManager<ICustomer> customerDBManager = new DatabaseManager<ICustomer>(customerDBFile, jsonParseManager);
        IDatabaseManager<IReservation> reservationDBManager = new DatabaseManager<IReservation>(reservationDBFile, jsonParseManager);

        return new RentalApp(jsonParseManager, equipmentDBManager, customerDBManager, reservationDBManager);
    }

}
