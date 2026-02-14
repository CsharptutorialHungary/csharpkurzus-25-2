using OnePieceCrewManager.Core;
using OnePieceCrewManager.Core.Services;

public class UpdateCrewAction : IMenuAction
{
    private readonly CrewMemberService _service;
    private readonly JsonStorageService _storage;

    public string Key => "3";
    public string Description => "Tag frissítése";

    public UpdateCrewAction(CrewMemberService service, JsonStorageService storage)
    {
        _service = service;
        _storage = storage;
    }

    public void Execute()
    {
        Console.Write("Frissítendő tag neve: ");
        var name = Console.ReadLine()!;
        var existing = _service.FindByName(name);

        if (existing == null)
        {
            Console.WriteLine($"Hiba: '{name}' nevű tag nem található.");
            return;
        }

        Console.WriteLine("Új adatok megadása (Enter = változatlan):");

        Console.Write($"Név ({existing.Name}): ");
        var nameStr = Console.ReadLine();
        var newName = string.IsNullOrWhiteSpace(nameStr) ? existing.Name : nameStr;

        Console.Write($"Szerep ({existing.Role}): ");
        var roleStr = Console.ReadLine();
        var role = existing.Role;
        if (!string.IsNullOrWhiteSpace(roleStr) && Enum.TryParse<Role>(roleStr, true, out var parsedRole))
            role = parsedRole;

        Console.Write($"Bounty ({existing.Bounty}): ");
        var bountyStr = Console.ReadLine();
        var bounty = existing.Bounty;
        if (!string.IsNullOrWhiteSpace(bountyStr) && int.TryParse(bountyStr, out var parsedBounty) && parsedBounty >= 0)
            bounty = parsedBounty;

        Console.Write($"Kor ({existing.Age}): ");
        var ageStr = Console.ReadLine();
        var age = existing.Age;
        if (!string.IsNullOrWhiteSpace(ageStr) && int.TryParse(ageStr, out var parsedAge) && parsedAge >= 0)
            age = parsedAge;

        Console.Write($"Erősség ({existing.StrengthLevel}): ");
        var strengthStr = Console.ReadLine();
        var strength = existing.StrengthLevel;
        if (!string.IsNullOrWhiteSpace(strengthStr) && int.TryParse(strengthStr, out var parsedStrength) && parsedStrength >= 1 && parsedStrength <= 100)
            strength = parsedStrength;

        var updated = new CrewMember(newName, role, bounty, age, strength);
        _service.Update(updated,name);

        if (_storage.Save(_service.ListAll().ToList()))
            Console.WriteLine("Tag sikeresen frissítve és mentve.");
        else
            Console.WriteLine("Hiba: a tag frissítve, de mentés nem sikerült.");
    }
}
