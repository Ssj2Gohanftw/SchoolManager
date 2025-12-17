using SchoolManager.Data;
using SchoolManager.Data.Repositories.Interfaces;
using SchoolManager.Models.Dtos.Student;
using SchoolManager.Models.Entities;
using SchoolManager.Services.Interfaces;

namespace SchoolManager.Services
{
    public class StudentServices : IStudentServices
    {
        private readonly IStudentRepository _studentRepository;
        public StudentServices(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }


        public async Task<Student> AddStudentAsync(AddStudentDto addStudentDto, CancellationToken cancellationToken)
        {
            var student = new Student()
            {
                FirstName = addStudentDto.FirstName,
                LastName = addStudentDto.LastName,
                Email = addStudentDto.Email,
                DateOfBirth = addStudentDto.DateOfBirth.ToUniversalTime()
            };
            await _studentRepository.AddAsync(student, cancellationToken);
            await _studentRepository.SaveChangesAsync(cancellationToken);
            return student;
        }


        public async Task<bool> DeleteStudentAsync(Guid id, CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetByIdAsync(id, cancellationToken);
            if (student is null)
            {
                return false;
            }
            _studentRepository.Remove(student);
            await _studentRepository.SaveChangesAsync(cancellationToken);
            return true;
        }


        public async Task<IEnumerable<Student>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var student = await _studentRepository.GetAllAsync(cancellationToken);
            return student;
        }

        public Task<Student?> GetStudentByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var student = _studentRepository.GetByIdAsync(id, cancellationToken);
            return student;
        }


        public async Task<bool> UpdateStudentAsync(Guid id, UpdateStudentDto updateStudentDto, CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetByIdAsync(id,cancellationToken);
            if (student is null)
            {
                return false;
            }
            student.FirstName = updateStudentDto.FirstName;
            student.LastName = updateStudentDto.LastName;
            student.DateOfBirth = updateStudentDto.DateOfBirth;
            student.Email = updateStudentDto.Email;
            _studentRepository.Update(student);
            await _studentRepository.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
