using SchoolManager.Data.Repositories.Interfaces;
using SchoolManager.Dtos.SubjectClass;
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
            var _class = await _classRepository.GetByIdAsync(addSubjectClassDto.ClassId);
            if (_class == null)
            {
                throw new Exception("Class not found");
            }
            var subjectsIdToAssign = addSubjectClassDto.SubjectId
                .Where(subId => subId != Guid.Empty)
                .Distinct()
                .ToList();
            if(subjectsIdToAssign == null || subjectsIdToAssign.Count==0)
            {
                 throw new Exception("Subjects not found!");
            }
            var existingSubjects = await _subjectRepository.GetAllAsync();
            if (existingSubjects == null)
            {
                throw new Exception("Subjects not found!");
            }
            var existingSubjectIds = existingSubjects
                .Select(sub => sub.SubjectId)
                .ToHashSet();

            await _subjectClassRepository.AssignSubjectsToClass(subjectsIdToAssign,addSubjectClassDto.ClassId);
            var assignments = await _subjectClassRepository
             .GetAllAsync();

            return assignments
                .Where(sc => sc.ClassId == addSubjectClassDto.ClassId && subjectsIdToAssign.Contains(sc.SubjectId))
                .Select(sc => new SubjectClassDto
                {
                    SubjectId = sc.SubjectId,
                    ClassId = sc.ClassId,
                    SubjectName = sc.Subject?.Name,
                    ClassName = sc.Class?.Name 
                })
                .ToList();
        }
    }

}

