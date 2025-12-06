using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ToDoList.Enums;
using ToDoList.Exceptions;
using ToDoList.Interfaces;

namespace ToDoList.Classes
{
    internal class ToDoTask : ITask
    {

        private string _taskName;
        private TaskCategory _category;
        private TaskState _state;
        private DateTime _deadLine;

        public ToDoTask()
        {
            //Jsonnak kell egy ilyen konstruktor
        }

        public ToDoTask(string taskName, string category, string deadline)
        {
            TaskName = taskName;
            SetCategory(category);
            SetDeadLine(deadline);

            _state = DateTime.Now > _deadLine ? TaskState.Missed : TaskState.Pending;
        }



        public string TaskName
        {
            get => _taskName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new InvalidTaskNameException("Name cannot be empty");
                }
                _taskName = value;
            }
        }


        public TaskCategory Category
        {
            set => _category = value;
            get => _category;

        }

        public void SetCategory(string value)
        {
            if (!Enum.TryParse<TaskCategory>(value, ignoreCase: true, out TaskCategory result) || !Enum.IsDefined(typeof(TaskCategory), result))
            {
                throw new InvalidCategoryException("Category does not exist");
            }
            _category = result;
        }

        public TaskState State
        {
            set => _state = value;
            get => _state;
        }


        public DateTime DeadLine
        {
            set => _deadLine = value;
            get => _deadLine;
        }

        public void SetDeadLine(string value)
        {
            string format = "yyyy.MM.dd. HH:mm";

            if (!DateTime.TryParseExact(value, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
            {
                throw new InvalidDateException("Invalid date");
            }
            _deadLine = result;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not ToDoTask other) return false;

            return _taskName == other._taskName
                && Category == other.Category
                && DeadLine == other.DeadLine
                && State == other.State;
        }

        public override string ToString()
        {
            return $"{_taskName,-30} {_category,-15} {_deadLine,-20: yyyy.MM.dd HH:mm}";
        }
    }
}
