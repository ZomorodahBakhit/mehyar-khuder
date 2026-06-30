namespace University.Core.DTOs;

using System;

public class CourseDto
{
    public int Id { get; set; }
    public string CourseName { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}