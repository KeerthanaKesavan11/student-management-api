using Xunit;
using Moq;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.API.Controllers;
using StudentManagement.Domain.Queries;
using StudentManagement.Domain.Command;
using StudentManagement.API.Exception;
using System.Threading.Tasks;
using System.Collections.Generic;
using StudentManagement.Models.Models.DTOs;

namespace StudentManagement.Tests.Controllers
{
    public class StudentsControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly StudentsController _controller;

        public StudentsControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new StudentsController(_mediatorMock.Object);
        }

        [Fact]
        public async Task GetAllStudents_ReturnsOkResult_WithListOfStudents()
        {
            // Arrange
            var students = new List<StudentModel> { new StudentModel { StudentId = 1, StudentName = "John Doe" } };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetStudentsQuery>(), default)).ReturnsAsync(students);

            // Act
            var result = await _controller.GetAllStudents();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(students, okResult.Value);
        }

        [Fact]
        public async Task GetStudentsById_ReturnsOkResult_WithStudent()
        {
            // Arrange
            var student = new StudentModel { StudentId = 1, StudentName = "John Doe" };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetStudentsByIdQuery>(), default)).ReturnsAsync(student);

            // Act
            var result = await _controller.GetStudentsById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(student, okResult.Value);
        }

        [Fact]
        public async Task GetStudentsById_ReturnsNotFound_WhenStudentNotFound()
        {
            // Arrange
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetStudentsByIdQuery>(), default)).ThrowsAsync(new NotFoundException("Student not found"));

            // Act
            var result = await _controller.GetStudentsById(1);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Student not found", notFoundResult.Value);
        }

        [Fact]
        public async Task CreateStudent_ReturnsCreatedAtActionResult_WithStudent()
        {
            // Arrange
            var student = new StudentModel { StudentId = 1, StudentName = "John Doe" };
            var command = new CreateStudentCommand { StudentName = "John Doe" };
            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateStudentCommand>(), default)).ReturnsAsync(student);

            // Act
            var result = await _controller.CreateStudent(command);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal("GetAllStudents", createdAtActionResult.ActionName);
            Assert.Equal(student.StudentId, createdAtActionResult.RouteValues["id"]);
            Assert.Equal("Student record inserted successfully", ((dynamic)createdAtActionResult.Value).message);
            Assert.Equal(student, ((dynamic)createdAtActionResult.Value).student);
        }

        [Fact]
        public async Task UpdateStudent_ReturnsOkResult_WhenUpdateIsSuccessful()
        {
            // Arrange
            var student = new StudentModel { StudentId = 1, StudentName = "John Doe" };
            var command = new UpdateStudentCommand { StudentId = 1, StudentName = "John Doe" };
            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateStudentCommand>(), default)).ReturnsAsync(student);

            // Act
            var result = await _controller.UpdateStudent(1, command);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Student record updated successfully.", okResult.Value);
        }

    }
}