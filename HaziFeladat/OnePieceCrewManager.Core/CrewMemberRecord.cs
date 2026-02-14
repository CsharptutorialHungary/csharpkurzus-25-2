namespace OnePieceCrewManager.Core
{

    public record CrewMember(
        string Name,
        Role Role,
        int Bounty,       // millió Berry
        int Age,
        int StrengthLevel // 1–100
    );

}
