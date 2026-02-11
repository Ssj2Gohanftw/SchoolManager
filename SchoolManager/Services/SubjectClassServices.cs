using SchoolManager.Data.Repositories.Interfaces;
using SchoolManager.Dtos.Subject;
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

        public async Task<List<SubjectClass>> AssignSubjects(AddSubjectClassDto addSubjectClassDto)
        {
            Class? _class = await _classRepository.GetByIdAsync(addSubjectClassDto.ClassId);
            if (_class == null)
            {
                throw new Exception("Class not found");
            }
            List<Guid> existingSubjectAssignments = await _subjectClassRepository.GetExistingSubAssignmentsForClass(addSubjectClassDto.ClassId);
            if (existingSubjectAssignments == null || existingSubjectAssignments.Count==0)
            {
                throw new Exception("Subjects not found!");
            }
          
            List<Guid> subjectsToAssign = addSubjectClassDto.SubjectId
                .Where(subId => subId != Guid.Empty)
                .Except(existingSubjectAssignments)
                .Distinct()
                .ToList();
            if (subjectsToAssign.Count == 0)
            {
                throw new Exception("Subjects not Found");
            }
            List<SubjectClass> assignments = subjectsToAssign
                .Select(subId => subId.ToSubjectClass(addSubjectClassDto.ClassId)).ToList();
            await _subjectClassRepository.AddRangeAsync(assignments);
            return assignments;
        }

        public async Task<List<SubjectSummaryDto>> GetAssignmentDetailsForClassAsync(Guid classId)
        {
            var assignments = await _subjectClassRepository.GetAllAssignmentDetailsForClass(classId);
            return assignments.Select(sc=>sc.ToSubjectSummaryDto()).ToList();
        }
        //public async Task<List<SubjectClassDto>> AssignSubjects(AddSubjectClassDto addSubjectClassDto)
        //{
        //    Class? _class = await _classRepository.GetByIdAsync(addSubjectClassDto.ClassId);
        //    if (_class == null)
        //    {
        //        throw new Exception("Class not found");
        //    }
        //    List<Guid> subjectsIdToAssign = addSubjectClassDto.SubjectId
        //        .Where(subId => subId != Guid.Empty)
        //        .Distinct()
        //        .ToList();
        //    if (subjectsIdToAssign == null || subjectsIdToAssign.Count == 0)
        //    {
        //        throw new Exception("Subjects not found!");
        //    }
        //    List<Subject> existingSubjects = await _subjectRepository.GetAllAsync();
        //    if (existingSubjects == null)
        //    {
        //        throw new Exception("Subjects not found!");
        //    }

        //    await _subjectClassRepository.AssignSubjectsToClass(subjectsIdToAssign, addSubjectClassDto.ClassId);
        //    List<SubjectClass> assignments = await _subjectClassRepository
        //     .GetAllAsync();

        //    return assignments
        //        .Where(sc => sc.ClassId == addSubjectClassDto.ClassId && subjectsIdToAssign.Contains(sc.SubjectId))
        //        .Select(sc => sc.ToSubjectClassDto())
        //        .ToList();
        //}

    }

}

