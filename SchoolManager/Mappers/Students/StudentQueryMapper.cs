using SchoolManager.Dtos.Student;
using SchoolManager.Extensions;
namespace SchoolManager.Mappers.Students
{
    public static class StudentQueryMapper
    {
        public static StudentQueryDto Normalize(this StudentQueryDto studentQueryDto)
        {

            return studentQueryDto.QueryNormalize((
                dto,
                pageNumber,
                pageSize,
                filterBy,
                search) =>
                    dto.ToStudentQueryDto(
                    pageNumber,
                    pageSize,
                    filterBy,
                    search));
        }
    }
}
