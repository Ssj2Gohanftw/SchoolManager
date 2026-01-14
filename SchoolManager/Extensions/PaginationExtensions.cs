//using SchoolManager.Dtos.Teacher;

//namespace SchoolManager.Extensions
//{
//    public static class PaginationExtensions
//    {
//        private const int DefaultPageNumber = 1;
//        private const int DefaultPageSize = 20;
//        private const int MaxPageSize = 200;

//        public static T Normalize<T>(this T dto)
//        {
//            var pageNumber = dto?.PageNumber ?? DefaultPageNumber;
//            if (pageNumber < 1)
//            {
//                pageNumber = DefaultPageNumber;
//            }

//            var pageSize = teacherQueryDto?.PageSize ?? DefaultPageSize;
//            if (pageSize < 1)
//            {
//                pageSize = DefaultPageSize;
//            }
//            if (pageSize > MaxPageSize)
//            {
//                pageSize = MaxPageSize;
//            }

//            var search = teacherQueryDto?.Search?.Trim();
//            if (string.IsNullOrWhiteSpace(search))
//            {
//                search = null;
//            }


//            var filterBy = teacherQueryDto?.FilterBy ?? FilterBy.None;


//            // Infer filter when caller provides parameters but doesn't set FilterBy explicitly
//            if (filterBy == FilterBy.None && search is not null)
//            {
//                filterBy = FilterBy.Search;
//            }
//            if (filterBy != FilterBy.Search)
//            {
//                search = null;
//            }
//            return teacherQueryDto.ToTeacherQueryDto(filterBy, pageNumber, pageSize, search);
//        }
//    }
//}
