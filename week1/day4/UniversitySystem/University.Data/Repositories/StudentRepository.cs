namespace University.Data.Repositories;

using Microsoft.EntityFrameworkCore;
using University.Data.Entities;

public interface IStudentRepository
{
    Task<IEnumerable<Student>> GetAllAsync();
    Task<Student?> GetByIdAsync(int id);
    Task<Student> AddAsync(Student student);
    Task UpdateAsync(Student student);
    Task DeleteAsync(Student student);
}

public class StudentRepository(UniversityDbContext context) : IStudentRepository
{
    public async Task<IEnumerable<Student>> GetAllAsync()
    {
        return await context.Set<Student>().ToListAsync();
    }

    public async Task<Student?> GetByIdAsync(int id)
    {
        return await context.Set<Student>().FindAsync(id);
    }

    public async Task<Student> AddAsync(Student student)
    {
        await context.Set<Student>().AddAsync(student);
        await context.SaveChangesAsync();
        return student;
    }

    public async Task UpdateAsync(Student student)
    {
        context.Set<Student>().Update(student);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Student student)
    {
        context.Set<Student>().Remove(student);
        await context.SaveChangesAsync();
    }
}
