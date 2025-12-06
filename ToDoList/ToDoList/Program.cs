using System.Runtime.InteropServices.Marshalling;
using ToDoList.Enums;
using ToDoList.HelperClasses;
using ToDoList.HelperClasses.UserActions;
using ToDoList.Interfaces;


JsonFileCreator.Create();
List<ITask> JsonList = JsonLoadFileToList.LoadList().Cast<ITask>().ToList();
RefreshTaskState.RefreshList(JsonList);
JsonOrderList.OrderList(JsonList);


bool isDirty = false;

var actions = new Dictionary<UserCommands, Action>
{
    {UserCommands.Leave, () => {JsonSaveListToFile.SaveListToFile(JsonList); Environment.Exit(0); }},
    {UserCommands.Add, () => {AddTaskToList.AddTask(JsonList); JsonOrderList.OrderList(JsonList) ; isDirty = true;}},
    {UserCommands.Remove, () => {RefreshTaskState.RefreshList(JsonList); JsonListTasks.ListTasks(JsonList);RemoveTask.Remove(JsonList); isDirty = true; } },
    {UserCommands.List, () => {RefreshTaskState.RefreshList(JsonList); JsonListTasks.ListTasks(JsonList);}},
    {UserCommands.ListOnlyCompleted, () => {RefreshTaskState.RefreshList(JsonList); JsonListTasks.ListTasks(JsonList,false,TaskState.Completed); } },
    {UserCommands.ListOnlyPending, () => {RefreshTaskState.RefreshList(JsonList); JsonListTasks.ListTasks(JsonList,false, TaskState.Pending); } },
    {UserCommands.ListOnlyMissed, () => {RefreshTaskState.RefreshList(JsonList); JsonListTasks.ListTasks(JsonList,false, TaskState.Missed); } },
    {UserCommands.UpdateTaskState, () => {RefreshTaskState.RefreshList(JsonList); JsonListTasks.ListTasks(JsonList);
                                           TaskStateUpdate.UpdateTaskState(JsonList); isDirty = true; } }
};



Console.WriteLine("Type a command to begin");
while (true)
{
    PrintEnums.PrintEnum<UserCommands>();

    Console.Write("Command: ");

    if (!Enum.TryParse<UserCommands>(Console.ReadLine(), out UserCommands command) || !Enum.IsDefined(typeof(UserCommands), command))
    {
        Console.Clear();
        Console.WriteLine("Invalid Command");
        continue;
    }

    Console.Clear();

    if (actions.TryGetValue(command, out Action? value))
    {
        value.Invoke();
    }
    else
    {
        Console.WriteLine("Command does not exist");
    }


    if (isDirty)
    {
        JsonSaveListToFile.SaveListToFile(JsonList);
        isDirty = false;
    }
}


