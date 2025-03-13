using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Domain.Queries;
using FluentValidation;
using StudentManagement.API.Exception;
using StudentManagement.Domain.Command;

namespace StudentManagement.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]

    public class StudentsController : Controller
    {
        private readonly IMediator _mediator;
       
        public StudentsController(IMediator mediator)
        {
            _mediator = mediator;
        }
       
        [HttpGet]
        [ApiVersion("1.0")]
        public async Task<IActionResult> GetAllStudentsV1()
        {
            var query = new GetStudentsQuery();
            var students = await _mediator.Send(query);
            return Ok(students);
        }
        [HttpGet]
        [ApiVersion("2.0")]
        public async Task<IActionResult> GetAllStudentsV2()
        {
            var query = new GetStudentsQueryV2();
            var students = await _mediator.Send(query);
            return Ok(students);
        }

        [HttpGet("{studentId}")]
        [ApiVersion("1.0")]
        public async Task<IActionResult> GetStudentsById(int studentId)
        {
            try
            {
                var query = new GetStudentsByIdQuery { StudentId = studentId };
                var students = await _mediator.Send(query);
                return Ok(students);
            }
            catch(ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost]
        [ApiVersion("1.0")]
        public async Task<IActionResult> CreateStudent([FromBody] CreateStudentCommand command)
        {
            try
            {
                var student = await _mediator.Send(command);
                return CreatedAtAction(nameof(GetAllStudentsV1), new { id = student.StudentId }, student );
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{studentId}")]
        [ApiVersion("1.0")]
        public async Task<IActionResult> UpdateStudent(int studentId, [FromBody] UpdateStudentCommand command)
        {
            if (studentId != command.StudentId)
            {
                return BadRequest("Student ID mismatch.");
            }
            try
            {
                await _mediator.Send(command);
                return Ok("Student record updated successfully.");
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("{studentId}/activate")]
        [ApiVersion("1.0")]
        public async Task<IActionResult> ActivateStudent(int studentId)
        {
            try
            {
                var command = new ActivateStudentCommand { StudentId = studentId };
                await _mediator.Send(command);
                return Ok("Student record activated successfully.");
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{studentId}")]
        [ApiVersion("1.0")]
        public async Task<IActionResult> RemoveStudent(int studentId)
        {
            try
            {
                var command = new RemoveStudentCommand { StudentId = studentId };
                await _mediator.Send(command);
                return Ok("Student record deleted successfully.");
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
