using Assignment__2UniversityCourseManagementSystem_EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using UniversityCourseManagementSystem.models;

namespace Assignment__2_EF
{
    public static class Seeder
    {
        public static void Seed(UniversityDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            if (context.Users.Any()) return;

            // 1. Users
            var users = new List<User>
            {
                //Students
                new User { UserId = 1, UserName = "MK11", FirstName = "Mehyar", LastName = "Khuder", EmailAddress = "mehyarkhuder11e@gmail.com", PhoneNumber = "+963968378834", Role = "Student" },
                new User { UserId = 2, UserName = "FawzyMinecraft", FirstName = "Fawzy", LastName = "Sukker", EmailAddress = "fawzy.sukkar2005@gmail.com", PhoneNumber = "+963941374366", Role = "Student" },
                new User { UserId = 3, UserName = "Zuhair_Alhomsi", FirstName = "Zuhair", LastName = "Alhomsi", EmailAddress = "Zuhairalhomsi13@gmail.com", PhoneNumber = "+963992042713", Role = "Student" },
                //Teachers
                new User { UserId = 4, UserName = "Sami", FirstName = "Sami", LastName = "Hijazi", EmailAddress = "samihijazi@gmail.com", PhoneNumber = "+1(240)381-9639", Role = "Teacher" },
                new User { UserId = 5, UserName = "Feryal", FirstName = "Feryal", LastName = "Tulaimat", EmailAddress = "feryal@gmail.com", PhoneNumber = "+1(240)381-9459", Role = "Teacher" }
            };
            context.Users.AddRange(users);
            SaveWithIdentityInsert(context, "Users");

            // 2. Sylla
            var sylla = new List<Syllabus>
            {
                new Syllabus { SyllabusId = 1, Description = "SQL Basics and Database Design" },
                new Syllabus { SyllabusId = 2, Description = "Introduction to C#" },
                new Syllabus { SyllabusId = 3, Description = "Introduction to Entity Framework" },
                new Syllabus { SyllabusId = 4, Description = "Introduction to WEB API" },
                new Syllabus { SyllabusId = 5, Description = "Introduction to React" }
            };
            context.Sylla.AddRange(sylla);
            SaveWithIdentityInsert(context, "Sylla"); 

            // 3. Courses
            var courses = new List<Course>
            {
                new Course { CourseId = 1, CourseName = "SQL Database", TeacherId = 4, StartDate = DateTime.Parse("2026-02-01"), EndDate = DateTime.Parse("2026-06-01"), SyllabusId = 1, Teacher = null!, Syllabus = null! },
                new Course { CourseId = 2, CourseName = "C# Programming", TeacherId = 4, StartDate = DateTime.Parse("2026-02-15"), EndDate = DateTime.Parse("2026-06-15"), SyllabusId = 2, Teacher = null!, Syllabus = null! },
                new Course { CourseId = 3, CourseName = "Entity Framework", TeacherId = 4, StartDate = DateTime.Parse("2026-07-01"), EndDate = DateTime.Parse("2026-11-01"), SyllabusId = 3, Teacher = null!, Syllabus = null! },
                new Course { CourseId = 4, CourseName = "Web API", TeacherId = 5, StartDate = DateTime.Parse("2026-07-15"), EndDate = DateTime.Parse("2026-11-15"), SyllabusId = 4, Teacher = null!, Syllabus = null! },
                new Course { CourseId = 5, CourseName = "React", TeacherId = 5, StartDate = DateTime.Parse("2026-03-01"), EndDate = DateTime.Parse("2026-07-01"), SyllabusId = 5, Teacher = null!, Syllabus = null! }
            };
            context.Courses.AddRange(courses);
            SaveWithIdentityInsert(context, "Courses"); 

            // 4. Assignments
            var assignments = new List<Assignment>
            {
                new Assignment { AssignmentId = 1, CourseId = 1, AssignmentTitle = "SQL A1", Description = "Basic statements", Weight = 0.10f, MaxGrade = 100, DueDate = DateTime.Parse("2026-02-15"), Course = null! },
                new Assignment { AssignmentId = 2, CourseId = 1, AssignmentTitle = "SQL A2", Description = "//2", Weight = 0.10f, MaxGrade = 100, DueDate = DateTime.Parse("2026-03-10"), Course = null! },
                new Assignment { AssignmentId = 3, CourseId = 1, AssignmentTitle = "SQL A3", Description = "//3", Weight = 0.15f, MaxGrade = 100, DueDate = DateTime.Parse("2026-04-05"), Course = null! },
                new Assignment { AssignmentId = 4, CourseId = 1, AssignmentTitle = "SQL A4", Description = "//4", Weight = 0.20f, MaxGrade = 100, DueDate = DateTime.Parse("2026-05-20"), Course = null! },
                new Assignment { AssignmentId = 5, CourseId = 1, AssignmentTitle = "SQL A5", Description = "//5", Weight = 0.20f, MaxGrade = 100, DueDate = DateTime.Parse("2026-06-01"), Course = null! },
                new Assignment { AssignmentId = 6, CourseId = 2, AssignmentTitle = "C# A1", Description = "//1", Weight = 0.15f, MaxGrade = 100, DueDate = DateTime.Parse("2026-03-01"), Course = null! },
                new Assignment { AssignmentId = 7, CourseId = 2, AssignmentTitle = "C# A2", Description = "//2", Weight = 0.15f, MaxGrade = 100, DueDate = DateTime.Parse("2026-03-25"), Course = null! },
                new Assignment { AssignmentId = 8, CourseId = 2, AssignmentTitle = "C# A3", Description = "//3", Weight = 0.30f, MaxGrade = 100, DueDate = DateTime.Parse("2026-04-20"), Course = null! },
                new Assignment { AssignmentId = 9, CourseId = 2, AssignmentTitle = "C# A4", Description = "//4", Weight = 0.20f, MaxGrade = 100, DueDate = DateTime.Parse("2026-05-15"), Course = null! },
                new Assignment { AssignmentId = 10, CourseId = 2, AssignmentTitle = "C# A5", Description = "//5", Weight = 0.20f, MaxGrade = 100, DueDate = DateTime.Parse("2026-06-12"), Course = null! },
                new Assignment { AssignmentId = 11, CourseId = 3, AssignmentTitle = "EF A1", Description = "DB", Weight = 0.20f, MaxGrade = 100, DueDate = DateTime.Parse("2026-07-15"), Course = null! },
                new Assignment { AssignmentId = 12, CourseId = 3, AssignmentTitle = "EF A2", Description = "//2", Weight = 0.20f, MaxGrade = 100, DueDate = DateTime.Parse("2026-08-10"), Course = null! },
                new Assignment { AssignmentId = 13, CourseId = 3, AssignmentTitle = "EF A3", Description = "//3", Weight = 0.30f, MaxGrade = 100, DueDate = DateTime.Parse("2026-09-05"), Course = null! },
                new Assignment { AssignmentId = 14, CourseId = 3, AssignmentTitle = "EF A4", Description = "//4", Weight = 0.15f, MaxGrade = 100, DueDate = DateTime.Parse("2026-10-01"), Course = null! },
                new Assignment { AssignmentId = 15, CourseId = 3, AssignmentTitle = "EF A5", Description = "//5", Weight = 0.15f, MaxGrade = 100, DueDate = DateTime.Parse("2026-10-25"), Course = null! },
                new Assignment { AssignmentId = 16, CourseId = 4, AssignmentTitle = "API A1", Description = "HTTP", Weight = 0.10f, MaxGrade = 100, DueDate = DateTime.Parse("2026-08-01"), Course = null! },
                new Assignment { AssignmentId = 17, CourseId = 4, AssignmentTitle = "API A2", Description = "//2", Weight = 0.15f, MaxGrade = 100, DueDate = DateTime.Parse("2026-08-25"), Course = null! },
                new Assignment { AssignmentId = 18, CourseId = 4, AssignmentTitle = "API A3", Description = "//3", Weight = 0.30f, MaxGrade = 100, DueDate = DateTime.Parse("2026-09-20"), Course = null! },
                new Assignment { AssignmentId = 19, CourseId = 4, AssignmentTitle = "API A4", Description = "//4", Weight = 0.20f, MaxGrade = 100, DueDate = DateTime.Parse("2026-10-15"), Course = null! },
                new Assignment { AssignmentId = 20, CourseId = 4, AssignmentTitle = "API A5", Description = "//5", Weight = 0.20f, MaxGrade = 100, DueDate = DateTime.Parse("2026-11-10"), Course = null! },
                new Assignment { AssignmentId = 21, CourseId = 5, AssignmentTitle = "React A1", Description = "//1", Weight = 0.10f, MaxGrade = 100, DueDate = DateTime.Parse("2026-03-15"), Course = null! },
                new Assignment { AssignmentId = 22, CourseId = 5, AssignmentTitle = "React A2", Description = "learn Props", Weight = 0.20f, MaxGrade = 100, DueDate = DateTime.Parse("2026-04-10"), Course = null! },
                new Assignment { AssignmentId = 23, CourseId = 5, AssignmentTitle = "React A3", Description = "learn State", Weight = 0.20f, MaxGrade = 100, DueDate = DateTime.Parse("2026-05-01"), Course = null! },
                new Assignment { AssignmentId = 24, CourseId = 5, AssignmentTitle = "React A4", Description = "learn Hooks", Weight = 0.20f, MaxGrade = 100, DueDate = DateTime.Parse("2026-05-28"), Course = null! },
                new Assignment { AssignmentId = 25, CourseId = 5, AssignmentTitle = "React A5", Description = "Complete Dashboard", Weight = 0.30f, MaxGrade = 100, DueDate = DateTime.Parse("2026-06-25"), Course = null! }
            };
            context.Assignments.AddRange(assignments);
            SaveWithIdentityInsert(context, "Assignments"); 

            // 5. Comments
            var comments = new List<Comment>
            {
                new Comment { CommentId = 1, AssignmentId = 1, CreatedByUserId = 1, CreatedDate = DateTime.Parse("2026-02-12 10:00:00"), CommentContent = "Thanks", Assignment = null!, User = null! },
                new Comment { CommentId = 2, AssignmentId = 2, CreatedByUserId = 2, CreatedDate = DateTime.Parse("2026-02-12 14:30:00"), CommentContent = "OK", Assignment = null!, User = null! },
                new Comment { CommentId = 3, AssignmentId = 3, CreatedByUserId = 3, CreatedDate = DateTime.Parse("2026-03-05 09:15:00"), CommentContent = "Well", Assignment = null!, User = null! },
                new Comment { CommentId = 4, AssignmentId = 4, CreatedByUserId = 1, CreatedDate = DateTime.Parse("2026-02-28 18:00:00"), CommentContent = "Fine", Assignment = null!, User = null! },
                new Comment { CommentId = 5, AssignmentId = 5, CreatedByUserId = 2, CreatedDate = DateTime.Parse("2026-03-26 11:00:00"), CommentContent = "Nice", Assignment = null!, User = null! },
                new Comment { CommentId = 6, AssignmentId = 6, CreatedByUserId = 3, CreatedDate = DateTime.Parse("2026-06-14 22:45:00"), CommentContent = "Thanks for your effort", Assignment = null!, User = null! },
                new Comment { CommentId = 7, AssignmentId = 7, CreatedByUserId = 1, CreatedDate = DateTime.Parse("2026-06-15 08:00:00"), CommentContent = "Thanks for your efforts", Assignment = null!, User = null! },
                new Comment { CommentId = 8, AssignmentId = 8, CreatedByUserId = 2, CreatedDate = DateTime.Parse("2026-06-30 13:20:00"), CommentContent = "Thanks for your efforts", Assignment = null!, User = null! },
                new Comment { CommentId = 9, AssignmentId = 9, CreatedByUserId = 3, CreatedDate = DateTime.Parse("2026-04-09 17:00:00"), CommentContent = "Thanks for your efforts", Assignment = null!, User = null! },
                new Comment { CommentId = 10, AssignmentId = 10, CreatedByUserId = 1, CreatedDate = DateTime.Parse("2026-04-09 19:30:00"), CommentContent = "Great job", Assignment = null!, User = null! }
            };
            context.Comments.AddRange(comments);
            SaveWithIdentityInsert(context, "Comments");

            // 6. Grades
            var gradesList = new List<Grade>();
            int gradeIdCounter = 1;

            for (int studentId = 1; studentId <= 3; studentId++)
            {
                for (int assignmentId = 1; assignmentId <= 25; assignmentId++)
                {
                    int calculatedGrade = 100 - (assignmentId + 2) - (studentId * 10);
                    gradesList.Add(new Grade
                    {
                        GradeId = gradeIdCounter++,
                        AssignmentId = assignmentId,
                        StudentId = studentId,
                        TotalGrade = calculatedGrade,
                        Assignment = null!,
                        Student = null!
                    });
                }
            }
            context.Grades.AddRange(gradesList);
            SaveWithIdentityInsert(context, "Grades");
        }

        // اضطريت استخدم فتح الاتصال اليدوي لادخال معرفات الموديلز خاصتنا يدويا متل ما ساوينا بوظيفة ال SQL
        private static void SaveWithIdentityInsert(UniversityDbContext context, string tableName)
        {
            try
            {
                context.Database.OpenConnection();

                string sqlOn = $"SET IDENTITY_INSERT [{tableName}] ON";
                context.Database.ExecuteSqlRaw(sqlOn);

                context.SaveChanges();

                string sqlOff = $"SET IDENTITY_INSERT [{tableName}] OFF";
                context.Database.ExecuteSqlRaw(sqlOff);
            }
            catch (Microsoft.Data.SqlClient.SqlException ex) when (ex.Number == 8106)
            {
                context.SaveChanges();
            }
            finally
            {
                context.Database.CloseConnection();
            }
        }
    }
}