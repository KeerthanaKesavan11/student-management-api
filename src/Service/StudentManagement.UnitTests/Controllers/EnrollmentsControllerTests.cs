using Moq;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.API.Controllers;
using StudentManagement.Domain.Queries;
using StudentManagement.API.Exception;
using StudentManagement.Models.Models.DTOs;
using FluentValidation;

namespace StudentManagement.UnitTests.Controllers
{
    public class EnrollmentsControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly EnrollmentsController _controller;

        public EnrollmentsControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new EnrollmentsController(_mediatorMock.Object);
        }

        [Fact]
        public async Task GetAllEnrollments_ReturnsOkResult_WithListOfEnrollments()
        {
            var enrollments = new List<EnrollmentModel>
            {
                new EnrollmentModel { StudentName = "John Doe", Course = "Math", Duration = "1 Year", EnrollmentDate = DateOnly.FromDateTime(DateTime.Now), Grade = "A" },
                new EnrollmentModel { StudentName = "Jane Smith", Course = "Science", Duration = "1 Year", EnrollmentDate = DateOnly.FromDateTime(DateTime.Now), Grade = "B" }
            };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllEnrollmentsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(enrollments);

            var result = await _controller.GetAllEnrollments();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<EnrollmentModel>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task GetEnrollmentById_ReturnsOkResult_WithEnrollment()
        {
            var studentId = 1;
            var enrollments = new List<EnrollmentModel>
            {
                new EnrollmentModel { StudentName = "John Doe", Course = "Math", Duration = "1 Year", EnrollmentDate = DateOnly.FromDateTime(DateTime.Now), Grade = "A" }
            };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetEnrollmentByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(enrollments);

            var result = await _controller.GetEnrollmentById(studentId);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<EnrollmentModel>>(okResult.Value);
            Assert.Single(returnValue);
        }

        [Fact]
        public async Task GetEnrollmentById_ReturnsNotFound_WhenEnrollmentNotFound()
        {
            var studentId = 1;
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetEnrollmentByIdQuery>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new NotFoundException("Student not found."));

            var result = await _controller.GetEnrollmentById(studentId);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Student not found.", notFoundResult.Value);
        }

        [Fact]
        public async Task GetEnrollmentById_ReturnsBadRequest_WhenValidationExceptionOccurs()
        {
            var studentId = 1;
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetEnrollmentByIdQuery>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new ValidationException("Invalid request."));

            var result = await _controller.GetEnrollmentById(studentId);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid request.", badRequestResult.Value);
        }
    }
}
