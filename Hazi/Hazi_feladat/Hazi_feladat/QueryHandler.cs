using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hazi_feladat
{
    internal class QueryHandler
    {
        private List<Movie> Movies;
        public QueryHandler(List<Movie> movies)
        {
            Movies = movies;
        }

        public void QueryMenu()
        {
            Console.WriteLine("--- Adatbázis szűrése ---");
            Console.WriteLine("1. Keresés cím alapján");
            Console.WriteLine("2. Szűrés értékelés alapján");
            Console.WriteLine("3. Rendezés év szerint");
            Console.WriteLine("4. Rendezés műfaj szerint");
            Console.WriteLine("5. Rendezés rendező szerint");
            Console.WriteLine("6. Legnagyobb értékelésű filmek");
            Console.WriteLine("0. Vissza a főmenübe");

            Console.Write("Választás: ");
            string input = Console.ReadLine() ?? "";
            Console.WriteLine();

            switch (input)
            {
                case "1": SearchByTitle(); break;
                case "2": FilterByRating(); break;
                case "3": SortByYear(); break;
                case "4": SortByGenre(); break;
                case "5": SortByDirector(); break;
                case "6": TopRatedMovies(); break;
                case "0": return;
                default: Console.WriteLine("Érvénytelen választás."); break;
            }
        }

       

        public void SearchByTitle()
        {
            Console.Write("Cím:");
            string title = Console.ReadLine() ?? "";

            var results = Movies
                ?.Where(m => m.Title != null && m.Title.Contains(title, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (results == null || results.Count == 0)
            {
                Console.WriteLine("Nincs találat.");
                return;
            }

            foreach (var movie in results)
            {
                Console.WriteLine(movie.Title);
            }
            Console.WriteLine("");
            Console.WriteLine("------------------");
        }


        public void FilterByRating()
        {
            Console.Write("Minimum értékelés: ");
            double minRating = double.TryParse(Console.ReadLine(), out double parsed) ? parsed : 5.0;
            var results = Movies
                ?.Where(m => m.Rating.HasValue && m.Rating >= minRating)
                .ToList();

            if (results == null || results.Count == 0)
            {
                Console.WriteLine("Nincs ilyen film.");
                return;
            }

            foreach (var movie in results)
            {
                Console.WriteLine($"{movie.Title}: {movie.Rating}");
            }
        }

        private void SortByDirector()
        {
            var sorted = Movies?
                .OrderBy(m => m.Director)
                .ToList();

            if (sorted == null) return;

            foreach (var movie in sorted)
            {
                Console.WriteLine($"{movie.Director}: {movie.Title}");
            }
        }

        public void SortByYear()
        {
            var sorted = Movies?
                .OrderBy(m => m.Year ?? int.MaxValue)
                .ToList();

            if (sorted == null) return;

            foreach (var movie in sorted)
            {
                Console.WriteLine($"{movie.Year}: {movie.Title}");
            }
        }

        public void SortByGenre()
        {
            var sorted = Movies?
                .OrderBy(m => m.Genre)
                .ToList();

            if (sorted == null) return;

            foreach (var movie in sorted)
            {
                Console.WriteLine($"{movie.Genre}: {movie.Title}");
            }
        }
        public void TopRatedMovies()
        {
            Console.Write("Listázandó filmek száma: ");
            int count = int.TryParse(Console.ReadLine(), out int parsed) ? parsed : 10;

            var topMovies = Movies
                .Where(m => m.Rating.HasValue)
                .OrderByDescending(m => m.Rating)   
                .Take(count)                    
                .ToList();
    

            foreach (var movie in topMovies)
            {
                Console.WriteLine($"{movie.Rating}, {movie.Title}");
            }
        }



    }
}
