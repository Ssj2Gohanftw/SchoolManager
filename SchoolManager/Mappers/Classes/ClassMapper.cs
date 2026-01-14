using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SchoolManager.Dtos.Class;
using SchoolManager.Dtos.Common;
using SchoolManager.Dtos.Student;
using SchoolManager.Models.Entities;
namespace SchoolManager.Mappers.Classes
{
    public static class ClassMapper
    {
        public static StudentClassDto ToStudentClassDto(this Student student)
        {
            return new StudentClassDto
            {
                StudentId = student.StudentId,
                FirstName = student.FirstName,
                LastName = student.LastName
            };
        }

        public static ClassesDto ToClassesDto(this Class _class)
        {
            return new ClassesDto
            {
                ClassId = _class.ClassId,
                Name = _class.Name,
                //Students = _class.Students.Select(s => s.ToStudentClassDto()).ToList()
            };
        }
        public static ClassDetailsDto ToClassDetailsDto(this Class _class)
        {
            return new ClassDetailsDto
            {
                ClassId = _class.ClassId,
                Name = _class.Name,
                Students = _class.Students.Select(s => s.ToStudentClassDto()).ToList()
            };
        }
        public static ClassQueryDto ToClassQueryDto(this ClassQueryDto classQueryDto, int pageNumber, int pageSize, FilterBy filter, string? search)
        {
            return new ClassQueryDto
            {
                FilterBy = filter,
                Search = search,
                SortBy = classQueryDto?.SortBy ?? ClassSortBy.Name,
                SortOrder = classQueryDto?.SortOrder ?? SortOrder.Ascending,
                PageNumber = pageNumber,
                PageSize = pageSize,
            };
        }
        public static Class ToClass(this AddClassDto addClassDto)
        {
            return new Class()
            {
                Name = addClassDto.Name
            };
        }
        public static void ToUpdateClass(this UpdateClassDto updateClassDto, Class _class)
        {
            _class.Name = updateClassDto.Name.Trim();
        }
    }
}
