public class CountByRoleAction : IMenuAction
{
    private readonly CrewAnalyticsService _analytics;

    public string Key => "5";
    public string Description => "Szerepenkénti darabszám";

    public CountByRoleAction(CrewAnalyticsService analytics)
    {
        _analytics = analytics;
    }

    public void Execute()
    {
        foreach (var pair in _analytics.CountByRole())
            Console.WriteLine($"{pair.Role}: {pair.Count} tag");
    }
}
