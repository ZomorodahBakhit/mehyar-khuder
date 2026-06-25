using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UniversityCourseManagementSystem.models
{
    public class Course
    {
        public int CourseId { get; set; }
        public required string CourseName { get; set; }
        public int? TeacherId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? SyllabusId { get; set; }
        public User? Teacher { get; set; }
        public Syllabus? Syllabus { get; set; }
        public ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();
    }
}
