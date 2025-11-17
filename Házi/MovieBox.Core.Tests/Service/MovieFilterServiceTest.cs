using MovieBox.Core.Filter;
using MovieBox.Core.Records;
using MovieBox.Core.Service;
using NUnit.Framework;

namespace MovieBox.Core.Tests.Service
{
    [TestFixture]
    public class MovieFilterServiceTest
    {
        private IMovieFilterService _filterService;
        private IEnumerable<Movie> _testMovies;

        public MovieFilterServiceTest()
        {
            _filterService = new MovieFilterService();
            _testMovies = new List<Movie>
            {
                new Movie("The Shawshank Redemption", "Frank Darabont", 1994, 9.3),
                new Movie("The Godfather", "Francis Ford Coppola", 1972, 9.2),
                new Movie("The Dark Knight", "Christopher Nolan", 2008, 9.0),
                new Movie("Pulp Fiction", "Quentin Tarantino", 1994, 8.9),
                new Movie("Inception", "Christopher Nolan", 2010, 8.8),
                new Movie("Forrest Gump", "Robert Zemeckis", 1994, 8.8),
                new Movie("Interstellar", "Christopher Nolan", 2014, 8.6),
                new Movie("The Matrix", "The Wachowskis", 1999, 8.7)
            };
        }

        #region Filter by Title Tests

        [Test]
        public void FilterMovies_WithTitleCriteria_ReturnsMoviesContainingTitle()
        {
            // Arrange
            var criteria = new MovieFilterCriteria { Title = "Dark" };

            // Act
            var result = _filterService.FilterMovies(_testMovies, criteria);

            // Assert
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result.First().Title, Is.EqualTo("The Dark Knight"));
        }

        [Test]
        public void FilterMovies_WithTitleCriteria_IsCaseInsensitive()
        {
            // Arrange
            var criteria = new MovieFilterCriteria { Title = "godfather" };

            // Act
            var result = _filterService.FilterMovies(_testMovies, criteria);

            // Assert
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result.First().Title, Is.EqualTo("The Godfather"));
        }

        [Test]
        public void FilterMovies_WithTitleCriteria_ReturnsMultipleMatches()
        {
            // Arrange
            var criteria = new MovieFilterCriteria { Title = "The" };

            // Act
            var result = _filterService.FilterMovies(_testMovies, criteria);

            // Assert
            Assert.That(result, Has.Count.EqualTo(4));
            Assert.That(result, Has.All.Matches<Movie>(m => m.Title.Contains("The")));
        }

        [Test]
        public void FilterMovies_WithTitleCriteria_NoMatches_ReturnsEmpty()
        {
            // Arrange
            var criteria = new MovieFilterCriteria { Title = "NonexistentMovie" };

            // Act
            var result = _filterService.FilterMovies(_testMovies, criteria);

            // Assert
            Assert.That(result, Is.Empty);
        }

        #endregion Filter by Title Tests

        #region Filter by Director Tests

        [Test]
        public void FilterMovies_WithDirectorCriteria_ReturnsMoviesByDirector()
        {
            // Arrange
            var criteria = new MovieFilterCriteria { DirectorContains = "Nolan" };

            // Act
            var result = _filterService.FilterMovies(_testMovies, criteria);

            // Assert
            Assert.That(result, Has.Count.EqualTo(3));
            Assert.That(result, Has.All.Matches<Movie>(m => m.Director.Contains("Nolan")));
        }

        [Test]
        public void FilterMovies_WithDirectorCriteria_IsCaseInsensitive()
        {
            // Arrange
            var criteria = new MovieFilterCriteria { DirectorContains = "nolan" };

            // Act
            var result = _filterService.FilterMovies(_testMovies, criteria);

            // Assert
            Assert.That(result, Has.Count.EqualTo(3));
        }

        [Test]
        public void FilterMovies_WithDirectorCriteria_NoMatches_ReturnsEmpty()
        {
            // Arrange
            var criteria = new MovieFilterCriteria { DirectorContains = "UnknownDirector" };

            // Act
            var result = _filterService.FilterMovies(_testMovies, criteria);

            // Assert
            Assert.That(result, Is.Empty);
        }

        #endregion Filter by Director Tests

        #region Filter by Year Tests

        [Test]
        public void FilterMovies_WithYearCriteria_ReturnsMoviesFromSpecificYear()
        {
            // Arrange
            var criteria = new MovieFilterCriteria { Year = 1994 };

            // Act
            var result = _filterService.FilterMovies(_testMovies, criteria);

            // Assert
            Assert.That(result, Has.Count.EqualTo(3));
            Assert.That(result, Has.All.Matches<Movie>(m => m.ReleaseYear == 1994));
        }

        [Test]
        public void FilterMovies_WithYearCriteria_NoMatches_ReturnsEmpty()
        {
            // Arrange
            var criteria = new MovieFilterCriteria { Year = 2030 };

            // Act
            var result = _filterService.FilterMovies(_testMovies, criteria);

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void FilterMovies_WithYearCriteria_SingleMatch()
        {
            // Arrange
            var criteria = new MovieFilterCriteria { Year = 1972 };

            // Act
            var result = _filterService.FilterMovies(_testMovies, criteria);

            // Assert
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result.First().Title, Is.EqualTo("The Godfather"));
        }

        #endregion Filter by Year Tests

        #region Filter by Rating Range Tests

        [Test]
        public void FilterMovies_WithRatingMinCriteria_ReturnsMoviesAboveMinRating()
        {
            // Arrange
            var criteria = new MovieFilterCriteria { RatingMin = 9.0 };

            // Act
            var result = _filterService.FilterMovies(_testMovies, criteria);

            // Assert
            Assert.That(result, Has.Count.EqualTo(3));
            Assert.That(result, Has.All.Matches<Movie>(m => m.Rating >= 9.0));
        }

        [Test]
        public void FilterMovies_WithRatingMaxCriteria_ReturnsMoviesBelowMaxRating()
        {
            // Arrange
            var criteria = new MovieFilterCriteria { RatingMax = 8.8 };

            // Act
            var result = _filterService.FilterMovies(_testMovies, criteria);

            // Assert
            Assert.That(result, Has.Count.EqualTo(4));
            Assert.That(result, Has.All.Matches<Movie>(m => m.Rating <= 8.8));
        }

        [Test]
        public void FilterMovies_WithRatingRangeCriteria_ReturnsMoviesInRange()
        {
            // Arrange
            var criteria = new MovieFilterCriteria { RatingMin = 8.6, RatingMax = 8.9 };

            // Act
            var result = _filterService.FilterMovies(_testMovies, criteria);

            // Assert
            Assert.That(result, Has.Count.EqualTo(5));
            Assert.That(result, Has.All.Matches<Movie>(m => m.Rating >= 8.6 && m.Rating <= 8.9));
        }

        [Test]
        public void FilterMovies_WithRatingRangeCriteria_NoMatches_ReturnsEmpty()
        {
            // Arrange
            var criteria = new MovieFilterCriteria { RatingMin = 9.5, RatingMax = 10.0 };

            // Act
            var result = _filterService.FilterMovies(_testMovies, criteria);

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void FilterMovies_WithExactRatingValue_ReturnsMoviesWithExactRating()
        {
            // Arrange
            var criteria = new MovieFilterCriteria { RatingMin = 8.8, RatingMax = 8.8 };

            // Act
            var result = _filterService.FilterMovies(_testMovies, criteria);

            // Assert
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.That(result, Has.All.Matches<Movie>(m => m.Rating == 8.8));
        }

        #endregion Filter by Rating Range Tests

        #region Filter by Result Count Tests

        [Test]
        public void FilterMovies_WithResultCountCriteria_LimitsResultsToSpecifiedCount()
        {
            // Arrange
            var criteria = new MovieFilterCriteria { ResultCount = 3 };

            // Act
            var result = _filterService.FilterMovies(_testMovies, criteria);

            // Assert
            Assert.That(result, Has.Count.EqualTo(3));
        }

        [Test]
        public void FilterMovies_WithResultCountCriteria_ReturnsFewerIfLessThanAvailable()
        {
            // Arrange
            var criteria = new MovieFilterCriteria { ResultCount = 2 };

            // Act
            var result = _filterService.FilterMovies(_testMovies, criteria);

            // Assert
            Assert.That(result, Has.Count.EqualTo(2));
        }

        [Test]
        public void FilterMovies_WithResultCountGreaterThanAvailable_ReturnsAllAvailable()
        {
            // Arrange
            var criteria = new MovieFilterCriteria { ResultCount = 100 };

            // Act
            var result = _filterService.FilterMovies(_testMovies, criteria);

            // Assert
            Assert.That(result, Has.Count.EqualTo(_testMovies.Count()));
        }

        [Test]
        public void FilterMovies_WithResultCountZero_ReturnsEmpty()
        {
            // Arrange
            var criteria = new MovieFilterCriteria { ResultCount = 0 };

            // Act
            var result = _filterService.FilterMovies(_testMovies, criteria);

            // Assert
            Assert.That(result, Is.Empty);
        }

        #endregion Filter by Result Count Tests

        #region Combined Filter Tests

        [Test]
        public void FilterMovies_WithTitleAndDirector_ReturnsCombinedResults()
        {
            // Arrange
            var criteria = new MovieFilterCriteria
            {
                Title = "The",
                DirectorContains = "Nolan"
            };

            // Act
            var result = _filterService.FilterMovies(_testMovies, criteria);

            // Assert
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result, Has.All.Matches<Movie>(m => m.Title.Contains("The") && m.Director.Contains("Nolan")));
        }

        [Test]
        public void FilterMovies_WithYearAndRating_ReturnsCombinedResults()
        {
            // Arrange
            var criteria = new MovieFilterCriteria
            {
                Year = 1994,
                RatingMin = 8.8
            };

            // Act
            var result = _filterService.FilterMovies(_testMovies, criteria);

            // Assert
            Assert.That(result, Has.Count.EqualTo(3));
            Assert.That(result, Has.All.Matches<Movie>(m => m.ReleaseYear == 1994 && m.Rating >= 8.8));
        }

        [Test]
        public void FilterMovies_WithAllCriteria_ReturnsCombinedResults()
        {
            // Arrange
            var criteria = new MovieFilterCriteria
            {
                Title = "The",
                DirectorContains = "Nolan",
                Year = 2008,
                RatingMin = 8.5,
                RatingMax = 9.5,
                ResultCount = 5
            };

            // Act
            var result = _filterService.FilterMovies(_testMovies, criteria);

            // Assert
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result.First().Title, Is.EqualTo("The Dark Knight"));
        }

        #endregion Combined Filter Tests

        #region Null or Empty Criteria Tests

        [Test]
        public void FilterMovies_WithNullCriteria_ReturnsAll()
        {
            // Arrange
            var criteria = new MovieFilterCriteria(); // All properties null/default

            // Act
            var result = _filterService.FilterMovies(_testMovies, criteria);

            // Assert
            Assert.That(result, Has.Count.EqualTo(_testMovies.Count()));
        }

        [Test]
        public void FilterMovies_WithEmptyStringTitle_ReturnsAll()
        {
            // Arrange
            var criteria = new MovieFilterCriteria { Title = "" };

            // Act
            var result = _filterService.FilterMovies(_testMovies, criteria);

            // Assert
            Assert.That(result, Has.Count.EqualTo(_testMovies.Count()));
        }

        [Test]
        public void FilterMovies_WithWhitespaceOnlyTitle_ReturnsAll()
        {
            // Arrange
            var criteria = new MovieFilterCriteria { Title = "   " };

            // Act
            var result = _filterService.FilterMovies(_testMovies, criteria);

            // Assert
            Assert.That(result, Has.Count.EqualTo(_testMovies.Count()));
        }

        #endregion Null or Empty Criteria Tests

        #region Edge Cases Tests

        [Test]
        public void FilterMovies_WithEmptyMoviesList_ReturnsEmpty()
        {
            // Arrange
            var emptyList = new List<Movie>();
            var criteria = new MovieFilterCriteria { Title = "Any" };

            // Act
            var result = _filterService.FilterMovies(emptyList, criteria);

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void FilterMovies_WithNegativeResultCount_ReturnsEmpty()
        {
            // Arrange
            var criteria = new MovieFilterCriteria { ResultCount = -1 };

            // Act
            var result = _filterService.FilterMovies(_testMovies, criteria);

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void FilterMovies_WithSpecialCharactersInTitle_ReturnsMatches()
        {
            // Arrange
            var moviesWithSpecialChars = new List<Movie>
            {
                new Movie("Movie: The Sequel", "Director", 2020, 8.0),
                new Movie("Movie, The Prequel", "Director", 2021, 8.5)
            };
            var criteria = new MovieFilterCriteria { Title = "Movie:" };

            // Act
            var result = _filterService.FilterMovies(moviesWithSpecialChars, criteria);

            // Assert
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result.First().Title, Is.EqualTo("Movie: The Sequel"));
        }

        #endregion Edge Cases Tests

        #region Result Type Tests

        [Test]
        public void FilterMovies_ReturnsListType()
        {
            // Arrange
            var criteria = new MovieFilterCriteria { Title = "The" };

            // Act
            var result = _filterService.FilterMovies(_testMovies, criteria);

            // Assert
            Assert.That(result, Is.TypeOf<List<Movie>>());
        }

        #endregion Result Type Tests
    }
}