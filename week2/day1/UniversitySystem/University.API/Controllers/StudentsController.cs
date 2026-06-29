namespace University.API.Controllers;

using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using University.API.Filters;
using University.Core.Forms;
using University.Core.Services;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[TypeFilter(typeof(ApiExceptionFilter))]
public class StudentsController : ControllerBase
{
    private readonly IStudentService _service;
    private readonly ILogger<StudentsController> _logger;

    public StudentsController(IStudentService studentService, ILogger<StudentsController> logger)
    {
        _service = studentService;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ApiResponse> GetAll()
    {
        _logger.LogInformation("GetAll endpoint was called. Fetching all students...");
        var students = await _service.GetAllAsync();
        return new ApiResponse(students);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ApiResponse> GetById(int id)
    {
        _logger.LogInformation("GetById endpoint was called. Fetching student"+id+"...");
        var student = await _service.GetByIdAsync(id);
        return new ApiResponse(student);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ApiResponse> Create([FromBody] CreateStudentForm form)
    {
        _logger.LogInformation("Create endpoint was called.");
        await _service.CreateAsync(form);
        return new ApiResponse("Student created successfully.");
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ApiResponse> Update(int id, [FromBody] UpdateStudentForm form)
    {
        _logger.LogInformation("Update endpoint was called.");
        await _service.UpdateAsync(id, form);
        return new ApiResponse("Student updated successfully.");
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ApiResponse> Delete(int id)
    {
        _logger.LogInformation("Delete endpoint was called.");
        await _service.DeleteAsync(id);
        return new ApiResponse("Student deleted successfully.");
    }
}