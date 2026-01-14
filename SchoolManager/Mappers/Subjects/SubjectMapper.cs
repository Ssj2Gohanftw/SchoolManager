using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SchoolManager.Dtos.Common;
using SchoolManager.Dtos.Subject;
using SchoolManager.Mappers.Classes;
using SchoolManager.Mappers.Teachers;
using SchoolManager.Models.Entities;
namespace SchoolManager.Mappers.Subjects
{
    public static class SubjectMapper
    {
        public static SubjectSummaryDto ToSubjectSummaryDto(this Subject subject)
        {
            return new SubjectSummaryDto
            {
                SubjectId = subject.SubjectId,
                Name = subject.Name
            };
        }
        public static SubjectDetailsDto ToSubjectDetailsDto(this Subject subject)
        {
            return new SubjectDetailsDto
            {
                SubjectId = subject.SubjectId,
                Name = subject.Name,
                Classes = subject.SubjectTeachers.Select(c => c.Class.ToClassesDto()).ToList(),
                Teachers = subject.SubjectTeachers.Select(t => t.Teacher.ToTeacherDto()).ToList()
            };
        }
        public static SubjectQueryDto ToSubjectQueryDto(this SubjectQueryDto subjectQueryDto,FilterBy filter,int pageNumber,int pageSize,string? search)
        {
            return new SubjectQueryDto
            {
                FilterBy = filter,
                Search = search,
                SortBy = subjectQueryDto?.SortBy ?? SubjectSortBy.Name,
                SortOrder = subjectQueryDto?.SortOrder ?? SortOrder.Ascending,
                PageNumber = pageNumber,
                PageSize = pageSize,
            };
        }
        public static Subject ToSubject(this AddSubjectDto addSubjectDto)
        {
            return new Subject
            {
                Name = addSubjectDto.Name
            };
        }
        public static void ToUpdateSubject(this UpdateSubjectDto updateSubjectDto, Subject subject)
        {

            subject.Name = updateSubjectDto.Name;
        }
    }
}