using OnePieceCrewManager.Core;
using OnePieceCrewManager.Core.Services;

public class CrewAnalyticsService : ICrewAnalyticsService
{
    private readonly ICrewRepositoryService _repository;

    public CrewAnalyticsService(ICrewRepositoryService repository)
    {
        _repository = repository;
    }

    public IEnumerable<CrewMember> FindHighBounty(int minBounty) =>
        _repository.ListAll()
                   .Where(c => c.Bounty >= minBounty)
                   .OrderByDescending(c => c.Bounty);

    public IEnumerable<(Role Role, int Count)> CountByRole() =>
        _repository.ListAll()
                   .GroupBy(c => c.Role)
                   .Select(g => (Role: g.Key, Count: g.Count()))
                   .OrderByDescending(x => x.Count);

    public (double avgBounty, int maxStrength, int youngestAge) Stats()
    {
        var crew = _repository.ListAll();
        if (crew.Count == 0) return (0, 0, 0);

        double avg = crew.Average(c => c.Bounty);
        int maxStrength = crew.Max(c => c.StrengthLevel);
        int youngestAge = crew.Min(c => c.Age);

        return (avg, maxStrength, youngestAge);
    }
}
