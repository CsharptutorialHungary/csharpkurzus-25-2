using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace DogDatabase.Core.Interfaces
{
    public interface IDogService
    {
        public void CreateDog();

        public void UpdateDogHealthStatus(string name);

        public void UpdateDogOwner(string name);
    }
}
