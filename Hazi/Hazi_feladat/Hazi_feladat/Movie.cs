using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hazi_feladat
{
    public record Movie(
    string Title,
    string? Director = null,
    int? Year = null,
    string? Genre = null,
    double? Rating = null
    );
}
