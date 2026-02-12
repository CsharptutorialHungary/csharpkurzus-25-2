using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieBox.Core.Records;

namespace MovieBox.Core.Filter
{
    public class MovieFilterCriteria
    {
        public string? Title { get; set; }
        public string? DirectorContains { get; set; }

        public int? Year { get; set; }

        public double? RatingMin { get; set; }
        public double? RatingMax { get; set; }

        public int? ResultCount { get; set; }

        public MovieStatus? Status { get; set; }
    }
}
