using Hazi_feladat;
using System.Text.Json;

internal class Program
{
    private static void Main(string[] args)
    {
        DatabaseHandler databaseHandler = new DatabaseHandler();
        QueryHandler queryHandler= new QueryHandler(databaseHandler.getMovies());

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("--- Film Adatbázis ---");
            Console.WriteLine("");
            Console.WriteLine("1. Film hozzáadása");
            Console.WriteLine("2. Összes film listázása");
            Console.WriteLine("3. Adatbázis szűrése");
            Console.WriteLine("0. Kilépés");
            Console.Write("Választás: ");

            string input = Console.ReadLine() ?? "";
            Console.WriteLine();

            switch (input)
            {
                case "1": databaseHandler.AddMovie(); break;
                case "2": databaseHandler.ListMovies(); break;
                case "3": queryHandler.QueryMenu(); break;
                case "0": return;
                default: Console.WriteLine("Érvénytelen választás."); break;
            }
        }

    }

}