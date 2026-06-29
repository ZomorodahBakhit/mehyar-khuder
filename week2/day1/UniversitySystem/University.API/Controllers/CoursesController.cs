namespace University.API.Controllers;

using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using University.API.Filters;
using University.Core.Forms;
using University.Core.Services;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[TypeFilter(typeof(ApiExceptionFilter))]
public class CoursesController : ControllerBase
{
    private readonly ICourseService _service;
    private readonly ILogger<CoursesController> _logger; 

    public CoursesController(ICourseService courseService, ILogger<CoursesController> logger)
    {
        _service = courseService;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ApiResponse> GetAll()
    {
        _logger.LogInformation("GetAll endpoint was called. Fetching all courses...");
        var courses = await _service.GetAllAsync();
        return new ApiResponse(courses);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ApiResponse> GetById(int id)
    {
        _logger.LogInformation("GetById endpoint was called. Fetching course" + id + "...");

        var course = await _service.GetByIdAsync(id);
        return new ApiResponse(course);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ApiResponse> Create([FromBody] CreateCourseForm form)
    {
        _logger.LogInformation("Create endpoint was called.");
        await _service.CreateAsync(form);
        return new ApiResponse("Course created successfully.");
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ApiResponse> Update(int id, [FromBody] UpdateCourseForm form)
    {
        _logger.LogInformation("Update endpoint was called.");
        await _service.UpdateAsync(id, form);
        return new ApiResponse("Course updated successfully.");
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ApiResponse> Delete(int id)
    {
        _logger.LogInformation("Delete endpoint was called.");
        await _service.DeleteAsync(id);
        return new ApiResponse("Course deleted successfully.");
    }
}
