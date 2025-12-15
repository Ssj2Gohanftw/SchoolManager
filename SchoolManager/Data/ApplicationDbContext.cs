using Microsoft.EntityFrameworkCore;
using SchoolManager.Models.Entities;

namespace SchoolManager.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Class> Class { get; set; }
        public DbSet<SubjectTeacher> SubjectTeacher { get; set; }
        public DbSet<StudentClass> StudentClass { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //StudentID and ClassID together form the primary key for StudentClass
            modelBuilder.Entity<StudentClass>().HasKey(sc => new { sc.StudentId, sc.ClassId });

            modelBuilder.Entity<SubjectTeacher>().HasKey(sc => new { sc.TeacherId, sc.SubjectId});
        }
    }
}
