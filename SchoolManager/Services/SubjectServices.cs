using Microsoft.EntityFrameworkCore;
using SchoolManager.Data.Repositories.Interfaces;
using SchoolManager.Dtos.Common;
using SchoolManager.Dtos.Subject;
using SchoolManager.Mappers.Subjects;
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
            var subject = addSubjectDto.ToSubject();
            try
            {
                await _subjectRepository.AddAsync(subject);
                return subject;
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("Unable to add subject.", ex);
            }
        }

        public async Task<bool> DeleteSubjectAsync(Guid id)
        {
            var subject = await _subjectRepository.GetByIdAsync(id);
            if (subject == null)
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

        public async Task<List<SubjectSummaryDto>> GetAllAsync()
        {
            var subjects = await _subjectRepository.GetAllAsync();
            return subjects.Select(s=>s.ToSubjectSummaryDto()).ToList();
        }

        public async Task<PagedResults<SubjectDetailsDto>> GetPagedSubjectsAsync(SubjectQueryDto subjectQueryDto)
        {
            var results = await _subjectRepository.GetPagedResults(subjectQueryDto);
            return new PagedResults<SubjectDetailsDto>()
            {
                Results = results.Results.Select(sub => sub.ToSubjectDetailsDto()).ToList(),
                TotalCount = results.TotalCount,
                PageNumber = results.PageNumber,
                PageSize = results.PageSize
            };
        }

        public async Task<SubjectDetailsDto?> GetSubjectByIdAsync(Guid id)
        {
            var subject = await _subjectRepository.GetByIdAsync(id);
            return subject?.ToSubjectDetailsDto();
        }

        public async Task<bool> UpdateSubjectAsync(Guid id, UpdateSubjectDto updateSubjectDto)
        {
            var subject = await _subjectRepository.GetByIdAsync(id);
            if (subject == null)
            {
                return false;
            }
            updateSubjectDto.ToUpdateSubject(subject);

            try
            {
                await _subjectRepository.Update(subject);
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }
    }
}
