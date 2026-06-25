using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UniversityCourseManagementSystem.models;

namespace Assignment__2_EF.classMapping
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        void IEntityTypeConfiguration<Comment>.Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(c => c.CommentId);
            builder.Property(c => c.CreatedDate).IsRequired();
            builder.Property(c => c.CommentContent).HasColumnType("TEXT").IsRequired(false);
            builder.HasOne(c => c.Assignment)
                   .WithMany(a => a.Comments)
                   .HasForeignKey(c => c.AssignmentId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(c => c.User)
                   .WithMany(u => u.Comments)
                   .HasForeignKey(c => c.CreatedByUserId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
