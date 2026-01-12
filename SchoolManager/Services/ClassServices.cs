using Microsoft.EntityFrameworkCore;
using SchoolManager.Data.Repositories.Interfaces;
using SchoolManager.Dtos.Class;
using SchoolManager.Dtos.Common;
using SchoolManager.Mappers.Classes;
using SchoolManager.Models.Entities;
using SchoolManager.Services.Interfaces;

namespace SchoolManager.Services
{
    public class ClassServices : IClassServices
    {
        private readonly IClassRepository _classRepository;
        public ClassServices(IClassRepository classRepository)
        {
            _classRepository = classRepository;
        }

        public async Task<List<ClassesDto>> GetAllAsync()
        {
            var classes = await _classRepository.GetAllAsync();
            return classes.Select(c => c.ToClassesDto()).ToList();
        }

        public async Task<ClassDetailsDto?> GetClassByIdAsync(Guid id)
        {
            var _class = await _classRepository.GetByIdAsync(id);
            return _class?.ToClassDetailsDto();

        }

        public async Task<Class?> AddClassAsync(AddClassDto addClassDto)
        {

            var className = addClassDto.Name.Trim();

            var existingClass = await _classRepository.GetByNameAsync(className);
            if (existingClass is not null)
            {
                return null;
            }

            //var _class = new Class()
            //{
            //    Name = className
            //};
            var _class = addClassDto.ToClass();
            try
            {

                await _classRepository.AddAsync(_class);
                return _class;
            }
            catch (DbUpdateException)
            {
                return null;
            }

        }

        public async Task<bool> UpdateClassAsync(Guid id, UpdateClassDto updateClassDto)
        {
            var _class = await _classRepository.GetByIdAsync(id);
            if (_class == null)
            {
                return false;
            }
            //_class.Name = updateClassDto.Name.Trim();
            updateClassDto.ToUpdateClass(_class);
            try
            {
                await _classRepository.Update(_class);
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }

        }

        public async Task<bool> DeleteClassAsync(Guid id)
        {
            var _class = await _classRepository.GetByIdAsync(id);
            if (_class == null)
            {
                return false;
            }
            try
            {
                await _classRepository.Remove(_class);
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }

        public async Task<PagedResults<ClassesDto>> GetPagedClassesAsync(ClassQueryDto classQueryDto)
        {
            var result = await _classRepository.GetPagedResultsAsync(classQueryDto);
            return new PagedResults<ClassesDto> {
                Results = result.Results.Select(c => c.ToClassesDto()).ToList(),
                PageNumber = result.PageNumber,
                PageSize = result.PageSize,
                TotalCount = result.TotalCount

            };
        }
    }
}
