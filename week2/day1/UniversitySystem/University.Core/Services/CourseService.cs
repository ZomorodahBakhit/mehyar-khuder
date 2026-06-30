namespace University.Core.Services;

using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using University.Core.DTOs;
using University.Core.Exceptions;
using University.Core.Forms;
using University.Core.Validation;
using University.Data.Entities;
using University.Data.Repositories;

public interface ICourseService
{
    Task<IEnumerable<CourseDto>> GetAllAsync();
    Task<CourseDto?> GetByIdAsync(int id);
    Task<CourseDto> CreateAsync(CreateCourseForm form);
    Task<bool> UpdateAsync(int id, UpdateCourseForm form);
    Task<bool> DeleteAsync(int id);
}
public class CourseService(ICourseRepository repository, ILogger<CourseService> logger) : ICourseService
{
    public async Task<IEnumerable<CourseDto>> GetAllAsync()
    {
        var courses = await repository.GetAllAsync();
        return courses.Select(c => new CourseDto
        {
            Id = c.Id,
            CourseName = c.CourseName,
            StartDate = c.StartDate,
            EndDate = c.EndDate
        });
    }

    public async Task<CourseDto?> GetByIdAsync(int id)
    {
        var course = await repository.GetByIdAsync(id);
        if (course is null)
        {
            logger.LogError("Course with ID {Id} not found.", id);
            throw new NotFoundException("Course not found.");
        }

        return new CourseDto
        {
            Id = course.Id,
            CourseName = course.CourseName,
            StartDate = course.StartDate,
            EndDate = course.EndDate
        };
    }

    public async Task<CourseDto> CreateAsync(CreateCourseForm form)
    {
        FormValidator.Validate(form);

        var course = new Course
        {
            CourseName = form.CourseName,
            StartDate = form.StartDate,
            EndDate = form.EndDate
        };

        var createdCourse = await repository.AddAsync(course);
        return new CourseDto
        {
            Id = createdCourse.Id,
            CourseName = createdCourse.CourseName,
            StartDate = createdCourse.StartDate,
            EndDate = createdCourse.EndDate
        };
    }

    public async Task<bool> UpdateAsync(int id, UpdateCourseForm form)
    {
        FormValidator.Validate(form);

        var course = await repository.GetByIdAsync(id);
        if (course is null)
        {
            logger.LogError("Course with ID {Id} not found.", id);
            throw new NotFoundException("Course not found.");
        }

        course.CourseName = form.CourseName;
        course.StartDate = form.StartDate;
        course.EndDate = form.EndDate;

        await repository.UpdateAsync(course);
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var course = await repository.GetByIdAsync(id);
        if (course is null)
        {
            logger.LogError("Course with ID {Id} not found.", id);
            throw new NotFoundException("Course not found.");
        }

        await repository.DeleteAsync(course);
        return true;
    }
}