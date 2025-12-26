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
        public DbSet<StudentSubject> StudentSubjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Class>()
                .HasIndex(c => c.Name)
                .IsUnique();

            modelBuilder.Entity<Student>()
                .HasOne(s => s.Class)
                .WithMany(c => c.Students)
                .HasForeignKey(s => s.ClassId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StudentSubject>()
                .HasKey(ss => new { ss.StudentId, ss.SubjectId });

            modelBuilder.Entity<StudentSubject>()
                .HasOne(ss => ss.Student)
                .WithMany(s => s.StudentSubjects)
                .HasForeignKey(ss => ss.StudentId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            modelBuilder.Entity<StudentSubject>()
                .HasOne(ss => ss.Subject)
                .WithMany()
                .HasForeignKey(ss => ss.SubjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();


            modelBuilder.Entity<SubjectTeacher>()
                .HasKey(st => new { st.TeacherId, st.ClassId, st.SubjectId});


            modelBuilder.Entity<SubjectTeacher>()
                .HasOne(st => st.Class)
                .WithMany(st=>st.SubjectTeachers)
                .HasForeignKey(st => st.ClassId);

            modelBuilder.Entity<SubjectTeacher>()
                .HasOne(st => st.Teacher)
                .WithMany(st => st.SubjectTeachers)
                .HasForeignKey(st => st.TeacherId);

            modelBuilder.Entity<SubjectTeacher>().
                HasOne(st => st.Subject)
                .WithMany(st => st.SubjectTeachers)
                .HasForeignKey(st => st.SubjectId);

        }
    }
}
