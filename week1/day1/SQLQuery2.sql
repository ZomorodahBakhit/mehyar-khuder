CREATE DATABASE University;
GO
USE University;
GO

CREATE TABLE Users (
    UserId INT PRIMARY KEY,
    UserName VARCHAR(64) NOT NULL,
    FirstName VARCHAR(64) NOT NULL,
    LastName VARCHAR(64) NOT NULL,
    EmailAddress VARCHAR(128) NOT NULL UNIQUE,
    PhoneNumber VARCHAR(16) NOT NULL,
    Role VARCHAR(32) NOT NULL
);

CREATE TABLE Syllabus (
    SyllabusId INT PRIMARY KEY,
    Description TEXT NULL
);

CREATE TABLE Courses (
    CourseId INT PRIMARY KEY,
    CourseName VARCHAR(100) NOT NULL,
    TeacherId INT NULL,
    StartDate DATETIME NOT NULL,
    EndDate DATETIME NOT NULL,
    SyllabusId INT NULL,
    FOREIGN KEY (TeacherId) REFERENCES Users(UserId),
    FOREIGN KEY (SyllabusId) REFERENCES Syllabus(SyllabusId)
);

CREATE TABLE Assignments (
    AssignmentId INT PRIMARY KEY,
    CourseId INT NOT NULL,
    AssignmentTitle VARCHAR(128) NOT NULL,
    Description TEXT NULL,
    Weight float NOT NULL,
    MaxGrade INT NOT NULL,
    DueDate DATE NOT NULL,
    FOREIGN KEY (CourseId) REFERENCES Courses(CourseId),
);

CREATE TABLE Comments(
    CommentId INT PRIMARY KEY,
    AssignmentId INT NOT NULL,
    CreatedByUserId INT NOT NULL,
    CreatedDate DATETIME NOT NULL,
    CommentContent TEXT NULL,
    FOREIGN KEY (AssignmentId) REFERENCES Assignments(AssignmentId),
    FOREIGN KEY (CreatedByUserId) REFERENCES Users(UserId),
    );

CREATE TABLE Grades (
    GradeId INT PRIMARY KEY,
    AssignmentId INT NOT NULL,
    StudentId INT NOT NULL,
    Grade INT NULL,
    FOREIGN KEY (AssignmentId) REFERENCES Assignments(AssignmentId),
    FOREIGN KEY (StudentId) REFERENCES Users(UserId)
);

--//////--

INSERT INTO Users (UserId, UserName, FirstName, LastName, EmailAddress, PhoneNumber, Role)
VALUES 
-- 1.Students:
(1, 'MK11', 'Mehyar', 'Khuder', 'mehyarkhuder11e@gmail.com', '+963968378834', 'Student'),
(2, 'FawzyMinecraft', 'Fawzy', 'Sukker', 'fawzy.sukkar2005@gmail.com', '+963941374366', 'Student'),
(3, 'Zuhair_Alhomsi', 'Zuhair', 'Alhomsi', 'Zuhairalhomsi13@gmail.com', '+963992042713', 'Student'),
-- 2.Teachers:
(4, 'Sami', 'Sami', 'Hijazi', 'samihijazi@gmail.com', '+1(240)381-9639', 'Teacher'),
(5, 'Feryal', 'Feryal', 'Hijazi', 'feryal@gmail.com', '+1(240)381-9459', 'Teacher');

-- 7.Syllabus: (It must Execute before Courses Cause of the foreign key between them)
INSERT INTO Syllabus (SyllabusId, Description) VALUES
(1, 'SQL Basics and Database Design'),
(2, 'Introduction to C#'),
(3, 'Introduction to Entity Framework'),
(4, 'Introduction to WEB API'),
(5, 'Introduction to React');

-- 3.Cources:
INSERT INTO Courses (CourseId, CourseName, TeacherId, StartDate, EndDate, SyllabusId) VALUES
(1, 'SQL Database', 4, '2026-02-01', '2026-06-01', 1),
(2, 'C# Programming', 4, '2026-02-15', '2026-06-15', 2),
(3, 'Entity Framework', 4, '2026-07-01', '2026-11-01', 3), 
(4, 'Web API', 5, '2026-07-15', '2026-11-15', 4),      
(5, 'React', 5, '2026-03-01', '2026-07-01', 5);

-- 4.Assignments:
TRUNCATE TABLE Assignments;
INSERT INTO Assignments (AssignmentId, CourseId, AssignmentTitle, Description, Weight, MaxGrade, DueDate) VALUES
(1, 1, 'SQL A1', 'Basic statements', 0.10, 100, '2026-02-15'),
(2, 1, 'SQL A2', '//2', 0.10, 100, '2026-03-10'),
(3, 1, 'SQL A3', '//3', 0.15, 100, '2026-04-05'),
(4, 1, 'SQL A4', '//4', 0.20, 100, '2026-05-20'),
(5, 1, 'SQL A5', '//5', 0.20, 100, '2026-06-01'),

(6, 2, 'C# A1', '//1', 0.15, 100, '2026-03-01'),
(7, 2, 'C# A2', '//2', 0.15, 100, '2026-03-25'),
(8, 2, 'C# A3', '//3', 0.30, 100, '2026-04-20'),
(9, 2, 'C# A4', '//4', 0.20, 100, '2026-05-15'),
(10, 2, 'C# A5', '//5', 0.20, 100, '2026-06-12'),

(11, 3, 'EF A1', 'DB', 0.20, 100, '2026-07-15'),
(12, 3, 'EF A2', '//2', 0.20, 100, '2026-08-10'),
(13, 3, 'EF A3', '//3', 0.30, 100, '2026-09-05'),
(14, 3, 'EF A4', '//4', 0.15, 100, '2026-10-01'),
(15, 3, 'EF A5', '//5', 0.15, 100, '2026-10-25'),

(16, 4, 'API A1', 'HTTP', 0.10, 100, '2026-08-01'),
(17, 4, 'API A2', '//2', 0.15, 100, '2026-08-25'),
(18, 4, 'API A3', '//3', 0.30, 100, '2026-09-20'),
(19, 4, 'API A4', '//4', 0.20, 100, '2026-10-15'),
(20, 4, 'API A5', '//5', 0.20, 100, '2026-11-10'),

(21, 5, 'React A1', '//1', 0.10, 100, '2026-03-15'),
(22, 5, 'React A2', 'learn Props', 0.20, 100, '2026-04-10'),
(23, 5, 'React A3', 'learn State', 0.20, 100, '2026-05-01'),
(24, 5, 'React A4', 'learn Hooks', 0.20, 100, '2026-05-28'),
(25, 5, 'React A5', 'Complete Dashboard', 0.30, 100, '2026-06-25');

-- 5.Comments:
TRUNCATE TABLE Comments; 
INSERT INTO Comments (CommentId, AssignmentId, CreatedByUserId, CreatedDate, CommentContent) VALUES
(1, 1, 1, '2026-02-12 10:00:00', 'Thanks'),
(2, 2, 2, '2026-02-12 14:30:00', 'OK'),
(3, 3, 3, '2026-03-05 09:15:00', 'Well'),
(4, 4, 1, '2026-02-28 18:00:00', 'Fine'),
(5, 5, 2, '2026-03-26 11:00:00', 'Nice'),
(6, 6, 3, '2026-06-14 22:45:00', 'Thanks for your effort'),
(7, 7, 1, '2026-06-15 08:00:00', 'Thanks for your efforts'),
(8, 8, 2, '2026-06-30 13:20:00', 'Thanks for your efforts'),
(9, 9, 3, '2026-04-09 17:00:00', 'Thanks for your efforts'),
(10, 10, 1, '2026-04-09 19:30:00', 'Great job');

-- 6.Grades:
TRUNCATE TABLE Grades;
DECLARE @GradeId int = 1;
DECLARE @StudentId int = 1;

WHILE @StudentId <= 3
BEGIN
    DECLARE @AssignmentId INT = 1;
    
    WHILE @AssignmentId <= 25
    BEGIN
        DECLARE @Grade INT = 100 - (@AssignmentId + 2) - (@StudentId * 10);
        INSERT INTO Grades (GradeId, AssignmentId, StudentId, Grade)
        VALUES (@GradeId, @AssignmentId, @StudentId, @Grade);
            
        SET @GradeId = @GradeId + 1;
        SET @AssignmentId = @AssignmentId + 1;
    END
    SET @StudentId = @StudentId + 1;
END;

-- 8.Select all Courses:
SELECT * FROM Courses;

-- 9.find all assignments for a specific course (consider it SQL Course):
SELECT * FROM Assignments 
WHERE CourseId = 1;

-- 10.find all students:
SELECT * FROM Users u
WHERE u.Role = 'Student';

-- 11.update student role:
UPDATE Users 
SET Role = 'Teacher'
WHERE UserId = 1;

-- 12.delete a comment :
DELETE FROM Comments
WHERE CommentId = 1;

-- 13.list all students along with their grades for a specific course:
SELECT u.UserId , u.UserName , c.CourseName, a.AssignmentTitle, g.Grade FROM Users u
JOIN Assignments a ON a.CourseId = 1
JOIN Courses c ON c.CourseId = 1
LEFT JOIN Grades g ON u.UserId = g.StudentId AND a.AssignmentId = g.AssignmentId
WHERE u.Role = 'Student';

-- 14.calculate the average grade for each course:
SELECT c.CourseName , AVG(g.Grade) AS CourseAvgGrade FROM Courses c
JOIN Assignments a ON c.CourseId = a.CourseId
JOIN Grades g ON a.AssignmentId = g.AssignmentId
GROUP BY c.CourseName;

-- 15.list all courses with their respective syllabuses:
SELECT c.CourseName, s.Description AS SyllabusDesciption FROM Courses c
LEFT JOIN Syllabus s ON  s.SyllabusId = c.SyllabusId;

-- 16.all comments for a specific course:
SELECT cou.CourseName , com.CommentContent AS Comments FROM Comments com
JOIN Assignments a ON a.AssignmentId = com.AssignmentId
JOIN Courses cou ON cou.CourseId = a.CourseId
WHERE cou.CourseId = 1; 

-- 17.procedure to add a new student:
GO
    CREATE PROCEDURE p_addNewStudent 
        @UserId INT,
        @UserName VARCHAR(64),
        @FirstName VARCHAR(64),
        @LastName VARCHAR(64),
        @EmailAddress VARCHAR(128),
        @PhoneNumber VARCHAR(16)
    AS
    BEGIN
        INSERT INTO Users (UserId, UserName, EmailAddress, FirstName, LastName, PhoneNumber, Role)
        VALUES (@UserId, @UserName, @EmailAddress, @FirstName, @LastName, @PhoneNumber, 'Student');
    END;
GO

EXEC p_addNewStudent 
    @UserId = 7,
    @UserName = 'Ahmad Mohsen', 
    @EmailAddress = 'Ahamadmo101@email.com', 
    @FirstName = 'Ahmad',
    @LastName = 'Mohsen',
    @PhoneNumber = '0912345678';

-- 18.procedure to add a new Assignment:
GO
    CREATE PROCEDURE p_addNewAssignment
        @AssignmentId INT,         
        @CourseId INT,
        @AssignmentTitle VARCHAR(128),
        @Description TEXT,
        @Weight FLOAT,
        @MaxGrade INT,
        @DueDate DATE
    AS
    BEGIN
        DECLARE @CurrentTotalWeight FLOAT;
        SELECT @CurrentTotalWeight = SUM(Weight) FROM Assignments
        WHERE CourseId = @CourseId;
        IF (@CurrentTotalWeight + @Weight <= 1.0)
            INSERT INTO Assignments (AssignmentId, CourseId, AssignmentTitle, Description, Weight, MaxGrade, DueDate)
            VALUES (@AssignmentId, @CourseId, @AssignmentTitle, @Description, @Weight, @MaxGrade, @DueDate);
    END;
GO

EXEC p_addNewAssignment 
    @AssignmentId = 26, 
    @CourseId = 4,
    @AssignmentTitle = 'API A6', 
    @Description = 'Final Assignment', 
    @Weight = 0.20, 
    @MaxGrade = 100, 
    @DueDate = '2026-12-01';

-- 19.function to calculate the Student Grade in a Course :
GO
    CREATE FUNCTION f_CalculateStudentCourseGrade (
        @StudentId INT,
        @CourseId INT
    )
    RETURNS VARCHAR(1) 
    AS
    BEGIN
        DECLARE @Total FLOAT;
        DECLARE @Letter VARCHAR(1);

        SELECT @Total = SUM(g.Grade * a.Weight) FROM Grades g
        JOIN Assignments a ON a.AssignmentId = g.AssignmentId 
        WHERE g.StudentId = @StudentId AND a.CourseId = @CourseId;

        SET @Letter = CASE 
            WHEN @Total >= 90 THEN 'A'
            WHEN @Total >= 80 THEN 'B'
            WHEN @Total >= 70 THEN 'C'
            WHEN @Total >= 60 THEN 'D'
            ELSE 'F'
        END;
        RETURN @Letter;
    END;
GO

SELECT u.UserName, c.CourseName, 
    dbo.f_CalculateStudentCourseGrade(u.UserId, c.CourseId) AS FinalGrade
FROM Users u
CROSS JOIN Courses c
WHERE u.Role = 'Student' AND u.UserId = 2 AND c.CourseId = 4;

-- 20. GPA:
GO
    CREATE FUNCTION f_CalculateStudentGPA (@StudentId INT)
    RETURNS float 
    AS
    BEGIN
        DECLARE @GPA float;
        SELECT @GPA = AVG(CourseTotalScore)
        FROM (
            SELECT a.CourseId, 
                SUM(g.Grade * a.Weight) AS CourseTotalScore
            FROM Grades g
            JOIN Assignments a ON a.AssignmentId = g.AssignmentId
            WHERE g.StudentId = @StudentId
            GROUP BY a.CourseId
        ) AS StudentCourses;
        RETURN @GPA;
    END;
GO

SELECT u.UserName,dbo.f_CalculateStudentGPA(UserId) AS GPA FROM Users u
WHERE Role = 'Student';