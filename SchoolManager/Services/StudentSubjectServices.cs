using SchoolManager.Data.Repositories.Interfaces;
using SchoolManager.Models.Dtos.StudentSubject;
using SchoolManager.Models.Entities;
using SchoolManager.Services.Interfaces;

namespace SchoolManager.Services
{
    public class StudentSubjectServices : IStudentSubjectServices
    {
        private readonly IStudentSubjectRepository _studentSubjectRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ISubjectRepository _subjectRepository;

        public StudentSubjectServices(
            IStudentSubjectRepository studentSubjectRepository,
            IStudentRepository studentRepository,
            ISubjectRepository subjectRepository)
        {
            _studentSubjectRepository = studentSubjectRepository;
            _studentRepository = studentRepository;
            _subjectRepository = subjectRepository;
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
    }
}
