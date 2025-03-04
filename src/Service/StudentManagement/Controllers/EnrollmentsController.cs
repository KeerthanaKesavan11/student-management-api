using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Domain.Queries;

namespace StudentManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnrollmentsController : Controller
    {
        private readonly IMediator _mediator;

        public EnrollmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEnrollments()
        {
            var query = new GetAllEnrollmentsQuery();
            var enrollments = await _mediator.Send(query);
            return Ok(enrollments);
        }

        [HttpGet("{studentId}")]
        public async Task<IActionResult> GetEnrollmentById(int studentId)
        {
            var query = new GetEnrollmentByIdQuery { StudentId = studentId };
            var enrollment = await _mediator.Send(query);
            if (enrollment == null)
            {
                return NotFound("Enrollment not found.");
            }
            return Ok(enrollment);
        }
    }
}
