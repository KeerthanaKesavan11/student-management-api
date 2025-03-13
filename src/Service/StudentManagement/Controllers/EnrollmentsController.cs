using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.API.Exception;
using StudentManagement.Domain.Queries;

namespace StudentManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    public class EnrollmentsController : Controller
    {
        private readonly IMediator _mediator;

        public EnrollmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEnrollments([FromQuery] string? filter)
        {
            var query = new GetAllEnrollmentsQuery { Filter = filter };
            var enrollments = await _mediator.Send(query);
            return Ok(enrollments);
        }

        [HttpGet("{studentId}")]        
        public async Task<IActionResult> GetEnrollmentById(int studentId)
        {
            try
            {
                var query = new GetEnrollmentByIdQuery { StudentId = studentId };
                var enrollment = await _mediator.Send(query);
                return Ok(enrollment);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }


        }
    }
}
