using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Interfaces;

namespace ToDoList.HelperClasses
{
    internal static class JsonOrderList
    {
        public static void OrderList(List<ITask> tasks) 
        {
            tasks.Sort((a,b) => a.DeadLine.CompareTo(b.DeadLine));
        }
    }
}
