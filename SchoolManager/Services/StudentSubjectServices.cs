using SchoolManager.Data.Repositories.Interfaces;
using SchoolManager.Dtos.StudentSubject;
using SchoolManager.Models.Entities;
using SchoolManager.Services.Interfaces;

namespace SchoolManager.Services
{
    public class StudentSubjectServices : IStudentSubjectServices
    {
        private readonly IStudentSubjectRepository _studentSubjectRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IClassRepository _classRepository;

        public StudentSubjectServices(
            IStudentSubjectRepository studentSubjectRepository,
            IStudentRepository studentRepository,
            ISubjectRepository subjectRepository,
            IClassRepository classRepository)
        {
            _studentSubjectRepository = studentSubjectRepository;
            _studentRepository = studentRepository;
            _subjectRepository = subjectRepository;
            _classRepository = classRepository;
        }

        public async Task AssignSubjectToStudentAsync(AddStudentSubjectDto dto)
        {
            var student = await _studentRepository.GetByIdAsync(dto.StudentId);
            var subject = await _subjectRepository.GetByIdAsync(dto.SubjectId);

            if (student is null || subject is null)
            {
                throw new InvalidOperationException("Student or Subject doesn't exist");
            }

            if (await _studentSubjectRepository.ExistsAsync(dto.StudentId, dto.SubjectId))
            {
                return;
            }

            await _studentSubjectRepository.AddAsync(new StudentSubject
            {
                StudentId = dto.StudentId,
                SubjectId = dto.SubjectId
            });
        }

        public Task<List<Subject>> GetSubjectsForStudentAsync(Guid studentId)
            => _studentSubjectRepository.GetSubjectsForStudentAsync(studentId);

        public async Task<bool> RemoveSubjectFromStudentAsync(DeleteStudentSubjectDto dto)
            => await _studentSubjectRepository.RemoveAsync(dto.StudentId, dto.SubjectId);

        public async Task AssignSubjectsToClassAsync(Guid classId, AssignSubjectsToClassDto dto)
        {
            if (dto is null)
            {
                throw new InvalidOperationException("Subjects can't be empty");
            }

            var subjectIds = dto.SubjectIds
                .Where(id => id != Guid.Empty)
                .Distinct()
                .ToList();

            if (subjectIds.Count == 0)
            {
                return;
            }

            var @class = await _classRepository.GetByIdAsync(classId);
            if (@class is null)
            {
                throw new InvalidOperationException("Class not found");
            }

            // Validate subjects exist
            foreach (var subjectId in subjectIds)
            {
                var subject = await _subjectRepository.GetByIdAsync(subjectId);
                if (subject is null)
                {
                    throw new InvalidOperationException("Subject not found");
                }
            }

           
            foreach (var student in @class.Students)
            {
                foreach (var subjectId in subjectIds)
                {
                    if (student.StudentSubjects.Any(ss => ss.SubjectId == subjectId))
                    {
                        continue;
                    }

                    await _studentSubjectRepository.AddAsync(new StudentSubject
                    {
                        StudentId = student.StudentId,
                        SubjectId = subjectId
                    });
                }
            }
        }
    }
}
