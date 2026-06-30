using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using University.Data.Entities;

namespace University.Data
{
    public class UniversityDbContext : IdentityDbContext<User, Role,
      int,
      UserClaim,
      UserRole,
      UserLogin,
      RoleClaim,
      UserToken>
    {
        public UniversityDbContext(DbContextOptions<UniversityDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UniversityDbContext).Assembly);
        }
    }
}