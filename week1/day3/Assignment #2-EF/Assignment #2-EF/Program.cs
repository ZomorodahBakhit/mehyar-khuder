using System;
using Assignment__2_EF;
using Assignment__2UniversityCourseManagementSystem_EF;
using UniversityCourseManagementSystem.models;

Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.WriteLine("Initializing Database and Seeding Data...");

using (var context = new UniversityDbContext())
{
    /// 1. Setup & Insert Data (MODELS>>>)
    Seeder.Seed(context);
    Console.WriteLine("Database initialization complete.\n");
    Console.WriteLine("==================================================");
    Console.WriteLine("       UNIVERSITY COURSE MANAGEMENT SYSTEM       ");
    Console.WriteLine("==================================================\n");

    /// 4. Queries :

    // 1: List all courses
    Console.WriteLine("--- 1. List All Courses ---");
    var courses = UniversityRepository.ListAllCourses(context);
    foreach (var c in courses)
    {
        Console.WriteLine($"Course ID: {c.CourseId} | Name: {c.CourseName}");
    }

    // 2: Show all assignments for a specific course (CourseId = 1)
    Console.WriteLine("\n--- 2. Assignments for Course 1 ---");
    var assignments = UniversityRepository.GetAssignmentsByCourse(context, 1);
    foreach (var a in assignments)
    {
        Console.WriteLine($"Assignment: {a.AssignmentTitle} | Max Grade: {a.MaxGrade}");
    }

    // 3: List all students
    Console.WriteLine("\n--- 3. List All Students ---");
    var students = UniversityRepository.GetAllStudents(context);
    foreach (var s in students)
    {
        Console.WriteLine($"Student: {s.FirstName} {s.LastName} | Email: {s.EmailAddress}");
    }

    // 4: Show all comments for a given assignment (AssignmentId = 1)
    Console.WriteLine("\n--- 4. Comments for Assignment 1 ---");
    var comments = UniversityRepository.GetCommentsByAssignment(context, 1);
    foreach (var c in comments)
    {
        Console.WriteLine($"Comment ID: {c.CommentId} | Content: {c.CommentContent}");
    }

    // 5: Show all grades for a student (StudentId = 1)
    Console.WriteLine("\n--- 5. Grades for Student 1 ---");
    var grades = UniversityRepository.GetGradesByStudent(context, 1);
    foreach (var g in grades)
    {
        Console.WriteLine($"Assignment ID: {g.AssignmentId} | Grade: {g.TotalGrade}");
    }

    // 6: List each assignment with its course and teacher
    Console.WriteLine("\n--- 6. Assignments with Teacher Details ---");
    UniversityRepository.ListAssignments(context);

    // 7: Show average grade per course
    Console.WriteLine("\n--- 7. Average Grade Per Course ---");
    UniversityRepository.DisplayAverageGrade(context);

    // 8 & 9: Letter Grades and GPA
    Console.WriteLine("\n--- 8 & 9. Student Performance Evaluation (Student 1) ---");
    double gpa = UniversityRepository.CalculateStudentGPA(context, 1);
    string letterGrade = UniversityRepository.CalculateStudentCourseGrade(context, 1, 1);

    Console.WriteLine($"Overall GPA for Student 1: {gpa}");
    Console.WriteLine($"Letter Grade for Student 1 in Course 1: {letterGrade}");

    /// 5. Updates & Deletions
    Console.WriteLine("\n--- Executing Updates & Deletions ---");

    // Update Role
    UniversityRepository.UpdateUserRole(context, 1, "Teacher");
    Console.WriteLine("Updated role of Student 1 to Teacher.");

    // Delete Comment
    UniversityRepository.DeleteComment(context, 2);// بحيث بعدني على اعتبار الاختيار يدوي متل ما اشتغلنا ب sql
    Console.WriteLine("Deleted comment with ID 2.");

    Console.WriteLine("\nAll operations completed successfully.");



    //----Testing :
    //UniversityRepository.AddNewStudent(context, newStudent: new User
    //{
    //    FirstName = "Test",
    //    LastName = "Student",
    //    EmailAddress = "mehyarkhuder11@gmail.com",
    //    PhoneNumber = "123456",
    //    Role = "Student",
    //    UserName = "Test",
    //    Comments = [],
    //    Courses = [],
    //    Grades = [],
    //    UserId = 10
    //});
    //var testingStudents = UniversityRepository.GetAllStudents(context);
    //foreach (var s in testingStudents)
    //{
    //    Console.WriteLine($"Student: {s.FirstName} {s.LastName} | Email: {s.EmailAddress}");
    //}

    Console.WriteLine("Press any key to exit...");
    Console.ReadKey();
}