using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogDatabase.Core.Models
{
    public record CompetitionRecord(
        string DogName,
        string CompetitionName,
        int Year,
        int Placement
    );
}
