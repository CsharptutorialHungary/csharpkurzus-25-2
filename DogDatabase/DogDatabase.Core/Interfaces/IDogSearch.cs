using DogDatabase.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogDatabase.Core.Interfaces
{
    public interface IDogSearch
    {
        public void SearchByYear(int year);

        public void SearchByOwner(string owner);

        public void SearchDogSiblings(string name);

        public void DisplayResults(List<DogRecord> dogs);
    }
}

