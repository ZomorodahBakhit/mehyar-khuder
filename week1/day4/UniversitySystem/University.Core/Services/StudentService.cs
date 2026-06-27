namespace University.Core.Services;

using System.Linq;
using University.Core.DTOs;
using University.Core.Forms;
using University.Data.Entities;
using University.Data.Repositories;

public interface IStudentService
{
    Task<IEnumerable<StudentDto>> GetAllAsync();
    Task<StudentDto?> GetByIdAsync(int id);
    Task<StudentDto> CreateAsync(CreateStudentForm form);
    Task<bool> UpdateAsync(int id, UpdateStudentForm form);
    Task<bool> DeleteAsync(int id);
}

public class StudentService(IStudentRepository repository) : IStudentService
{
    public async Task<IEnumerable<StudentDto>> GetAllAsync()
    {
        var students = await repository.GetAllAsync();
        return students.Select(s => new StudentDto { Id = s.Id, Name = s.Name, Email = s.Email });
    }

    public async Task<StudentDto?> GetByIdAsync(int id)
    {
        var student = await repository.GetByIdAsync(id);
        if (student is null) return null;

        return new StudentDto { Id = student.Id, Name = student.Name, Email = student.Email };
    }

    public async Task<StudentDto> CreateAsync(CreateStudentForm form)
    {
        var student = new Student
        {
            Name = form.Name,
            Email = form.Email
        };

        var createdStudent = await repository.AddAsync(student);
        return new StudentDto { Id = createdStudent.Id, Name = createdStudent.Name, Email = createdStudent.Email };
    }

    public async Task<bool> UpdateAsync(int id, UpdateStudentForm form)
    {
        var student = await repository.GetByIdAsync(id);
        if (student is null) return false;

        student.Name = form.Name;
        student.Email = form.Email;

        await repository.UpdateAsync(student);
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var student = await repository.GetByIdAsync(id);
        if (student is null) return false;

        await repository.DeleteAsync(student);
        return true;
    }
}