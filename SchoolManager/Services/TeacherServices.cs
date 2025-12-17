using SchoolManager.Data.Repositories.Interfaces;
using SchoolManager.Models.Dtos.Teacher;
using SchoolManager.Models.Entities;
using SchoolManager.Services.Interfaces;

namespace SchoolManager.Services
{
    public class TeacherServices : ITeacherService
    {
        private readonly ITeacherRepository _teacherRepository;
        public TeacherServices(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }
        public async Task<Teacher> AddTeacherAsync(AddTeacherDto addTeacherDto, CancellationToken cancellationToken = default)
        {
            var teachers = new Teacher()
            {
                FirstName = addTeacherDto.FirstName,
                LastName = addTeacherDto.LastName,
                Email = addTeacherDto.Email
            };
            await _teacherRepository.AddAsync(teachers, cancellationToken);
            await _teacherRepository.SaveChangesAsync(cancellationToken);
            return teachers;
        }

        public async Task<bool> DeleteTeacherAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var teacher = await _teacherRepository.GetByIdAsync(id, cancellationToken);
            if (teacher is null)
            {
                return false;
            }
            _teacherRepository.Remove(teacher);
            await _teacherRepository.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<IEnumerable<Teacher>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var teachers = await _teacherRepository.GetAllAsync(cancellationToken);
            return teachers;
        }

        public async Task<Teacher?> GetStudentByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var teacher = await _teacherRepository.GetByIdAsync(id, cancellationToken);
            return teacher;
        }

        public async Task<bool> UpdateTeacherAsync(Guid id, UpdateTeacherDto updateTeacherDto, CancellationToken cancellationToken = default)
        {
            var teacher = await _teacherRepository.GetByIdAsync(id, cancellationToken);
            if (teacher is null)
            {
                return false;
            }
            teacher.FirstName = updateTeacherDto.FirstName;
            teacher.LastName = updateTeacherDto.LastName;
            teacher.Email= updateTeacherDto.Email;
            await _teacherRepository.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}

