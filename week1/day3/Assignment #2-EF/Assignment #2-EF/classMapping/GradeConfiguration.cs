using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UniversityCourseManagementSystem.models;

namespace Assignment__2_EF.classMapping
{
    public class GradeConfiguration : IEntityTypeConfiguration<Grade>
    {
        void IEntityTypeConfiguration<Grade>.Configure(EntityTypeBuilder<Grade> builder)
        {
            builder.HasKey(g => g.GradeId);
            builder.Property(g => g.GradeId).ValueGeneratedNever();

            builder.Property(g => g.TotalGrade).HasColumnName("Grade").IsRequired(false);

            builder.HasOne(g => g.Assignment)
                   .WithMany(a => a.Grades)
                   .HasForeignKey(g => g.AssignmentId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(g => g.Student)
                   .WithMany(u => u.Grades)
                   .HasForeignKey(g => g.StudentId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
