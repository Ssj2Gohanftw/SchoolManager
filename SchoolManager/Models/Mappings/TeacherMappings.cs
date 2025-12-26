using SchoolManager.Models.Dtos.Teacher;
using SchoolManager.Models.Entities;

namespace SchoolManager.Models.Mappings
{
    public static class TeacherMappings
    {
        public static TeacherSummaryDto ToTeacherSummaryDto(this Teacher teacher)
        {
            return new TeacherSummaryDto
            {
                TeacherId = teacher.TeacherId,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                Email = teacher.Email
            };
        }
    }
}