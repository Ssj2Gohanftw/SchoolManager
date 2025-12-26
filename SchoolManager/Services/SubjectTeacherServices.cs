using SchoolManager.Data.Repositories.Interfaces;
using SchoolManager.Models.Dtos.SubjectTeacher;
using SchoolManager.Models.Entities;
using SchoolManager.Models.Mappings;
using SchoolManager.Services.Interfaces;

namespace SchoolManager.Services
{
    public class SubjectTeacherServices : ISubjectTeacherServices
    {
        private readonly ISubjectTeacherRepository _subjectTeacherRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IClassRepository _classRepository;
        private readonly  ISubjectRepository _subjectRepository;
        public SubjectTeacherServices(
            ISubjectTeacherRepository subjectTeacherRepository,
            ITeacherRepository teacherRepository,
            IClassRepository classRepository,
            ISubjectRepository subjectRepository
            )
        {
            _subjectTeacherRepository = subjectTeacherRepository;
            _teacherRepository = teacherRepository;
            _classRepository = classRepository;
            _subjectRepository = subjectRepository;
        }
        public async Task AssignAsync(AddSubjectTeacherDto addSubjectTeacherDto)
        {
            var teacherExists= await _teacherRepository.GetByIdAsync(addSubjectTeacherDto.TeacherId);
            if(teacherExists is null)
            {
                throw new InvalidOperationException("Teacher not found");
            }
            var classExists = await _classRepository.GetByIdAsync(addSubjectTeacherDto.ClassId);
            
            if (classExists is  null)
            {
                throw new InvalidOperationException("Class not found");
            }
            var subjectExists = await _subjectRepository.GetByIdAsync(addSubjectTeacherDto.SubjectId);
            if (subjectExists is null)
            {
                throw new InvalidOperationException("Subject not found");
            }

            var assignment=new SubjectTeacher
            {
                TeacherId=addSubjectTeacherDto.TeacherId,
                ClassId=addSubjectTeacherDto.ClassId,
                SubjectId=addSubjectTeacherDto.SubjectId
            };
            await _subjectTeacherRepository.AddAsync(assignment);
        }

        public async Task<List<SubjectTeacherDto>> GetAssignmentsForClassAsync(Guid classId)
        {
            var assignments = await _subjectTeacherRepository.GetAssignmentsForClass(classId);
            return assignments.Select(a =>
            a.ToSubjectTeacherDto()
            ).ToList();
        }

        public async Task<List<SubjectTeacherDto>> GetAssignmentsForSubjectAsync(Guid subjectId)
        {
            var assignments = await _subjectTeacherRepository.GetAssignmentsForSubject(subjectId);
            return assignments.Select(a =>
            a.ToSubjectTeacherDto()
            ).ToList();

        }

        public async Task<List<SubjectTeacherDto>> GetAssignmentsForTeacherAsync(Guid teacherId)
        {
            var assignments = await _subjectTeacherRepository.GetAssignmentsForTeacher(teacherId);
            return assignments.Select(a =>
            a.ToSubjectTeacherDto()
            ).ToList();
        }

        public async Task<bool> UnassignAsync(DeleteSubjectTeacherDto subjectTeacherDto)
        {
            var unassignTeacher = new SubjectTeacher
            {
                TeacherId = subjectTeacherDto.TeacherId,
                ClassId = subjectTeacherDto.ClassId,
                SubjectId = subjectTeacherDto.SubjectId
            };
            return await _subjectTeacherRepository.Remove(unassignTeacher);
        }
    }
}
