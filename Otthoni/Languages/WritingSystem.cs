abstract class WritingSystem
{
    public readonly string name;
    public readonly string symbols;
    public WritingSystem(string name, string symbols)
    {
        this.name = name;
        this.symbols = symbols;
    }
}