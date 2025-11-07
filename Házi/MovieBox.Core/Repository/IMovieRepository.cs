using MovieBox.Core.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieBox.Core.Repository
{
    public interface IMovieRepository
    {
        /// <summary>
        /// Loads the collection of movies from the data store asynchronously.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Movie>> LoadMovies();


        /// <summary>
        /// Saves a collection of movies to the data store asynchronously.
        /// </summary>
        /// <param name="movies">The collection of <see cref="Movie"/> objects to be saved. Cannot be null.</param>
        /// <returns>A task that represents the asynchronous save operation.</returns>
        Task SaveMovies(IEnumerable<Movie> movies);
    }
}
