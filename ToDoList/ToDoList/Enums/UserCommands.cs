using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Enums
{
    internal enum UserCommands
    {
        Leave,
        Add,
        Remove,
        UpdateTaskState,
        List,
        ListOnlyPending,
        ListOnlyCompleted,
        ListOnlyMissed
    }
}
