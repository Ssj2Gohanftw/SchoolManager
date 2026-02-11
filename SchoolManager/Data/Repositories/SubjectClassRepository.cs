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

        public async Task<List<SubjectClass>> GetAllAssignmentDetailsForClass(Guid classId)
        {
            return await _entity
                .AsNoTracking()
                .Include(sc => sc.Subject)
                .Include(sc => sc.Class)
                .Where(sc=>sc.ClassId==classId)
                .ToListAsync();
        }

        //public async Task AssignSubjectsToClass(List<Guid> subjectIds, Guid classId)
        //{
        //    List<Guid> existingSubs = await GetExistingSubAssignmentsForClass(classId);

        //List<SubjectClass> newSubjectAssignments = subjectIds
        //    .Except(existingSubs)
        //    .Select(sc => sc.ToSubjectClass(classId)).ToList();

        //    if (newSubjectAssignments.Any())
        //    {
        //        await _entity.AddRangeAsync(newSubjectAssignments);
        //    }
        //    await _dbContext.SaveChangesAsync();

        //}
        //public async Task<List<SubjectClass>> AssignSubjectsToClass(List<Guid> subjectIds, Guid classId)
        //{
        //    List<Guid> existingSubs = await GetExistingSubAssignmentsForClass(classId);

        //    List<SubjectClass> newSubjectAssignments = subjectIds
        //        .Except(existingSubs)
        //        .Select(sc => sc.ToSubjectClass(classId)).ToList();
        //    return newSubjectAssignments;
        //}

        public async Task<List<Guid>> GetExistingSubAssignmentsForClass(Guid classId)
        {
           return await _entity
                .AsNoTracking()
                .Where(sc => sc.ClassId == classId)
                .Select(sc => sc.SubjectId)
                .ToListAsync();
        }
    }
}
        