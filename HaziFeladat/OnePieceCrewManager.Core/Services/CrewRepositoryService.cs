namespace OnePieceCrewManager.Core.Services
{
    public class CrewRepositoryService : ICrewRepositoryService
    {
        private readonly List<CrewMember> _crew = new();

        public CrewRepositoryService(List<CrewMember>? initialCrew = null)
        {
            if (initialCrew != null) _crew.AddRange(initialCrew);
        }

        public IReadOnlyList<CrewMember> ListAll() => _crew;

        public void Add(CrewMember member) => _crew.Add(member);

        public CrewMember? FindByName(string name) =>
            _crew.FirstOrDefault(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

        public void Update(CrewMember member, string name)
        {
            var index = _crew.FindIndex(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (index == -1)
                throw new InvalidOperationException($"Crew member '{name}' not found.");

            _crew[index] = member;
        }

        public void Delete(CrewMember member)
        {
            var existing = _crew.FirstOrDefault(c => c.Name.Equals(member.Name, StringComparison.OrdinalIgnoreCase));
            if (existing == null)
                throw new InvalidOperationException($"Crew member '{member.Name}' not found.");

            _crew.Remove(existing);
        }
    }

}
