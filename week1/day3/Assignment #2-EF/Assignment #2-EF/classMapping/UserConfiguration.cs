using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniversityCourseManagementSystem.models;

namespace Assignment__2_EF.classMapping
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        void IEntityTypeConfiguration<User>.Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.UserId);
            builder.Property(u => u.UserName).IsRequired().HasMaxLength(64);
            builder.Property(u => u.FirstName).IsRequired().HasMaxLength(64);
            builder.Property(u => u.LastName).IsRequired().HasMaxLength(64);
            builder.Property(u => u.PhoneNumber).IsRequired().HasMaxLength(16);
            builder.Property(u => u.Role).IsRequired().HasMaxLength(32);
            builder.Property(u => u.EmailAddress).IsRequired().HasMaxLength(128);
            builder.HasIndex(u => u.EmailAddress).IsUnique();
        }
    }
}