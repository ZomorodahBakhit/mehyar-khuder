using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UniversityCourseManagementSystem.models;

namespace Assignment__2_EF.classMapping
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        void IEntityTypeConfiguration<Course>.Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(c => c.CourseId);
            builder.Property(c => c.CourseId).ValueGeneratedNever();
            builder.Property(c => c.CourseName).IsRequired().HasMaxLength(100);
            builder.Property(c => c.StartDate).IsRequired();
            builder.Property(c => c.EndDate).IsRequired();
            builder.HasOne(c => c.Teacher)
                   .WithMany(u => u.Courses)
                   .HasForeignKey(c => c.TeacherId)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(c => c.Syllabus)
                   .WithMany()
                   .HasForeignKey(c => c.SyllabusId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
