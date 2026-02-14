using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Hazi_feladat
{
    internal class DatabaseHandler
    {
        private FileHandler FileManager;
        private List<Movie>? Movies;


        public DatabaseHandler() 
        {
           FileManager = new FileHandler();
            Movies = FileManager.LoadMovies();
        }

        public List<Movie>? getMovies()
        {
            return this.Movies;
        }
      

        public void AddMovie()
        {
            try
            {
                
                string Title = "";

                while (string.IsNullOrEmpty(Title))
                {
                    Console.Write("Cím (kötelező): ");
                    Title = Console.ReadLine();
                }               

                Console.Write("Rendező (opcionális): ");
                string? Director = Console.ReadLine();

                Console.Write("Műfaj (opcionális): ");
                string? Genre = Console.ReadLine();

                Console.Write("Év (opcionális): ");
                string? yearInput = Console.ReadLine();

                int? Year = null;
                if (int.TryParse(yearInput, out int parsedYear))
                {
                    Year = parsedYear;
                }

                Console.Write("Értékelés (0-10, Opcionális) : ");
                string? RatingInput = Console.ReadLine();

                double? Rating = null;
                if (double.TryParse(RatingInput, out double parsedRating))
                {
                    Rating = parsedRating;
                }


                Movies.Add(new Movie(Title, Director, Year, Genre, Rating));
                FileHandler.SaveMoviesToJson(Movies);
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Hibás adat: {ex.Message}");
            }
        }
        public void ListMovies()
        {
            if (Movies.Count == 0)
            {
                Console.WriteLine("Nincs egyetlen film sem az adatbázisban.");
                return;
            }

            foreach (Movie Movie in Movies)
            {
                Console.WriteLine($"{Movie.Title} ({Movie.Year}) - {Movie.Genre}, rendező: {Movie.Director}, értékelés: {Movie.Rating}");
            }
        }


    }
}
