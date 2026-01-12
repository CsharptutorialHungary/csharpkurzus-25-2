namespace LibraryApp.Core;

[AttributeUsage(AttributeTargets.Class)]
public class CommandAttribute : Attribute
{
    public required string Symbol { get; init; }
}
