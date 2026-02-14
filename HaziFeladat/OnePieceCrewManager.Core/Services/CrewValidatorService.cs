using OnePieceCrewManager.Core;
using OnePieceCrewManager.Core.Exceptions;
using OnePieceCrewManager.Core.Services;

public class CrewValidatorService : ICrewValidatorService
{
    public void ValidateNew(CrewMember member, IReadOnlyList<CrewMember> existingCrew)
    {
        if (existingCrew.Any(c => c.Name.Equals(member.Name, StringComparison.OrdinalIgnoreCase)))
            throw new CrewException($"Már létezik ilyen nevű tag: {member.Name}");

        CommonValidation(member, existingCrew);
    }

    public void ValidateUpdate(CrewMember member, IReadOnlyList<CrewMember> existingCrew, string originalName)
    {
        if (!member.Name.Equals(originalName, StringComparison.OrdinalIgnoreCase))
        {
            if (existingCrew.Any(c => c.Name.Equals(member.Name, StringComparison.OrdinalIgnoreCase)))
            {
                throw new CrewException($"Már létezik ilyen nevű tag: {member.Name}");
            }
        }
        CommonValidation(member, existingCrew);
    }

    private void CommonValidation(CrewMember member, IReadOnlyList<CrewMember> existingCrew)
    {
        if (member.Bounty < 0)
            throw new CrewException("A bounty nem lehet negatív.");

        if (member.StrengthLevel is < 1 or > 100)
            throw new CrewException("Az erősség 1 és 100 között kell legyen.");

        if (member.Role == Role.Captain && existingCrew.Any(c => c.Role == Role.Captain && !c.Name.Equals(member.Name, StringComparison.OrdinalIgnoreCase)))
            throw new CrewException("A legénységben csak egy kapitány lehet.");

        if (member.Role == Role.Helmsman && existingCrew.Any(c => c.Role == Role.Helmsman && !c.Name.Equals(member.Name, StringComparison.OrdinalIgnoreCase)))
            throw new CrewException("A legénységben csak egy kormányos lehet.");
    }
}

