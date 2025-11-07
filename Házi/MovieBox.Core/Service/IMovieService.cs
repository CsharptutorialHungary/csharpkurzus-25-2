using MovieBox.Core.Filter;
using MovieBox.Core.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieBox.Core.Service
{
    public interface IMovieService
    {
        /// <summary>
        /// Loads the movie list from the repository into the service.
        /// </summary>
        Task InitializeAsync();

        /// <summary>
        /// Saves the current movie list back to the repository.
        /// </summary>
        Task SaveAsync();

        /// <summary>
        /// Adds a new movie to the in-memory collection.
        /// </summary>
        void AddMovie(Movie movie);

        /// <summary>
        /// Returns an unfiltered list of all movies.
        /// </summary>
        IEnumerable<Movie> GetAllMovies();

        /// <summary>
        /// Gets calculated stats for the entire collection.
        /// </summary>
        MovieCollectionStats GetMovieCollectionStats();

        /// <summary>
        /// Searches the collection using a flexible filter object.
        /// </summary>
        /// <param name="criteria">The filter criteria to apply.</param>
        /// <returns>A list of matching movies.</returns>
        IEnumerable<Movie> SearchMovies(MovieFilterCriteria criteria);

        /// <summary>
        /// Deletes a movie from the collection by index.
        /// </summary>
        /// <param name="index">The zero-based index of the movie to delete.</param>
        /// <returns>True if the movie was deleted successfully; false otherwise.</returns>
        bool DeleteMovie(int index);
    }
}
