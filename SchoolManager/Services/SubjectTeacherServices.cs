using SchoolManager.Data.Repositories.Interfaces;
using SchoolManager.Dtos.SubjectTeacher;
using SchoolManager.Mappers;
using SchoolManager.Models;
using SchoolManager.Services.Interfaces;

namespace SchoolManager.Services
{
    public class SubjectTeacherServices :ISubjectTeacherServices
    {
        private readonly ISubjectTeacherRepository _subjectTeacherRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IClassRepository _classRepository;
        private readonly ISubjectRepository _subjectRepository;
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
            Teacher? teacherExists = await _teacherRepository.GetByIdAsync(addSubjectTeacherDto.TeacherId);
            if (teacherExists == null)
            {
                throw new InvalidOperationException("Teacher not found");
            }
            Class? classExists = await _classRepository.GetByIdAsync(addSubjectTeacherDto.ClassId);

            if (classExists == null)
            {
                throw new InvalidOperationException("Class not found");
            }
            Subject? subjectExists = await _subjectRepository.GetByIdAsync(addSubjectTeacherDto.SubjectId);
            if (subjectExists == null)
            {
                throw new InvalidOperationException("Subject not found");
            }

            SubjectTeacher assignment = addSubjectTeacherDto.ToSubjectTeacher();
            await _subjectTeacherRepository.AddAsync(assignment);
        }

        public async Task<List<SubjectTeacherDto>> GetAssignmentsForClassAsync(Guid classId)
        {
            List<SubjectTeacher> assignments = await _subjectTeacherRepository.GetAssignmentsForClass(classId);
            return assignments.Select(a =>
            a.ToSubjectTeacherDto()
            ).ToList();
        }

        public async Task<List<SubjectTeacherDto>> GetAssignmentsForSubjectAsync(Guid subjectId)
        {
            List<SubjectTeacher> assignments = await _subjectTeacherRepository.GetAssignmentsForSubject(subjectId);
            return assignments.Select(a =>
            a.ToSubjectTeacherDto()
            ).ToList();

        }

        public async Task<List<SubjectTeacherDto>> GetAssignmentsForTeacherAsync(Guid teacherId)
        {
            List<SubjectTeacher> assignments = await _subjectTeacherRepository.GetAssignmentsForTeacher(teacherId);
            return assignments.Select(a =>
            a.ToSubjectTeacherDto()
            ).ToList();
        }

        public async Task<bool> UnassignAsync(DeleteSubjectTeacherDto deleteSubjectTeacherDto)
        {
            SubjectTeacher unassignTeacher = deleteSubjectTeacherDto.ToUnassignSubjectTeacher();
            return await _subjectTeacherRepository.Remove(unassignTeacher);
        }
    }
}
