using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UniversityCourseManagementSystem.models;

namespace UniversityCourseManagementSystem.classMapping
{
    public class AssignmentConfiguration : IEntityTypeConfiguration<Assignment>
    {
        void IEntityTypeConfiguration<Assignment>.Configure(EntityTypeBuilder<Assignment> builder)
        {
            builder.HasKey(a => a.AssignmentId);
            builder.Property(a => a.AssignmentTitle).IsRequired().HasMaxLength(128);
            builder.Property(a => a.Description).HasColumnType("TEXT").IsRequired(false);
            builder.Property(a => a.Weight).IsRequired();
            builder.Property(a => a.MaxGrade).IsRequired();
            builder.Property(a => a.DueDate).IsRequired();
            builder.HasOne(a => a.Course)
                   .WithMany(c => c.Assignments)
                   .HasForeignKey(a => a.CourseId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
