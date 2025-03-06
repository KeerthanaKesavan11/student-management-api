using Moq;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.API.Controllers;
using StudentManagement.Domain.Queries;
using StudentManagement.Models.Models.DTOs;

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
                new EnrollmentModel
                {
                    StudentName = "John Doe",
                    Course = "Math",
                    Duration = "1 Year",
                    EnrollmentDate = new DateOnly(2023, 1, 1),
                    Grade = "A"
                }
            };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllEnrollmentsQuery>(), default)).ReturnsAsync(enrollments);

            var result = await _controller.GetAllEnrollments();

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(enrollments, okResult.Value);
        }

        [Fact]
        public async Task GetEnrollmentById_ReturnsOkResult_WithEnrollment()
        {
            var enrollments = new List<EnrollmentModel>
            {
                new EnrollmentModel
                {
                    StudentName = "John Doe",
                    Course = "Math",
                    Duration = "3 months",
                    EnrollmentDate = new DateOnly(2023, 1, 1),
                    Grade = "A"
                }
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetEnrollmentByIdQuery>(), default)).ReturnsAsync(enrollments);

            var result = await _controller.GetEnrollmentById(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(enrollments, okResult.Value);
        }

        [Fact]
        public async Task GetEnrollmentById_ReturnsNotFound_WhenEnrollmentNotFound()
        {
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetEnrollmentByIdQuery>(), default)).ReturnsAsync(new List<EnrollmentModel>());

            var result = await _controller.GetEnrollmentById(1);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Enrollment not found.", notFoundResult.Value);
        }
    }
}
