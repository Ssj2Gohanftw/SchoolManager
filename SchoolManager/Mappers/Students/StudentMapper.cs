using SchoolManager.Dtos.Student;
using SchoolManager.Models.Entities;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SchoolManager.Dtos.Common;
namespace SchoolManager.Mappers.Students
{
    public static class StudentMapper
    {
        public static StudentDto ToStudentDto(this Student student)
        {
            return new StudentDto()
            {
                StudentId = student.StudentId,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,
                ClassName = student.Class?.Name
            };

        }
        public static StudentDetailsDto ToStudentDetailsDto(this Student student)
        {
            return new StudentDetailsDto()
            {
                StudentId = student.StudentId,
                FirstName = student.FirstName,
                LastName = student.LastName,
                DateOfBirth = student.DateOfBirth,
                Email = student.Email,
                ClassId = student.ClassId,
                ClassName = student.Class?.Name,
                AdditionalInfo = student.AdditionalInfo,
                Subjects = student.StudentSubjects?.Select(ss => ss.ToStudentSubjectDto()).ToList() ?? new List<StudentSubjectDto>()
            };

        }
        public static StudentQueryDto ToStudentQueryDto(this StudentQueryDto studentQueryDto,int pageNumber,int pageSize,FilterBy filter,string? search) 
        {
            return new StudentQueryDto
            {
                FilterBy = filter,
                Search = search,
                SortBy = studentQueryDto?.SortBy ?? StudentSortBy.FirstName,
                SortOrder = studentQueryDto?.SortOrder ?? SortOrder.Ascending,
                PageNumber = pageNumber,
                PageSize = pageSize,
            };
        }
        public static Student ToStudent(this AddStudentDto addStudent)
        {
            return new Student()
            {
                FirstName = addStudent.FirstName,
                LastName = addStudent.LastName,
                Email = addStudent.Email,
                DateOfBirth = addStudent.DateOfBirth,
                AdditionalInfo = addStudent.AdditionalInfo
            };

        }
        public static void ToUpdateStudent(this UpdateStudentDto updateStudentDto, Student student, Guid classId)
        {

            student.FirstName = updateStudentDto.FirstName;
            student.LastName = updateStudentDto.LastName;
            student.Email = updateStudentDto.Email;
            student.DateOfBirth = updateStudentDto.DateOfBirth;
            student.ClassId = classId;
            if (updateStudentDto.AdditionalInfo != null)
            {
                student.AdditionalInfo = updateStudentDto.AdditionalInfo;
            }
        }
    }
}
