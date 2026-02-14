using OnePieceCrewManager.Core.Services;
public class ListCrewAction : IMenuAction
{
    private readonly ICrewRepositoryService _repository;

    public string Key => "1";
    public string Description => "Lista";

    public ListCrewAction(ICrewRepositoryService repository)
    {
        _repository = repository;
    }

    public void Execute()
    {
        foreach (var c in _repository.ListAll())
            Console.WriteLine($"{c.Name} — {c.Role}, {c.Bounty}M Berry, {c.StrengthLevel}/100 erő");
    }
}

