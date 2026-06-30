namespace University.Data.Repositories;

using Microsoft.EntityFrameworkCore;
using University.Data.Entities;

public interface ICourseRepository
{
    Task<IEnumerable<Course>> GetAllAsync();
    Task<Course?> GetByIdAsync(int id);
    Task<Course> AddAsync(Course course);
    Task UpdateAsync(Course course);
    Task DeleteAsync(Course course);
}

public class CourseRepository(UniversityDbContext context) : ICourseRepository
{
    public async Task<IEnumerable<Course>> GetAllAsync()
    {
        return await context.Set<Course>().ToListAsync();
    }

    public async Task<Course?> GetByIdAsync(int id)
    {
        return await context.Set<Course>().FindAsync(id);
    }

    public async Task<Course> AddAsync(Course course)
    {
        await context.Set<Course>().AddAsync(course);
        await context.SaveChangesAsync();
        return course;
    }

    public async Task UpdateAsync(Course course)
    {
        context.Set<Course>().Update(course);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Course course)
    {
        context.Set<Course>().Remove(course);
        await context.SaveChangesAsync();
    }
}
