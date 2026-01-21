using Microsoft.EntityFrameworkCore;
using SchoolManager.Data.Repositories.Interfaces;
using SchoolManager.Models;

namespace SchoolManager.Data.Repositories
{
    public class SubjectClassRepository : Repository<SubjectClass>, ISubjectClassRepository
    {
        public SubjectClassRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
        public async Task AssignSubjectsToClass(List<Guid> subjectIds, Guid classId)
        {
            List<Guid> existingSubs = await _entity
                .Where(sc => sc.ClassId == classId)
                .Select(sc => sc.SubjectId)
                .ToListAsync();

            List<SubjectClass> newAssignments = subjectIds
                .Except(existingSubs)
                .Select(subjectId => new SubjectClass
                {
                    SubjectId = subjectId,
                    ClassId = classId
                }).ToList();

            if (newAssignments.Any())
            {
                await _entity.AddRangeAsync(newAssignments);
            }
            await _dbContext.SaveChangesAsync();

        }

    }
}
        