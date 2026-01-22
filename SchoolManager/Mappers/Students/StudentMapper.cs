using SchoolManager.Dtos.Student;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SchoolManager.Dtos.Common;
using SchoolManager.Models;
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
                Gender=student.Gender,
                DateOfBirth = student.DateOfBirth,
                Email = student.Email,
                ClassId = student.ClassId,
                ClassName = student.Class?.Name,
                Branch=student.Class.Branch.ToString(),
                AdditionalInfo = student.AdditionalInfo,
                //Subjects = student.StudentSubjects?.Select(ss => ss.ToStudentSubjectDto()).ToList() ?? new List<StudentSubjectDto>()
                Subjects = student?.Class?
                .SubjectClasses?
                .Select(ss=>ss.ToStudentSubjectDto())
                .ToList() 
            };

        }
        public static StudentHobbiesDto ToStudentHobbiesDto(this Student student)
        {
            return new StudentHobbiesDto()
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                Hobbies = student.AdditionalInfo?.Hobbies
            };

        }
        public static OldestStudentDto ToOldestStudentDto(this Student student)
        {
            var today = DateTime.Today;
            var age = today.Year - student.DateOfBirth.Year;
            return new OldestStudentDto()
            {
                StudentId = student.StudentId,
                Name = student.FirstName + " " + student.LastName,
                Age = age,
                ClassName = student?.Class.Name
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
                Gender=addStudent.Gender,
                DateOfBirth = addStudent.DateOfBirth,
                Email = addStudent.Email,
                AdditionalInfo = addStudent.AdditionalInfo
            };

        }
        public static void ToUpdateStudent(this Student student, UpdateStudentDto updateStudentDto)
        {
            if (updateStudentDto.FirstName != null) student.FirstName = updateStudentDto.FirstName;
            if (updateStudentDto.LastName != null) student.LastName = updateStudentDto.LastName;
            if (updateStudentDto.DateOfBirth.HasValue) student.DateOfBirth = updateStudentDto.DateOfBirth.Value;
            if (updateStudentDto.Email != null) student.Email = updateStudentDto.Email;
            if (updateStudentDto.AdditionalInfo != null) student.AdditionalInfo = updateStudentDto.AdditionalInfo;
        }
        }
    }
