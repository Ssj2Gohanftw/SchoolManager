using Microsoft.EntityFrameworkCore;
using SchoolManager.Data.Repositories.Interfaces;
using SchoolManager.Dtos.Common;
using SchoolManager.Dtos.Student;
using SchoolManager.Mappers.Students;
using SchoolManager.Models.Entities;
using SchoolManager.Services.Interfaces;

namespace SchoolManager.Services
{
    public class StudentServices : IStudentServices
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IClassRepository _classRepository;

        public StudentServices(IStudentRepository studentRepository, IClassRepository classRepository)
        {
            _studentRepository = studentRepository;
            _classRepository = classRepository;
        }

        public async Task<Student?> AddStudentAsync(AddStudentDto addStudentDto)
        {
            var @class = await _classRepository.GetByNameAsync(addStudentDto.ClassName.Trim());
            if (@class is null)
            {
                throw new InvalidOperationException("Class doesn't exist");
            }
            //var existingStudent=await _studentRepository.getna
            var student = addStudentDto.ToStudent(@class.ClassId);
            await _studentRepository.AddAsync(student);
            return student;
        }

        public async Task<bool> DeleteStudentAsync(Guid id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            if (student is null)
            {
                return false;
            }
            try
            {
                await _studentRepository.Remove(student);
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
                
            }
        }  

        public async Task<List<StudentDto>> GetAllAsync()
        {
            var student = await _studentRepository.GetAllAsync();
            return student.Select(s=>s.ToStudentDto()).ToList();
        }

        public async Task<PagedResults<StudentDetailsDto>> GetPagedStudentsAsync(StudentQueryDto studentQueryDto)
        {
            studentQueryDto = studentQueryDto.Normalize();
            var result = await _studentRepository.GetPagedAsync(studentQueryDto);
            return new PagedResults<StudentDetailsDto>
            {
                Results = result.Results.Select(s => s.ToStudentDetailsDto()).ToList(),
                PageNumber = result.PageNumber,
                PageSize = result.PageSize,
                TotalCount = result.TotalCount
            };
        }

        public Task<Student?> GetStudentByIdAsync(Guid id)
        {
            var student = _studentRepository.GetByIdAsync(id);
            return student;
        }

        public async Task<bool> UpdateStudentAsync(Guid id, UpdateStudentDto updateStudentDto)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            if (student is null)
            {
                return false;
            }

            var @class = await _classRepository.GetByNameAsync(updateStudentDto.ClassName);
            if (@class is null)
            {
                throw new InvalidOperationException("Class doesn't exist");
            }

            updateStudentDto.ToUpdateStudent(student, @class.ClassId);
            try
            {
                await _studentRepository.Update(student);
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }
    }
}
