namespace OnePieceCrewManager.Core.Services
{
    public interface ICrewRepositoryService
    {
        IReadOnlyList<CrewMember> ListAll();
        void Add(CrewMember member);
        CrewMember? FindByName(string name);
        void Update(CrewMember member, string name);
        void Delete(CrewMember member);
    }

}
