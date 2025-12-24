using Microsoft.EntityFrameworkCore;
using SchoolManager.Data.Repositories.Interfaces;
using SchoolManager.Models.Dtos.Subject;
using SchoolManager.Models.Entities;
using SchoolManager.Services.Interfaces;

namespace SchoolManager.Services
{
    public class SubjectServices : ISubjectServices
    {
        private readonly ISubjectRepository _subjectRepository;
        public SubjectServices(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }
        public async Task<Subject> AddSubjectAsync(AddSubjectDto addSubjectDto)
        {
            var subject = new Subject()
            {
                Name = addSubjectDto.Name
            };
            await _subjectRepository.AddAsync(subject);
            return subject;
        }

        public async Task<bool> DeleteSubjectAsync(Guid id)
        {
            var subject = await _subjectRepository.GetByIdAsync(id);
            if (subject is null)
            {
                return false;
            }
            try
            {
                await _subjectRepository.Remove(subject);
                return true;
            }
            catch (DbUpdateException)
            {
            return false;
            }
            
        }

        public Task<List<Subject>> GetAllAsync()
        {
            var subjects = _subjectRepository.GetAllAsync();
            return subjects;
        }

        public Task<Subject?> GetSubjectByIdAsync(Guid id)
        {
            var subjects = _subjectRepository.GetByIdAsync(id);
            return subjects;
        }

        public async Task<bool> UpdateSubjectAsync(Guid id, UpdateSubjectDto updateSubjectDto)
        {
            var subject = await _subjectRepository.GetByIdAsync(id);
            if (subject is null)
            {
                return false;
            }
            subject.Name = updateSubjectDto.Name;
            return true;
        }
    }
}
