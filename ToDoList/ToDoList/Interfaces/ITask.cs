using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Enums;

namespace ToDoList.Interfaces
{
    internal interface ITask
    {
        string TaskName { get; set; }
        TaskCategory Category { get; set; }
        TaskState State { get; set; }
        DateTime DeadLine { get; set; }
    }
}