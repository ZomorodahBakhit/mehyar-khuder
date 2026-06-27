namespace University.API.Controllers;

using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using University.Core.Forms;
using University.Core.Services;

[ApiController]
[Route("api/[controller]")]
public class StudentsController(IStudentService studentService) : ControllerBase
{
    [HttpGet]
    public async Task<ApiResponse> GetAll()
    {
        var students = await studentService.GetAllAsync();
        return new ApiResponse(students);
    }

    [HttpGet("{id:int}")]
    public async Task<ApiResponse> GetById(int id)
    {
        var student = await studentService.GetByIdAsync(id);
        if (student is null)
        {
            return new ApiResponse("Student not found", statusCode: 404);
        }
        return new ApiResponse(student);
    }

    [HttpPost]
    public async Task<ApiResponse> Create([FromBody] CreateStudentForm form)
    {
        await studentService.CreateAsync(form);
        return new ApiResponse("Student created successfully.");
    }

    [HttpPut("{id:int}")]
    public async Task<ApiResponse> Update(int id, [FromBody] UpdateStudentForm form)
    {
        var updated = await studentService.UpdateAsync(id, form);

        if (!updated)
        {
            return new ApiResponse("Student not found", statusCode: 404);
        }
        return new ApiResponse("Student updated successfully.");
    }

    [HttpDelete("{id:int}")]
    public async Task<ApiResponse> Delete(int id)
    {
        var deleted = await studentService.DeleteAsync(id);
        if (!deleted)
        {
            return new ApiResponse("Student not found", statusCode: 404);
        }
        return new ApiResponse("Student deleted successfully.");
    }
}