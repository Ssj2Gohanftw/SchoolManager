using SchoolManager.Dtos.Student;
using SchoolManager.Models.Entities;

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
                //Subjects=student.StudentSubjects?.Select(ss=>ss.ToStudentSubjectDto()).ToList()??new List<StudentSubjectDto>()
            };

        }
        public static Student ToStudent(this AddStudentDto addStudent,Guid classId)
        {
            return new Student()
            {
                FirstName = addStudent.FirstName,
                LastName = addStudent.LastName,
                Email = addStudent.Email,
                DateOfBirth=addStudent.DateOfBirth,
                ClassId = classId
            };

        }
        public static void ToUpdateStudent(this UpdateStudentDto updateStudentDto,Student student, Guid classId)
        {

            student.FirstName = updateStudentDto.FirstName;
            student.LastName = updateStudentDto.LastName;
            student.Email = updateStudentDto.Email;
            student.DateOfBirth = updateStudentDto.DateOfBirth;
            student.ClassId = classId;
        }
    }
}
