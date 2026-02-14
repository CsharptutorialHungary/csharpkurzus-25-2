using OnePieceCrewManager.Core.Services;

public class FindByNameAction : IMenuAction
{
    private readonly CrewMemberService _service;

    public string Key => "7";
    public string Description => "Keresés név alapján";

    public FindByNameAction(CrewMemberService service)
    {
        _service = service;
    }

    public void Execute()
    {
        Console.Write("Név: "); var name = Console.ReadLine()!;
        var found = _service.FindByName(name);

        Console.WriteLine(found is null
            ? "Nincs ilyen tag."
            : $"{found.Name} — {found.Role}, {found.Bounty}M Berry, {found.StrengthLevel}/100 erő");
    }
}

