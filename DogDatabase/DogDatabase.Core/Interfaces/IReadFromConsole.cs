using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogDatabase.Core.Interfaces
{
    public interface IReadFromConsole
    {
        public int ReadInt();

        public string ReadNonEmptyString();

        public string? ReadOptionalString();

        public bool CheckIfGenderIsCorrect(string gender);

        public bool CheckIfHealthStatusIsCorrect(string healthStatus);
    }
}
