

using RentalApp.Core;


// TODO Fix:
const string basePath = @"C:\Users\xloranthus\source\repos\csharpkurzus-25-2\Hazi_feladat\RentalApp\RentalApp";
// const string basePath = @"C:\Users\wivie\source\repos\csharpkurzus-25-2\Hazi_feladat\RentalApp\RentalApp";
string equipmentDBFile = Path.Combine(basePath, "equipmentDB.json");
string customerDBFile = Path.Combine(basePath, "customerDB.json");
string reservationDBFile = Path.Combine(basePath, "reservationDB.json");

IRentalApp app = RentalAppFactory.Create(equipmentDBFile, customerDBFile, reservationDBFile);


string? cmd;
Console.Write("$>");

while (string.IsNullOrEmpty(cmd = Console.ReadLine()) is false)
{

    string[] cmds = cmd.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
    if(cmds.Length != 2)
    {
        Console.WriteLine("Missing parameter.");
        Console.Write("$>");
        continue;
    }

    string cmdType = cmds[0];
    string cmdParam = cmds[1];

    string jsonString = default!;
    if(cmdType.StartsWith("add") || cmdType.StartsWith("upd"))
    {
        if (File.Exists(cmdParam) is false)
        {
            Console.WriteLine($"{cmdParam} does not exist.");
            Console.Write("$>");
            continue;
        }
        jsonString = File.ReadAllText(cmdParam);
    }


    switch (cmdType)
    {
        case "addeq":
            Console.WriteLine(app.AddEquipment(jsonString));
            break;
        case "updeq":
            Console.WriteLine(app.UpdateEquipment(jsonString));
            break;
        case "deleq":
            Console.WriteLine(app.DeleteEquipment(cmdParam));
            break;
        case "addcus":
            Console.WriteLine(app.AddCustomer(jsonString));
            break;
        case "updcus":
            Console.WriteLine(app.UpdateCustomer(jsonString));
            break;
        case "delcus":
            Console.WriteLine(app.DeleteCustomer(cmdParam));
            break;
        default:
            Console.WriteLine("Unknown command.");
            break;
    }

    Console.Write("$>");
}

