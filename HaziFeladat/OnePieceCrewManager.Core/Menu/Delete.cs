using OnePieceCrewManager.Core.Services;

public class DeleteCrewAction : IMenuAction
{
    private readonly CrewMemberService _service;
    private readonly JsonStorageService _storage;

    public string Key => "4";
    public string Description => "Tag törlése";

    public DeleteCrewAction(CrewMemberService service, JsonStorageService storage)
    {
        _service = service;
        _storage = storage;
    }

    public void Execute()
    {
        Console.Write("Törlendő tag neve: ");
        var name = Console.ReadLine()!;
        var existing = _service.FindByName(name);

        if (existing == null)
        {
            Console.WriteLine($"Hiba: '{name}' nevű tag nem található.");
            return;
        }

        _service.Delete(existing);

        if (_storage.Save(_service.ListAll().ToList()))
            Console.WriteLine("Tag sikeresen törölve és mentve.");
        else
            Console.WriteLine("Hiba: a tag törölve, de mentés nem sikerült.");
    }
}
