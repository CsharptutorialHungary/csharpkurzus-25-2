using RoadMaintenanceApp.Data;
using RoadMaintenanceApp.Models;
using RoadMaintenanceApp.Services;

class Program
{
    static void Main(string[] args)
    {
        using var context = new AppDbContext();
        var service = new RoadService(context);

        while (true)
        {
            Console.WriteLine("\n--- Út Karbantartó Rendszer ---");
            Console.WriteLine("1. Új út felvétele");
            Console.WriteLine("2. Út módosítása");
            Console.WriteLine("3. Út törlése");
            Console.WriteLine("4. Keresés és Listázás (JSON Exporttal)");
            Console.WriteLine("5. Kilépés");
            Console.Write("Válasszon: ");

            var choice = Console.ReadLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        AddNewRoad(service);
                        break;
                    case "2":
                        ModifyRoad(service);
                        break;
                    case "3":
                        DeleteRoad(service);
                        break;
                    case "4":
                        SearchRoads(service);
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Érvénytelen választás.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"HIBA TÖRTÉNT: {ex.Message}");
                Console.ResetColor();
            }
        }

        static void AddNewRoad(RoadService service)
        {
            Console.Write("ID: ");
            string id = Console.ReadLine()!;

            var condition = ReadCondition();

            Console.Write("Hossz (km): ");
            int length = int.Parse(Console.ReadLine()!);

            Console.Write("Forgalom (0-10): ");
            int traffic = int.Parse(Console.ReadLine()!);

            service.AddRoad(id, condition, length, traffic);
            Console.WriteLine("Sikeres felvétel!");
        }

        static void ModifyRoad(RoadService service)
        {
            Console.Write("Módosítandó út ID: ");
            string id = Console.ReadLine()!;

            Console.WriteLine("Adja meg az új állapot adatokat:");
            var condition = ReadCondition();

            Console.Write("Új Hossz (km): ");
            int length = int.Parse(Console.ReadLine()!);

            Console.Write("Új Forgalom: ");
            int traffic = int.Parse(Console.ReadLine()!);

            service.UpdateRoad(id, condition, length, traffic);
            Console.WriteLine("Sikeres módosítás!");
        }

        static void DeleteRoad(RoadService service)
        {
            Console.Write("Törlendő ID: ");
            string id = Console.ReadLine()!;
            service.DeleteRoad(id);
            Console.WriteLine("Sikeres törlés!");
        }

        static void SearchRoads(RoadService service)
        {
            Console.WriteLine("Hagyja üresen, ami nem számít a keresésben.");
            Console.Write("ID töredék: ");
            string? id = Console.ReadLine();

            Console.Write("Forgalmi szint: ");
            string? traficStr = Console.ReadLine();
            int? trafic = string.IsNullOrEmpty(traficStr) ? null : int.Parse(traficStr);

            Console.Write("Hossz (Km): ");
            string? lengthStr = Console.ReadLine();
            int? length = string.IsNullOrEmpty(lengthStr) ? null : int.Parse(lengthStr);

            Console.Write("Jó állapot % (pontos egyezés): ");
            string? goodStr = Console.ReadLine();
            int? good = string.IsNullOrEmpty(goodStr) ? null : int.Parse(goodStr);

            Console.Write("Közepes állapot % (pontos egyezés): ");
            string? midStr = Console.ReadLine();
            int? mid = string.IsNullOrEmpty(midStr) ? null : int.Parse(midStr);

            Console.Write("Rossz állapot % (pontos egyezés): ");
            string? badStr = Console.ReadLine();
            int? bad = string.IsNullOrEmpty(badStr) ? null : int.Parse(badStr);

            var results = service.Search(id, trafic, length, good, mid, bad);

            Console.WriteLine("\n--- Eredmények ---");
            foreach (var r in results)
            {
                Console.WriteLine($"ID: {r.Id} | Good: {r.GoodConditionPercent}% | Mid: {r.MidConditionPercent}% | Bad: {r.BadConditionPercent}% | Len: {r.LengthKm} | Traf: {r.TraficLevel}");
            }

            Console.Write("\nSzeretné kimenteni az ID-kat JSON-be? (i/N): ");
            if (Console.ReadLine()?.ToLower() == "i")
            {
                service.ExportIdsToJson(results, "export.json");
                Console.WriteLine("Mentve az 'export.json' fájlba. ");
            }
        }


        static RoadCondition ReadCondition()
        {
            Console.Write("Jó állapot %: ");
            int good = int.Parse(Console.ReadLine()!);

            Console.Write("Közepes állapot %: ");
            int mid = int.Parse(Console.ReadLine()!);

            Console.Write("Rossz állapot %: ");
            int bad = int.Parse(Console.ReadLine()!);

            return new RoadCondition(good, mid, bad);
        }
    }
}