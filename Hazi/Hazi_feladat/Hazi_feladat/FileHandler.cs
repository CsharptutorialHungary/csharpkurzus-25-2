using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Hazi_feladat
{
    internal class FileHandler
    {
        private const string FileName = "movies.json";

        public List<Movie> LoadMovies()
        {
            try
            {
                string json = File.ReadAllText(FileName);

                List<Movie>? Movies = JsonSerializer.Deserialize<List<Movie>>(json);

                if (Movies is not null)
                {
                    Console.WriteLine("Film adatbázis betöltve:");
                    Console.WriteLine($"Összes film: {Movies.Count}");
                }
                else
                {
                    Console.WriteLine("A JSON fájl üres vagy hibás formátumú.");
                    Movies = new List<Movie>();
                }
                return Movies;
            }
            catch (FileNotFoundException ex)
            {               
                Console.WriteLine("Hiba történt a beolvasás közben:");
                Console.WriteLine(ex.Message);
                return new List<Movie>();
            }
        }

        public static void SaveMoviesToJson(List<Movie> movies)
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };

                string json = JsonSerializer.Serialize(movies, options);
                File.WriteAllText(FileName, json);

            }
            catch (FileNotFoundException ex)
            {
            }
        }

    }
}
