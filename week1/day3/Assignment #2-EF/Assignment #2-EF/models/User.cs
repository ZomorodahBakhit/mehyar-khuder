using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UniversityCourseManagementSystem.models
{
    public class User
    {
        public int UserId { get; set; }
        public required string UserName { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string EmailAddress { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Role { get; set; }
        public ICollection<Course> Courses { get; set; } = new List<Course>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Grade> Grades { get; set; } = new List<Grade>();
    }
}
