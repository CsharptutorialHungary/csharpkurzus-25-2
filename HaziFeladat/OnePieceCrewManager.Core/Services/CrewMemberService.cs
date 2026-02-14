namespace OnePieceCrewManager.Core.Services
{
    public class CrewMemberService
    {

        private readonly ICrewRepositoryService _repository;
        private readonly ICrewValidatorService _validator;

        public CrewMemberService(ICrewRepositoryService repository, ICrewValidatorService validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public void Add(CrewMember member)
        {
            _validator.ValidateNew(member, _repository.ListAll());
            _repository.Add(member);
        }

        public IReadOnlyList<CrewMember> ListAll() => _repository.ListAll();

        public CrewMember? FindByName(string name) => _repository.FindByName(name);

        public void Update(CrewMember member, string name)
        {            
            _validator.ValidateUpdate(member, _repository.ListAll(),name);            
            _repository.Update(member, name);
        }

        public void Delete(CrewMember member)
        {
            _repository.Delete(member);
        }
    }
}
