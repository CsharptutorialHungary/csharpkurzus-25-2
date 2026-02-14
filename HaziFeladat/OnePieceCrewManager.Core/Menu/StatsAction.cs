public class StatsAction : IMenuAction
{
    private readonly CrewAnalyticsService _analytics;

    public string Key => "6";
    public string Description => "Statisztikák";

    public StatsAction(CrewAnalyticsService analytics)
    {
        _analytics = analytics;
    }

    public void Execute()
    {
        var (avgBounty, maxStrength, youngestAge) = _analytics.Stats();
        Console.WriteLine($"Átlag bounty: {avgBounty:F1}M Berry");
        Console.WriteLine($"Legnagyobb erősség: {maxStrength}/100");
        Console.WriteLine($"Legfiatalabb kor: {youngestAge}");
    }
}
