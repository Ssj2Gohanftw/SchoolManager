using SchoolManager.Models.Dtos.Teacher;

namespace SchoolManager.Models.Mappings.Teacher
{
    public static class TeacherMappings
    {
        public static TeacherSummaryDto ToTeacherSummaryDto(this Entities.Teacher teacher)
        {
            return new TeacherSummaryDto
            {
                TeacherId = teacher.TeacherId,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                Email = teacher.Email
            };
        }

        public static TeacherDetailsDto ToTeacherDetailsDto(this Entities.Teacher teacher)
        {
            return new TeacherDetailsDto
            {
                TeacherId = teacher.TeacherId,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                Email = teacher.Email,
                Assignments = teacher.SubjectTeachers.Select(st => new TeacherAssignmentDto
                {
                    ClassId = st.ClassId,
                    ClassName = st.Class.Name,
                    SubjectId = st.SubjectId,
                    SubjectName = st.Subject.Name
                }).ToList()
            };
        }
    }
}