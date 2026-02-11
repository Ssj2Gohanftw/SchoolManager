using Microsoft.EntityFrameworkCore;
using SchoolManager.Data.Repositories.Interfaces;
using SchoolManager.Dtos.Class;
using SchoolManager.Dtos.Common;
using SchoolManager.Mappers.Classes;
using SchoolManager.Mappers.Common;
using SchoolManager.Models;
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
            List<Class> classes = await _classRepository.GetAllAsync();
            return classes.Select(c => c.ToClassesDto()).ToList();
        }

        public async Task<ClassDetailsDto?> GetClassByIdAsync(Guid id)
        {
            Class? _class = await _classRepository.GetByIdAsync(id);
            return _class?.ToClassDetailsDto();

        }

        public async Task<Class?> AddClassAsync(AddClassDto addClassDto)
        {

            string className = addClassDto.Name.Trim();

            Class? existingClass = await _classRepository.GetByNameAsync(className);
            if (existingClass !=null)
            {
                return null;
            }

            Class _class = addClassDto.ToClass();
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
            Class? _class = await _classRepository.GetByIdAsync(id);
            if (_class == null)
            {
                return false;
            }
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
            Class? _class = await _classRepository.GetByIdAsync(id);
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

        public async Task<PagedResults<ClassDetailsDto>> GetPagedClassesAsync(ClassQueryDto classQueryDto)
        {
            PagedResults<Class> result = await _classRepository.GetPagedResultsAsync(classQueryDto);
            List<ClassDetailsDto> classDetails = result.Results.Select(c => c.ToClassDetailsDto()).ToList();
            return classDetails.ToPagedResults( result.TotalCount);
        }
    }
}
