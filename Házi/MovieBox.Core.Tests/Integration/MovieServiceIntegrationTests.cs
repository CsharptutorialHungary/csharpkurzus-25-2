using MovieBox.Core.Filter;
using MovieBox.Core.Records;
using MovieBox.Core.Repository;
using MovieBox.Core.Service;
using MovieBox.Infrastucture;
using NUnit.Framework;

namespace MovieBox.Core.Tests.Integration
{
    [TestFixture]
    public class MovieServiceIntegrationTests
    {
        private IMovieService _movieService;
        private IMovieRepository _repository;
        private IMovieFilterService _filterService;
        private const string TestFilePath = "test_movies_integration.json";

        public MovieServiceIntegrationTests()

        {
            _repository = new JsonMovieRepository(TestFilePath);
            _filterService = new MovieFilterService();
            _movieService = new MovieService(_repository, _filterService);
        }

        [SetUp]
        public void Setup()
        {
            // Clean up test file before each test
            if (File.Exists(TestFilePath))
            {
                File.Delete(TestFilePath);
            }

            // Use test-specific file path
            _repository = new JsonMovieRepository(TestFilePath);
            _filterService = new MovieFilterService();
            _movieService = new MovieService(_repository, _filterService);
        }

        [TearDown]
        public void TearDown()
        {
            // Clean up test file after each test
            if (File.Exists(TestFilePath))
            {
                File.Delete(TestFilePath);
            }
        }

        #region Round-Trip Tests (Save/Load Integration)

        [Test]
        public async Task SaveAndLoadRoundTrip_PreserveMovieData()
        {
            // Arrange
            var originalMovies = new List<Movie>
                 {
                    new Movie("Test Movie 1", "Director 1", 2020, 8.5),
                    new Movie("Test Movie 2", "Director 2", 2021, 9.0),
                    new Movie("Test Movie 3", "Director 3", 2022, 7.5)
                };

            foreach (var movie in originalMovies)
            {
                _movieService.AddMovie(movie);
            }

            // Act
            await _movieService.SaveAsync();

            // Create new service instance to simulate loading
            var newService = new MovieService(_repository, _filterService);
            await newService.InitializeAsync();

            // Assert
            var loadedMovies = newService.GetAllMovies().ToList();
            Assert.That(loadedMovies, Has.Count.EqualTo(3));
            Assert.That(loadedMovies[0].Title, Is.EqualTo("Test Movie 1"));
            Assert.That(loadedMovies[0].Director, Is.EqualTo("Director 1"));
            Assert.That(loadedMovies[0].ReleaseYear, Is.EqualTo(2020));
            Assert.That(loadedMovies[0].Rating, Is.EqualTo(8.5));
        }

        [Test]
        public async Task SaveEmptyCollection_LoadReturnsEmpty()
        {
            // Arrange - no movies added

            // Act
            await _movieService.SaveAsync();

            var newService = new MovieService(_repository, _filterService);
            await newService.InitializeAsync();

            // Assert
            var loadedMovies = newService.GetAllMovies();
            Assert.That(loadedMovies, Is.Empty);
        }

        [Test]
        public async Task SaveMultipleMovies_AllMoviesPreserved()
        {
            // Arrange
            var movies = new List<Movie>
            {
                new Movie("Movie A", "Director A", 2010, 9.0),
                new Movie("Movie B", "Director B", 2015, 8.0),
                new Movie("Movie C", "Director C", 2020, 7.0),
                new Movie("Movie D", "Director D", 2021, 8.5)
            };

            foreach (var movie in movies)
            {
                _movieService.AddMovie(movie);
            }

            // Act
            await _movieService.SaveAsync();

            var newService = new MovieService(_repository, _filterService);
            await newService.InitializeAsync();

            // Assert
            var loaded = newService.GetAllMovies().ToList();
            Assert.That(loaded, Has.Count.EqualTo(4));
            Assert.That(loaded.Select(m => m.Title), Is.EqualTo(movies.Select(m => m.Title)));
        }

        #endregion Round-Trip Tests (Save/Load Integration)

        #region Initialize/Load Integration Tests

        [Test]
        public async Task InitializeAsync_LoadsMoviesFromRepository()
        {
            // Arrange - Create test data file first
            var testMovies = new List<Movie>
            {
                new Movie("Preloaded Movie 1", "Director A", 2015, 8.9),
                new Movie("Preloaded Movie 2", "Director B", 2016, 9.1)
            };

            var preloadService = new MovieService(_repository, _filterService);
            foreach (var movie in testMovies)
            {
                preloadService.AddMovie(movie);
            }
            await preloadService.SaveAsync();

            // Act
            var newService = new MovieService(_repository, _filterService);
            await newService.InitializeAsync();

            // Assert
            var loaded = newService.GetAllMovies().ToList();
            Assert.That(loaded, Has.Count.EqualTo(2));
            Assert.That(loaded[0].Title, Is.EqualTo("Preloaded Movie 1"));
        }

        [Test]
        public async Task InitializeAsync_WithEmptyFile_ReturnsEmptyCollection()
        {
            // Arrange
            var emptyService = new MovieService(_repository, _filterService);
            await emptyService.SaveAsync(); // Save empty collection

            // Act
            var newService = new MovieService(_repository, _filterService);
            await newService.InitializeAsync();

            // Assert
            var loaded = newService.GetAllMovies();
            Assert.That(loaded, Is.Empty);
        }

        [Test]
        public async Task InitializeAsync_ClearsExistingMovies()
        {
            // Arrange
            var firstService = new MovieService(_repository, _filterService);
            firstService.AddMovie(new Movie("First Movie", "Director", 2020, 8.0));
            await firstService.SaveAsync();

            var secondService = new MovieService(_repository, _filterService);
            secondService.AddMovie(new Movie("Temporary Movie", "Director", 2021, 7.0));

            // Act
            await secondService.InitializeAsync(); // Should clear "Temporary Movie" and load "First Movie"

            // Assert
            var movies = secondService.GetAllMovies().ToList();
            Assert.That(movies, Has.Count.EqualTo(1));
            Assert.That(movies[0].Title, Is.EqualTo("First Movie"));
        }

        #endregion Initialize/Load Integration Tests

        #region Statistics Calculation Integration Tests

        [Test]
        public void GetMovieCollectionStats_CalculatesCorrectlyWithRepositoryData()
        {
            // Arrange
            var movies = new List<Movie>
            {
               new Movie("Movie 1", "Director", 1990, 9.0),
               new Movie("Movie 2", "Director", 1995, 8.0),
               new Movie("Movie 3", "Director", 2000, 7.0),
               new Movie("Movie 4", "Director", 2005, 9.5)
            };

            foreach (var movie in movies)
            {
                _movieService.AddMovie(movie);
            }

            // Act
            var stats = _movieService.GetMovieCollectionStats();

            // Assert
            Assert.That(stats.TotalCount, Is.EqualTo(4));
            Assert.That(stats.AverageRating, Is.EqualTo(8.375).Within(0.01));
            Assert.That(stats.HighestRatedMovie?.Title, Is.EqualTo("Movie 4"));
            Assert.That(stats.HighestRatedMovie?.Rating, Is.EqualTo(9.5));
        }

        #endregion Statistics Calculation Integration Tests

        #region Search/Filter Integration Tests

        [Test]
        public void SearchMovies_IntegrationWithFilterService()
        {
            // Arrange
            var movies = new List<Movie>
            {
                new Movie("The Dark Knight", "Christopher Nolan", 2008, 9.0),
                new Movie("Inception", "Christopher Nolan", 2010, 8.8),
                new Movie("Pulp Fiction", "Quentin Tarantino", 1994, 8.9)
            };

            foreach (var movie in movies)
            {
                _movieService.AddMovie(movie);
            }

            var criteria = new MovieFilterCriteria { DirectorContains = "Nolan" };

            // Act
            var result = _movieService.SearchMovies(criteria);

            // Assert
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.That(result, Has.All.Matches<Movie>(m => m.Director.Contains("Nolan")));
        }

        [Test]
        public void SearchMovies_WithComplexCriteria()
        {
            // Arrange
            var movies = new List<Movie>
            {
                new Movie("The Shawshank Redemption", "Frank Darabont", 1994, 9.3),
                new Movie("Pulp Fiction", "Quentin Tarantino", 1994, 8.9),
                new Movie("Forrest Gump", "Robert Zemeckis", 1994, 8.8),
                new Movie("The Dark Knight", "Christopher Nolan", 2008, 9.0)
            };

            foreach (var movie in movies)
            {
                _movieService.AddMovie(movie);
            }

            var criteria = new MovieFilterCriteria
            {
                Year = 1994,
                RatingMin = 8.8,
                ResultCount = 2
            };

            // Act
            var result = _movieService.SearchMovies(criteria).ToList();

            // Assert
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.That(result, Has.All.Matches<Movie>(m => m.ReleaseYear == 1994 && m.Rating >= 8.8));
        }

        [Test]
        public void SearchMovies_NoMatchesReturnsEmpty()
        {
            // Arrange
            var movies = new List<Movie>
            {
                new Movie("Movie 1", "Director A", 2020, 8.0),
                new Movie("Movie 2", "Director B", 2021, 7.0)
            };

            foreach (var movie in movies)
            {
                _movieService.AddMovie(movie);
            }

            var criteria = new MovieFilterCriteria { DirectorContains = "NonExistent" };

            // Act
            var result = _movieService.SearchMovies(criteria);

            // Assert
            Assert.That(result, Is.Empty);
        }

        #endregion Search/Filter Integration Tests

        #region Error Handling Integration Tests

        [Test]
        public void AddMovie_WithNullMovie_ThrowsArgumentNullException()
        {
            // Arrange
            Movie? nullMovie = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _movieService.AddMovie(nullMovie));
        }

        [Test]
        public void SaveAsync_WithEmptyCollection_DoesNotThrow()
        {
            // Act & Assert
            Assert.DoesNotThrowAsync(async () => await _movieService.SaveAsync());
        }

        [Test]
        public void InitializeAsync_WithNoExistingFile_DoesNotThrow()
        {
            // Arrange - No file exists
            var newService = new MovieService(_repository, _filterService);

            // Act & Assert
            Assert.DoesNotThrowAsync(async () => await newService.InitializeAsync());
        }

        #endregion Error Handling Integration Tests

        #region Add/Update Workflow Integration Tests

        [Test]
        public async Task FullWorkflow_AddSaveLoadAndSearch()
        {
            // Arrange - Initial service
            _movieService.AddMovie(new Movie("Inception", "Christopher Nolan", 2010, 8.8));
            _movieService.AddMovie(new Movie("Interstellar", "Christopher Nolan", 2014, 8.6));
            _movieService.AddMovie(new Movie("Pulp Fiction", "Quentin Tarantino", 1994, 8.9));

            // Act - Save
            await _movieService.SaveAsync();

            // Create new service and load
            var newService = new MovieService(_repository, _filterService);
            await newService.InitializeAsync();

            // Search in loaded data
            var searchCriteria = new MovieFilterCriteria { DirectorContains = "Nolan" };
            var searchResult = newService.SearchMovies(searchCriteria).ToList();

            // Assert
            Assert.That(newService.GetAllMovies(), Has.Count.EqualTo(3));
            Assert.That(searchResult, Has.Count.EqualTo(2));
            Assert.That(newService.GetMovieCollectionStats().AverageRating, Is.GreaterThan(8.5));
        }

        [Test]
        public async Task MultipleAddAndSave_PreservesAllMovies()
        {
            // Arrange & Act - First batch
            _movieService.AddMovie(new Movie("Movie 1", "Director 1", 2020, 8.0));
            _movieService.AddMovie(new Movie("Movie 2", "Director 2", 2021, 8.5));
            await _movieService.SaveAsync();

            // Create new service, load, and add more
            var newService = new MovieService(_repository, _filterService);
            await newService.InitializeAsync();
            newService.AddMovie(new Movie("Movie 3", "Director 3", 2022, 9.0));
            await newService.SaveAsync();

            // Reload and verify
            var finalService = new MovieService(_repository, _filterService);
            await finalService.InitializeAsync();

            // Assert
            var finalMovies = finalService.GetAllMovies().ToList();
            Assert.That(finalMovies, Has.Count.EqualTo(3));
            Assert.That(finalMovies.Select(m => m.Title), Contains.Item("Movie 1"));
            Assert.That(finalMovies.Select(m => m.Title), Contains.Item("Movie 3"));
        }

        #endregion Add/Update Workflow Integration Tests

        #region Data Validation Integration Tests

        [Test]
        public async Task LoadedMovies_MaintainDataIntegrity()
        {
            // Arrange
            var originalMovie = new Movie("Exact Title", "Exact Director", 1985, 7.55);
            _movieService.AddMovie(originalMovie);
            await _movieService.SaveAsync();

            // Act
            var newService = new MovieService(_repository, _filterService);
            await newService.InitializeAsync();
            var loadedMovie = newService.GetAllMovies().First();

            // Assert - Verify all properties match exactly
            Assert.That(loadedMovie.Title, Is.EqualTo("Exact Title"));
            Assert.That(loadedMovie.Director, Is.EqualTo("Exact Director"));
            Assert.That(loadedMovie.ReleaseYear, Is.EqualTo(1985));
            Assert.That(loadedMovie.Rating, Is.EqualTo(7.55));
        }

        #endregion Data Validation Integration Tests

        #region Delete Movie Integration Tests

        [Test]
        public void DeleteMovie_RemovesMovieAtValidIndex()
        {
            // Arrange
            _movieService.AddMovie(new Movie("Movie 1", "Director 1", 2020, 8.0));
            _movieService.AddMovie(new Movie("Movie 2", "Director 2", 2021, 8.5));
            _movieService.AddMovie(new Movie("Movie 3", "Director 3", 2022, 9.0));

            // Act
            bool result = _movieService.DeleteMovie(1); // Delete "Movie 2"

            // Assert
            Assert.That(result, Is.True);
            var movies = _movieService.GetAllMovies().ToList();
            Assert.That(movies, Has.Count.EqualTo(2));
            Assert.That(movies[0].Title, Is.EqualTo("Movie 1"));
            Assert.That(movies[1].Title, Is.EqualTo("Movie 3"));
        }

        [Test]
        public void DeleteMovie_WithFirstIndex_RemovesFirstMovie()
        {
            // Arrange
            _movieService.AddMovie(new Movie("First", "Dir", 2020, 8.0));
            _movieService.AddMovie(new Movie("Second", "Dir", 2021, 8.5));
            _movieService.AddMovie(new Movie("Third", "Dir", 2022, 9.0));

            // Act
            bool result = _movieService.DeleteMovie(0);

            // Assert
            Assert.That(result, Is.True);
            var movies = _movieService.GetAllMovies().ToList();
            Assert.That(movies[0].Title, Is.EqualTo("Second"));
        }

        [Test]
        public void DeleteMovie_WithLastIndex_RemovesLastMovie()
        {
            // Arrange
            _movieService.AddMovie(new Movie("First", "Dir", 2020, 8.0));
            _movieService.AddMovie(new Movie("Second", "Dir", 2021, 8.5));
            _movieService.AddMovie(new Movie("Third", "Dir", 2022, 9.0));

            // Act
            bool result = _movieService.DeleteMovie(2);

            // Assert
            Assert.That(result, Is.True);
            var movies = _movieService.GetAllMovies().ToList();
            Assert.That(movies, Has.Count.EqualTo(2));
            Assert.That(movies[1].Title, Is.EqualTo("Second"));
        }

        #endregion Delete Movie Integration Tests
    }
}