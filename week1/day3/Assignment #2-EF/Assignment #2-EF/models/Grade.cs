using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityCourseManagementSystem.models
{
    public class Grade
    {
        public int GradeId { get; set; }
        public int AssignmentId { get; set; }
        public int StudentId { get; set; }
        public int? TotalGrade { get; set; } 
        public required Assignment Assignment { get; set; }
        public required User Student { get; set; }
    }
}
