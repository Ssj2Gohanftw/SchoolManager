using SchoolManager.Data.Repositories.Interfaces;
using SchoolManager.Dtos.SubjectClass;
using SchoolManager.Mappers;
using SchoolManager.Models;
using SchoolManager.Services.Interfaces;


namespace SchoolManager.Services
{
    public class SubjectClassServices : ISubjectClassServices
    {
        private readonly ISubjectClassRepository _subjectClassRepository;
        private readonly IClassRepository _classRepository;
        private readonly ISubjectRepository _subjectRepository;

        public SubjectClassServices(ISubjectClassRepository subjectClassRepository,
            IClassRepository classRepository,
            ISubjectRepository subjectRepository)
        {
            _subjectClassRepository = subjectClassRepository;
            _subjectRepository = subjectRepository;
            _classRepository = classRepository;
        }
        public async Task<List<SubjectClassDto>> AssignSubjects(AddSubjectClassDto addSubjectClassDto)
        {
            Class? _class = await _classRepository.GetByIdAsync(addSubjectClassDto.ClassId);
            if (_class == null)
            {
                throw new Exception("Class not found");
            }
            List<Guid> subjectsIdToAssign = addSubjectClassDto.SubjectId
                .Where(subId => subId != Guid.Empty)
                .Distinct()
                .ToList();
            if(subjectsIdToAssign == null || subjectsIdToAssign.Count==0)
            {
                 throw new Exception("Subjects not found!");
            }
            List<Subject> existingSubjects = await _subjectRepository.GetAllAsync();
            if (existingSubjects == null)
            {
                throw new Exception("Subjects not found!");
            }
            HashSet<Guid> existingSubjectIds = existingSubjects
                .Select(sub => sub.SubjectId)
                .ToHashSet();

            List<Guid> missing = subjectsIdToAssign.Where(id => !existingSubjectIds.Contains(id)).ToList();
            if (missing.Count > 0)
                throw new Exception($"Subject(s) not found: {string.Join(", ", missing)}");

            await _subjectClassRepository.AssignSubjectsToClass(subjectsIdToAssign,addSubjectClassDto.ClassId);
            List<SubjectClass> assignments = await _subjectClassRepository
             .GetAllAsync();

            return assignments
                .Where(sc => sc.ClassId == addSubjectClassDto.ClassId && subjectsIdToAssign.Contains(sc.SubjectId))
                .Select(sc => sc.ToSubjectClassDto())
                .ToList();
        }
    }

}

