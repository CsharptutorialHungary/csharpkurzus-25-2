public interface IMenuAction
{
    string Key { get; }
    string Description { get; }
    void Execute();
}

