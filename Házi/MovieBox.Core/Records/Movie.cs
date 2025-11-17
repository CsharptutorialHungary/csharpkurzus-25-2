using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieBox.Core.Records
{
    public record Movie(string Title, string Director, int ReleaseYear, double Rating);
}
