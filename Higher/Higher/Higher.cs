namespace Higher;

internal record PlayerScore (string Name, int Score, string GameName, DateTime Date)
{
    // TODO: Szerintem nem javasolt recor-nak metodusanak lennie. Egy kulon extension method-okat csinalnek hozza
    public void PrettyPrint()
    {
        Console.WriteLine($"{Name} | {Score} | {GameName} | {Date}\n");
    }
}
