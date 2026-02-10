using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Exceptions
{
    internal class InvalidStateException : Exception
    {
        public InvalidStateException(string message) : base(message)
        {
            
        }
    }
}
