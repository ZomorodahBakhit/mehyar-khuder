using Assignment__2UniversityCourseManagementSystem_EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UniversityCourseManagementSystem.models;

namespace Assignment__2_EF
{
    public static class UniversityRepository
    {
        // 1. List all courses 
        public static List<Course> ListAllCourses(UniversityDbContext context) => context.Courses.ToList();

        // 2. Show all assignments for a specific course 
        public static List<Assignment> GetAssignmentsByCourse(UniversityDbContext context, int courseId)
        {
            return context.Assignments
                          .Where(a => a.CourseId == courseId)
                          .ToList();
        }

        // 3. List all students
        public static List<User> GetAllStudents(UniversityDbContext context)
        {
            return context.Users
                          .Where(u => u.Role == "Student")
                          .ToList();
        }

        // 4. Show all comments for a given assignment
        public static List<Comment> GetCommentsByAssignment(UniversityDbContext context, int assignmentId)
        {
            return context.Comments
                          .Where(c => c.AssignmentId == assignmentId)
                          .ToList();
        }

        // 5. Show all grades for a student 
        public static List<Grade> GetGradesByStudent(UniversityDbContext context, int studentId)
        {
            return context.Grades
                          .Where(g => g.StudentId == studentId)
                          .ToList();
        }

        // 6. List each assignment with its course and the teacher’s full name
        public static void ListAssignments(UniversityDbContext context)
        {
            var results = context.Assignments
                .Select(a => new
                {
                    AssignmentTitle = a.AssignmentTitle,
                    CourseName = a.Course.CourseName,
                    TeacherFullName = a.Course.Teacher != null
                ? $"{a.Course.Teacher.FirstName} {a.Course.Teacher.LastName}"
                : "N/A"
                }).ToList();

            foreach (var item in results)
            {
                Console.WriteLine($"Assignment: {item.AssignmentTitle} | Course: {item.CourseName} | Teacher: {item.TeacherFullName}");
            }
        }

        // 7. Query to show average grade per course
        public static void DisplayAverageGrade(UniversityDbContext context)
        {
            var courseAverages = context.Grades
                .GroupBy(g => g.Assignment.Course.CourseName)
                .Select(group => new
                {
                    CourseName = group.Key,
                    CourseAvgGrade = group.Average(g => g.TotalGrade)
                }).ToList();

            foreach (var ca in courseAverages)
            {
                Console.WriteLine($"Course: {ca.CourseName} -> Average Grade: {ca.CourseAvgGrade:F2}");
            }
        }

        // 8.Student Grade
        public static string CalculateStudentCourseGrade(UniversityDbContext context, int studentId, int courseId)
        {
            var totalScore = context.Grades
                .Where(g => g.StudentId == studentId && g.Assignment.CourseId == courseId)
                .Sum(g => g.TotalGrade * g.Assignment.Weight);

            if (totalScore >= 90) return "A";
            if (totalScore >= 80) return "B";
            if (totalScore >= 70) return "C";
            if (totalScore >= 60) return "D";
            return "F";
        }

        // 9. Method to calculate GPA for a student
        public static double CalculateStudentGPA(UniversityDbContext context, int studentId)
        {
            var courseTotals = context.Grades
                .Where(g => g.StudentId == studentId)
                .GroupBy(g => g.Assignment!.CourseId)
                .Select(group => group.Sum(g => (double)(g.TotalGrade! * g.Assignment!.Weight)))
                .ToList();

            if (!courseTotals.Any()) return 0.0;

            return courseTotals.Average();
        }

        //UpdateUserRole
        public static void UpdateUserRole(UniversityDbContext context, int userId, string newRole)
        {
            var user = context.Users.Find(userId);
            if (user != null)
            {
                user.Role = newRole;
                context.SaveChanges();
            }
        }
        //DeleteSpecificComment
        public static void DeleteComment(UniversityDbContext context, int commentId)
        {
            var comment = context.Comments.Find(commentId);
            if (comment != null)
            {
                context.Comments.Remove(comment);
                context.SaveChanges();
            }
        }

        //-----Testing------
        public static void AddNewStudent(UniversityDbContext context, User newStudent)
        {
            newStudent.Role = "Student";
            context.Users.Add(newStudent);
            context.SaveChanges();
            Console.WriteLine($"Donee .. Student {newStudent.FirstName} added successfully!");
        }

        //..
    }
}
