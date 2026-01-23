using SchoolManager.Dtos.Subject;
using SchoolManager.Extensions;

namespace SchoolManager.Mappers.Subjects
{
    public static class SubjectQueryMapper
    {
        public static SubjectQueryDto Normalize(this SubjectQueryDto subjectQueryDto)
        {
            return subjectQueryDto.QueryNormalize((dto,pageNumber,pageSize,FilterBy,search) =>
                dto.ToSubjectQueryDto(FilterBy, pageNumber, pageSize, search));        
        }
    }
}
