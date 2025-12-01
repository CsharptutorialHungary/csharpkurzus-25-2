using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogDatabase.Core.Interfaces
{
    public interface IRepository<T>
    {
        List<T> LoadAll();
        void SaveAll(List<T> items);
        void Add(T item);
    }
}
