using Microsoft.EntityFrameworkCore;
using SchoolManager.Data.Repositories.Interfaces;
using SchoolManager.Dtos.Common;
using SchoolManager.Dtos.Student;
using SchoolManager.Dtos.StudentClass;
using SchoolManager.Mappers.Common;
using SchoolManager.Mappers.Students;
using SchoolManager.Models;
using SchoolManager.Services.Interfaces;

namespace SchoolManager.Services
{
    public class StudentServices : IStudentServices
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IClassRepository _classRepository;
        public StudentServices(
            IStudentRepository studentRepository,
            IClassRepository classRepository,
            IFeeRepository feeRepository)
        {
            _studentRepository = studentRepository;
            _classRepository = classRepository;
        }

        public async Task<Student?> AddStudentAsync(AddStudentDto addStudentDto)
        {
            Student student = addStudentDto.ToStudent();
            await _studentRepository.AddAsync(student);
            return student;
        }

        public async Task<bool> AssignStudentToClassAsync(Guid studentId, AssignStudentClassDto assignStudentClassDto)
        {
            Student? student = await _studentRepository.GetByIdAsync(studentId);
            if (student == null)
            {
                return false;
            }
            Class? _class = await _classRepository.GetByNameAsync(assignStudentClassDto.ClassName!);
            student.ClassId = _class?.ClassId;
            await _studentRepository.Update(student);
            return true;
        }
        
        public async Task<bool> DeleteStudentAsync(Guid id)
        {
            Student? student = await _studentRepository.GetByIdAsync(id);
            if (student == null)
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
            List<Student> student = await _studentRepository.GetAllAsync();
            return student.Select(s => s.ToStudentDto()).ToList();
        }

        public async Task<OldestStudentDto> GetOldestStudentAsync()
        {
            var student = await _studentRepository.GetOldestStudentAsync();
            return student.ToOldestStudentDto();
        }

        public async Task<PagedResults<StudentDetailsDto>> GetPagedStudentsAsync(StudentQueryDto studentQueryDto)
        {
            try
            {
                PagedResults<Student> result = await _studentRepository.GetPagedAsync(studentQueryDto);
                List<StudentDetailsDto> studentDetails = result.Results.Select(s => s.ToStudentDetailsDto()).ToList();
                return studentDetails.ToPagedResults(result.PageNumber, result.PageSize, result.TotalCount);
            }
            catch (Exception)
            {

                throw new Exception("Something went wrong!") ;
            }
            
        }

        public async Task<Student?> GetStudentByIdAsync(Guid id)
        {
            Student? student = await _studentRepository.GetByIdAsync(id);
            return student;
        }

        public async Task<List<StudentHobbiesDto>> GetStudentHobbies()
        {
            List<Student> student = await _studentRepository.GetHobbies();
            return student.Select(s => s.ToStudentHobbiesDto()).ToList();
        }

        public async Task<bool> UpdateStudentAsync(Guid id, UpdateStudentDto updateStudentDto)
        {
            Student? student = await _studentRepository.GetByIdAsync(id);
            if (student == null)
            {
                return false;
            }
            student.ToUpdateStudent(updateStudentDto);
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
