using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityCourseManagementSystem.models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int AssignmentId { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? CommentContent { get; set; }

        public required Assignment Assignment { get; set; }
        public required User User { get; set; }
    }
}
