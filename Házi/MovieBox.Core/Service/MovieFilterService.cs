using MovieBox.Core.Filter;
using MovieBox.Core.Records;

namespace MovieBox.Core.Service
{
    public class MovieFilterService : IMovieFilterService
    {
        public IEnumerable<Movie> FilterMovies(IEnumerable<Movie> movies, MovieFilterCriteria criteria)
        {
            var query = movies.AsQueryable();
            if (!string.IsNullOrWhiteSpace(criteria.Title))
            {
                query = query.Where(m => m.Title.Contains(criteria.Title, StringComparison.OrdinalIgnoreCase));
            }
            if (!string.IsNullOrWhiteSpace(criteria.DirectorContains))
            {
                query = query.Where(m => m.Director.Contains(criteria.DirectorContains, StringComparison.OrdinalIgnoreCase));
            }
            if (criteria.Year.HasValue)
            {
                query = query.Where(m => m.ReleaseYear == criteria.Year.Value);
            }
            if (criteria.RatingMin.HasValue)
            {
                query = query.Where(m => m.Rating >= criteria.RatingMin.Value);
            }
            if (criteria.RatingMax.HasValue)
            {
                query = query.Where(m => m.Rating <= criteria.RatingMax.Value);
            }

            return criteria.ResultCount.HasValue ? query.Take(criteria.ResultCount.Value).ToList() : query.ToList();
        }
    }
}
