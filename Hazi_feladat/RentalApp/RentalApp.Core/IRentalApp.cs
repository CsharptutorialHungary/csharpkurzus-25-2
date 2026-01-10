

namespace RentalApp.Core;


public interface IRentalApp
{
    public string AddEquipment(string jsonString);
    public string DeleteEquipment(string barcode);
    public string UpdateEquipment(string jsonString);

    public string AddCustomer(string jsonString);
    public string DeleteCustomer(string barcode);
    public string UpdateCustomer(string jsonString);
}
