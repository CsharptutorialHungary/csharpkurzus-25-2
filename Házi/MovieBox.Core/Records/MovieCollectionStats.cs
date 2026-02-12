using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieBox.Core.Records
{
    public record MovieCollectionStats(
        int TotalCount,
        double AverageRating,
        Movie? HighestRatedMovie,
        Dictionary<int, int> MoviesPerDecade
    );
}
