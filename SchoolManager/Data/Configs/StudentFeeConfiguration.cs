using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManager.Models;

namespace SchoolManager.Data.Configs
{
    public class StudentFeeConfiguration : IEntityTypeConfiguration<StudentFee>
    {
        public void Configure(EntityTypeBuilder<StudentFee> builder)
        {
            builder
              .HasOne(s => s.Student)
              .WithMany(sf => sf.StudentFees)
              .HasForeignKey(s => s.StudentId)
              .OnDelete(DeleteBehavior.Cascade)
              .IsRequired();

            builder
                .HasOne(f => f.Fee)
                .WithMany(sf => sf.StudentFees)
                .HasForeignKey(f => f.FeeId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder
                .HasIndex(ss => new { ss.StudentId, ss.FeeId })
                .IsUnique();

            builder
                .HasKey(sf => new { sf.StudentId, sf.FeeId });

        }
    }
}
