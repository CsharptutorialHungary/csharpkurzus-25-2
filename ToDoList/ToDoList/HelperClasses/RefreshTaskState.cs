using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Interfaces;

namespace ToDoList.HelperClasses
{
    internal static class RefreshTaskState
    {
        public static void RefreshList(IEnumerable<ITask> tasks)
        {
            foreach (var item in tasks)
            {
                if (item.State is not Enums.TaskState.Completed && DateTime.Now > item.DeadLine)
                {
                    item.State = Enums.TaskState.Missed;
                }
            }
        }
    }
}
