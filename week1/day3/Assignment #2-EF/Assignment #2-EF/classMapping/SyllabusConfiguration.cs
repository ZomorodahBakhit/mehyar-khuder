using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UniversityCourseManagementSystem.models;

namespace Assignment__2_EF.classMapping
{
    internal class SyllabusConfiguration : IEntityTypeConfiguration<Syllabus>
    {
        public void Configure(EntityTypeBuilder<Syllabus> builder)
        {
            builder.HasKey(s => s.SyllabusId);
            builder.Property(s => s.SyllabusId).ValueGeneratedNever();
            builder.Property(s => s.Description).HasColumnType("TEXT").IsRequired(false);
        }
    }
}
