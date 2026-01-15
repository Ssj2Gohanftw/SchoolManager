using SchoolManager.Dtos.Class;
using SchoolManager.Extensions;

namespace SchoolManager.Mappers.Classes
{
    public static class ClassQueryMapper
    {
        public static ClassQueryDto Normalize(this ClassQueryDto classQueryDto)
        {
            return classQueryDto.QueryNormalize((dto,pageNumber,pageSize,FilterBy,search)=> 
                dto.ToClassQueryDto(pageNumber, pageSize, FilterBy, search));
        }
    }
}
