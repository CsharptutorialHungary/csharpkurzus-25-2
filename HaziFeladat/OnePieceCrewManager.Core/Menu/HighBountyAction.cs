public class HighBountyAction : IMenuAction
{
    private readonly CrewAnalyticsService _analytics;

    public string Key => "8";
    public string Description => "Nagy bounty-s tagok";

    public HighBountyAction(CrewAnalyticsService analytics)
    {
        _analytics = analytics;
    }

    public void Execute()
    {
        Console.Write("Minimum bounty (millió Berry): ");
        var minStr = Console.ReadLine()!;
        if (!int.TryParse(minStr, out var min))
        {
            Console.WriteLine("Hiba: nem szám.");
            return;
        }

        var high = _analytics.FindHighBounty(min);
        if (!high.Any()) Console.WriteLine("Nincs találat.");
        else foreach (var c in high) Console.WriteLine($"{c.Name} — {c.Bounty}M Berry");
    }
}
