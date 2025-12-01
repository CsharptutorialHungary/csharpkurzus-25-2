using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogDatabase.Core.Models
{
    public record DogRecord(
        string Name,
        string Gender,
        string HealthStatus,
        int Birthyear,
        string Owner,
        string? Mother,
        string? Father
    );
}
