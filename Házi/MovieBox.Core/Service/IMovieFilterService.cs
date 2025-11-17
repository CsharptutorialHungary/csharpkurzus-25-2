using MovieBox.Core.Filter;
using MovieBox.Core.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieBox.Core.Service
{
    public interface IMovieFilterService
    {

        /// <summary>
        /// Filters a collection of movies based on the provided criteria.
        /// </summary>
        /// <param name="movies">The collection of movies to filter.</param>
        /// <param name="criteria">The filter criteria to apply.</param>
        /// <returns>A list of movies that match the filter criteria.</returns>
        IEnumerable<Movie> FilterMovies(IEnumerable<Movie> movies, MovieFilterCriteria criteria);
    }
}
