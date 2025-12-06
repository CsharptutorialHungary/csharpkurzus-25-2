using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.HelperClasses
{
    internal static class PrintEnums
    {
        public static void PrintEnum<T>() where T : Enum
        {
            foreach (var value in Enum.GetValues(typeof(T)))
            {
                Console.WriteLine($"{(int)value}  {value}");
            }
        }
    }
}
