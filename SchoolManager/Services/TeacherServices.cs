using Microsoft.EntityFrameworkCore;
using SchoolManager.Data.Repositories.Interfaces;
using SchoolManager.Dtos.Common;
using SchoolManager.Dtos.Teacher;
using SchoolManager.Mappers.Common;
using SchoolManager.Mappers.Teachers;
using SchoolManager.Models;
using SchoolManager.Services.Interfaces;

namespace SchoolManager.Services
{
    public class TeacherServices : ITeacherServices
    {
        private readonly ITeacherRepository _teacherRepository;

        public TeacherServices(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        public async Task<List<TeacherSummaryDto>> GetAllAsync()
        {
            List<Teacher> teachers = await _teacherRepository.GetAllAsync();
            return teachers.Select(t => t.ToTeacherSummaryDto()).ToList();
        }

        public async Task<TeacherDetailsDto?> GetTeacherByIdAsync(Guid id)
        {
            Teacher? teacher = await _teacherRepository.GetByIdAsync(id);
            return teacher?.ToTeacherDetailsDto();
        }

        public async Task<Teacher?> AddTeacherAsync(AddTeacherDto addTeacherDto)
        {
            Teacher teachers = addTeacherDto.ToTeacher();
            try
            {
                await _teacherRepository.AddAsync(teachers);
                return teachers;
            }
            catch (DbUpdateException)
            {
                return null;
            }
        }

        public async Task<bool> DeleteTeacherAsync(Guid id)
        {
            Teacher? teacher = await _teacherRepository.GetByIdAsync(id);
            if (teacher == null)
            {
                return false;
            }

            try
            {
                await _teacherRepository.Remove(teacher);
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }

        public async Task<bool> UpdateTeacherAsync(Guid id, UpdateTeacherDto updateTeacherDto)
        {
            Teacher? teacher = await _teacherRepository.GetByIdAsync(id);
            if (teacher == null)
            {
                return false;
            }
            updateTeacherDto.ToUpdateTeacher(teacher);

            try
            {
                await _teacherRepository.Update(teacher);
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }
        public async Task<PagedResults<TeacherDetailsDto>> GetPagedTeachersAsync(TeacherQueryDto teacherQueryDto)
        {
            PagedResults<Teacher> result = await _teacherRepository.GetPagedAsync(teacherQueryDto);
            List<TeacherDetailsDto> teacherDetails = result.Results.Select(t => t.ToTeacherDetailsDto()).ToList();
            return teacherDetails.ToPagedResults(result.PageNumber, result.PageSize, result.TotalCount);
        }
    }
}

