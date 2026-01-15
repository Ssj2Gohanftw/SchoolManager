using SchoolManager.Dtos.Teacher;
using SchoolManager.Extensions;

namespace SchoolManager.Mappers.Teachers
{
    public static class TeacherQueryMapper
    {
        public static TeacherQueryDto Normalize(this TeacherQueryDto teacherQueryDto)
        {
            return teacherQueryDto.QueryNormalize(
            (dto, pageNumber, pageSize, FilterBy, search) =>
                dto.ToTeacherQueryDto(FilterBy, pageNumber, pageSize, search)
            );
        }
    }
}
