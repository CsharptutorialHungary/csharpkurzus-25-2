namespace OnePieceCrewManager.Core.Services
{
    public interface ICrewValidatorService
    {
        void ValidateNew(CrewMember member, IReadOnlyList<CrewMember> existingCrew);

        void ValidateUpdate(CrewMember member, IReadOnlyList<CrewMember> existingCrew, string name);
    }
}
