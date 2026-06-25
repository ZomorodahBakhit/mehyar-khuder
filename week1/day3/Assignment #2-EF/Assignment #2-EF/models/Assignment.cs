using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityCourseManagementSystem.models
{
    public class Assignment
    {
        public int AssignmentId { get; set; }
        public int CourseId { get; set; }
        public required string AssignmentTitle { get; set; }
        public string? Description { get; set; }
        public float Weight { get; set; }
        public int MaxGrade { get; set; }
        public DateTime DueDate { get; set; }
        public required Course Course { get; set; }
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Grade> Grades { get; set; } = new List<Grade>();
    }
}
