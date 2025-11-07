using MovieBox.Core.Records;
using MovieBox.Core.Service;
using MovieBox.Ui;

namespace MovieBox
{
    public class MovieAppController(IMovieService movieService, IConsoleUIService ui)
    {
        /// <summary>
        /// Starts and runs the main application loop.
        /// </summary>
        public async Task Run()
        {
            await movieService.InitializeAsync();
            if (movieService.GetAllMovies().Count() == 0)
                AddInitialData();

            bool keepRunning = true;
            while (keepRunning)
            {
                ui.Clear();
                string choice = ui.GetMainMenuChoice();
                ui.Clear();

                switch (choice)
                {
                    case "1":
                        ListAllMovies();
                        break;

                    case "2":
                        AddNewMovie();
                        break;

                    case "3":
                        SearchForMovies();
                        break;

                    case "4":
                        ShowStatistics();
                        break;

                    case "5":
                        DeleteMovie();
                        break;

                    case "6":
                        keepRunning = false;
                        break;

                    default:
                        ui.ShowMessage("Invalid option, please try again.", MessageType.Error);
                        break;
                }

                if (keepRunning)
                {
                    ui.WaitForUser();
                }
            }

            await movieService.SaveAsync();
        }

        // --- Menu Action Methods ---

        private void ListAllMovies()
        {
            ui.ShowMessage("--- All Movies ---", MessageType.Title);
            var movies = movieService.GetAllMovies();
            ui.DisplayMovies(movies);
        }

        private void AddNewMovie()
        {
            try
            {
                var movie = ui.GetNewMovieData();
                movieService.AddMovie(movie);
                ui.ShowMessage("\nMovie added successfully!", MessageType.Success);
                ui.ShowMessage($"You added: {movie.Title} ({movie.ReleaseYear})");
            }
            catch (Exception ex)
            {
                ui.ShowMessage($"\nFailed to add movie: {ex.Message}", MessageType.Error);
            }
        }

        private void SearchForMovies()
        {
            try
            {
                var criteria = ui.GetSearchCriteria();
                var movies = movieService.SearchMovies(criteria);
                ui.ShowMessage("\n--- Search Results ---", MessageType.Title);
                ui.DisplayMovies(movies);
            }
            catch (Exception ex)
            {
                ui.ShowMessage($"\nFailed to search: {ex.Message}", MessageType.Error);
            }
        }

        private void ShowStatistics()
        {
            var stats = movieService.GetMovieCollectionStats();
            ui.DisplayStats(stats);
        }

        private void DeleteMovie()
        {
            try
            {
                var movies = movieService.GetAllMovies();
                if (!movies.Any())
                {
                    ui.ShowMessage("No movies available to delete.", MessageType.Error);
                    return;
                }

                int selectedIndex = ui.GetMovieSelectionForDeletion(movies);

                if (selectedIndex == -1)
                {
                    return;
                }

                if (movieService.DeleteMovie(selectedIndex))
                {
                    ui.ShowMessage("\nMovie deleted successfully!", MessageType.Success);
                }
                else
                {
                    ui.ShowMessage("\nFailed to delete movie.", MessageType.Error);
                }
            }
            catch (Exception ex)
            {
                ui.ShowMessage($"\nFailed to delete movie: {ex.Message}", MessageType.Error);
            }
        }

        private void AddInitialData()
        {
            movieService.AddMovie(new Movie("The Shawshank Redemption", "Frank Darabont", 1994, 9.3));
            movieService.AddMovie(new Movie("The Godfather", "Francis Ford Coppola", 1972, 9.2));
            movieService.AddMovie(new Movie("The Dark Knight", "Christopher Nolan", 2008, 9.0));
            movieService.AddMovie(new Movie("Pulp Fiction", "Quentin Tarantino", 1994, 8.9));
            movieService.AddMovie(new Movie("Forrest Gump", "Robert Zemeckis", 1994, 8.8));
            movieService.AddMovie(new Movie("Inception", "Christopher Nolan", 2010, 8.8));
        }
    }
}