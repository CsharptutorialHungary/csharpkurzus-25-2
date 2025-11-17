using MovieBox.Core.Filter;
using MovieBox.Core.Records;
using MovieBox.Core.Repository;

namespace MovieBox.Core.Service
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _repository;
        private readonly IMovieFilterService _filterService;
        private readonly List<Movie> _movies;

        public MovieService(IMovieRepository repository, IMovieFilterService filterService)
        {
            _repository = repository;
            _filterService = filterService;
            _movies = new List<Movie>();
        }

        public void AddMovie(Movie movie)
        {
            if (movie is null) throw new ArgumentNullException(nameof(movie));
            _movies.Add(movie);
        }

        public IEnumerable<Movie> GetAllMovies()
            => _movies.AsReadOnly();

        public MovieCollectionStats GetMovieCollectionStats()
        {
            var total = _movies.Count;
            var average = total > 0 ? _movies.Average(m => m.Rating) : 0.0;
            var highest = _movies.OrderByDescending(m => m.Rating).ThenBy(m => m.Title).FirstOrDefault();

            var perDecade = _movies
                .GroupBy(m => (m.ReleaseYear / 10) * 10)
                .ToDictionary(g => g.Key, g => g.Count());

            return new MovieCollectionStats(total, average, highest, perDecade);
        }

        public async Task InitializeAsync()
        {
            try
            {
                var loaded = await _repository.LoadMovies();
                if (loaded != null)
                {
                    _movies.Clear();
                    _movies.AddRange(loaded);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[MovieService] Error: Failed to initialize movies. {ex.Message}");
                throw new InvalidOperationException("Failed to initialize MovieService.", ex);
            }
        }

        public async Task SaveAsync()
        {
            await _repository.SaveMovies(_movies);
        }

        public void DeleteMovie(Movie movie)
        {
            if (movie is null) throw new ArgumentNullException(nameof(movie));
            _movies.Remove(movie);
        }

        public IEnumerable<Movie> SearchMovies(MovieFilterCriteria criteria)
            => _filterService.FilterMovies(_movies.AsReadOnly(), criteria);

        public bool DeleteMovie(int index)
        {
            if (index < 0 || index >= _movies.Count)
            {
                return false;
            }

            _movies.RemoveAt(index);
            return true;
        }
    }
}