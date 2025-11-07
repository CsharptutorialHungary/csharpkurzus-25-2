using MovieBox.Core.Filter;
using MovieBox.Core.Records;

namespace MovieBox.Ui
{
    /// <summary>
    /// Defines a service for handling all console user interface operations.
    /// This keeps all Console.Write/Read calls separate from the application logic.
    /// </summary>
    public interface IConsoleUIService
    {
        /// <summary>
        /// Clears the console screen.
        /// </summary>
        void Clear();

        /// <summary>
        /// Displays the main menu and gets the user's validated choice.
        /// </summary>
        /// <returns>A string representing the user's menu choice.</returns>
        string GetMainMenuChoice();

        /// <summary>
        /// Displays a formatted table of movies.
        /// </summary>
        void DisplayMovies(IEnumerable<Movie> movies);

        /// <summary>
        /// Displays the movie collection statistics.
        /// </summary>
        void DisplayStats(MovieCollectionStats stats);

        /// <summary>
        /// Prompts the user to enter data for a new movie.
        /// </summary>
        /// <returns>A new Movie record populated with user data.</returns>
        Movie GetNewMovieData();

        /// <summary>
        /// Prompts the user to enter filter criteria.
        /// </summary>
        /// <returns>A MovieFilterCriteria object populated with user data.</returns>
        MovieFilterCriteria GetSearchCriteria();

        /// <summary>
        /// Displays a numbered list of movies and prompts user to select one to delete.
        /// </summary>
        /// <param name="movies">The collection of movies to display.</param>
        /// <returns>The zero-based index of the selected movie, or -1 if cancelled.</returns>
        int GetMovieSelectionForDeletion(IEnumerable<Movie> movies);

        /// <summary>
        /// Displays a message to the user (e.g., success, error, info).
        /// </summary>
        /// <param name="message">The text to display.</param>
        /// <param name="type">The type of message (Info, Success, Error).</param>
        void ShowMessage(string message, MessageType type = MessageType.Info);

        /// <summary>
        /// Pauses the application and waits for the user to press a key.
        /// </summary>
        void WaitForUser();
    }
}