using OnePieceCrewManager.Core;
using OnePieceCrewManager.Core.Services;

public class AddCrewAction : IMenuAction
{
    private readonly CrewMemberService _service;
    private readonly JsonStorageService _storage;

    public string Key => "2";
    public string Description => "Új tag hozzáadása";

    public AddCrewAction(CrewMemberService service, JsonStorageService storage)
    {
        _service = service;
        _storage = storage;
    }

    public void Execute()
    {
        Console.Write("Név: "); var name = Console.ReadLine()!;
        Console.WriteLine("Választható szerepek:");
        foreach (var r in Enum.GetValues<Role>())
        {
            Console.WriteLine($"- {r}");
        }
        Console.Write("Szerep: "); var roleStr = Console.ReadLine()!;
        Console.Write("Bounty: "); var bountyStr = Console.ReadLine()!;
        Console.Write("Kor: "); var ageStr = Console.ReadLine()!;
        Console.Write("Erősség (1–100): "); var strengthStr = Console.ReadLine()!;

        if (!Enum.TryParse<Role>(roleStr, true, out var role))
        {
            Console.WriteLine("Hiba: ismeretlen szerep. Válassz a felsorolt szerepek közül.");
            return;
        }

        if (!int.TryParse(bountyStr, out var bounty))
        {
            if (bounty < 0) 
            {
                Console.WriteLine("Hiba: a bounty értékének pozitív számnak kell lennie (millió Berry).");
                return;
            }
            Console.WriteLine("Hiba: a bounty értékének pozitív számnak kell lennie (millió Berry).");
            return;
        }

        if (!int.TryParse(ageStr, out var age))
        {
            if (age<0) 
            {
                Console.WriteLine("Hiba: az életkor értékének pozitív számnak kell lennie.");
                return;
            }
            Console.WriteLine("Hiba: az életkor értékének pozitív számnak kell lennie.");
            return;
        }

        if (!int.TryParse(strengthStr, out var strength))
        {
            if (strength < 1 || strength > 100) 
            {
                Console.WriteLine("Hiba: az erősség értékének számnak kell lennie (1–100).");
                return;
            }
            Console.WriteLine("Hiba: az erősség értékének számnak kell lennie (1–100).");
            return;
        }


        var member = new CrewMember(name, role, bounty, age, strength);
        _service.Add(member);

        if (_storage.Save(_service.ListAll().ToList()))
            Console.WriteLine("Tag sikeresen hozzáadva és mentve.");
        else
            Console.WriteLine("Hiba: a tag hozzáadva, de mentés nem sikerült.");
    }
}
