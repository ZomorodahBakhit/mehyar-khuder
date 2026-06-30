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

public interface IStudentService
{
    Task<IEnumerable<StudentDto>> GetAllAsync();
    Task<StudentDto?> GetByIdAsync(int id);
    Task<StudentDto> CreateAsync(CreateStudentForm form);
    Task<bool> UpdateAsync(int id, UpdateStudentForm form);
    Task<bool> DeleteAsync(int id);
}
public class StudentService(IStudentRepository repository, ILogger<StudentService> logger) : IStudentService
{
    public async Task<IEnumerable<StudentDto>> GetAllAsync()
    {
        var students = await repository.GetAllAsync();
        return students.Select(s => new StudentDto { Id = s.Id, Name = s.Name, Email = s.Email });
    }

    public async Task<StudentDto?> GetByIdAsync(int id)
    {
        var student = await repository.GetByIdAsync(id);
        if (student is null)
        {
            logger.LogError("Student with ID {Id} not found.", id); 
            throw new NotFoundException("Student not found.");
        }
        return new StudentDto { Id = student.Id, Name = student.Name, Email = student.Email };
    }

    public async Task<StudentDto> CreateAsync(CreateStudentForm form)
    {
        FormValidator.Validate(form);
        if (await repository.IsEmailExistsAsync(form.Email))
        {
            logger.LogError("Failed to create student. Email already exists: {Email}", form.Email);
            throw new BusinessException("Email already exists.");
        }
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
        FormValidator.Validate(form);
        var student = await repository.GetByIdAsync(id);
        if (student is null)
        {
            logger.LogError("Student with ID {Id} not found.", id);
            throw new NotFoundException("Student not found.");
        }

        if (await repository.IsEmailExistsAsync(form.Email, id))
        {
            logger.LogError("Failed to update student. Email already exists: {Email}", form.Email);
            throw new BusinessException("Email already exists.");
        }

        student.Name = form.Name;
        student.Email = form.Email;

        await repository.UpdateAsync(student);
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var student = await repository.GetByIdAsync(id);
        if (student is null)
        {
            logger.LogError("Student with ID {Id} not found.", id);
            throw new NotFoundException("Student not found.");
        }

        await repository.DeleteAsync(student);
        return true;
    }
}