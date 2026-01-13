using SchoolManager.Dtos.Student;
namespace SchoolManager.Mappers.Students
{
    public static class StudentQueryMapper
    {
        private const int DefaultPageNumber = 1;
        private const int DefaultPageSize = 20;
        private const int MaxPageSize = 200;

        public static StudentQueryDto Normalize(this StudentQueryDto studentQueryDto)
        {
            var pageNumber = studentQueryDto?.PageNumber ?? DefaultPageNumber;
            if (pageNumber < 1)
            {
                pageNumber = DefaultPageNumber;
            }

            var pageSize = studentQueryDto?.PageSize ?? DefaultPageSize;
            if (pageSize < 1)
            {
                pageSize = DefaultPageSize;
            }
            if (pageSize > MaxPageSize)
            {
                pageSize = MaxPageSize;
            }

            var search = studentQueryDto?.Search?.Trim();
            if (string.IsNullOrWhiteSpace(search))
            {
                search = null;
            }


            var filterBy = studentQueryDto?.FilterBy ?? StudentFilterBy.None;

            if (filterBy == StudentFilterBy.None)

            {
                if (search != null)
                {
                    filterBy = StudentFilterBy.Search;
                }
            }

            // Keep filter inputs consistent with selected filter option
            if (filterBy != StudentFilterBy.Search)
            {
                search = null;
            }

            return studentQueryDto.ToStudentQueryDto(pageNumber, pageSize,filterBy,search);
        }
    }
}
