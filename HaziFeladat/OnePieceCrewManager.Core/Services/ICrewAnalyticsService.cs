namespace OnePieceCrewManager.Core.Services
{
    public interface ICrewAnalyticsService
    {
        IEnumerable<CrewMember> FindHighBounty(int minBounty);
        IEnumerable<(Role Role, int Count)> CountByRole();
        (double avgBounty, int maxStrength, int youngestAge) Stats();
    }

}
