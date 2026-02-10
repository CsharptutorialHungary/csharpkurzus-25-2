using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Exceptions
{
    internal class InvalidTaskNameException : Exception
    {
        public InvalidTaskNameException(string message) : base(message)
        {

        }
    }
}
