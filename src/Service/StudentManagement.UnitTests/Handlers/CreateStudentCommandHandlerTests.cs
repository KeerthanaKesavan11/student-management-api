using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using StudentManagement.Domain.Command;
using StudentManagement.Domain.Handlers;
using StudentManagement.Domain.Validators;
using StudentManagement.Models.Models;
using StudentManagement.Repository.Interfaces;
using Xunit;

namespace StudentManagement.UnitTests.Handlers
{
    public class CreateStudentCommandHandlerTests
    {
        private readonly Mock<IStudentRepository> _repositoryMock;
        private readonly CreateStudentCommandHandler _handler;

        public CreateStudentCommandHandlerTests()
        {
            _repositoryMock = new Mock<IStudentRepository>();
            _handler = new CreateStudentCommandHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ValidRequest_ShouldCreateStudent()
        {
            // Arrange
            var command = new CreateStudentCommand
            {
                StudentName = "John Doe",
                Dob = new DateOnly(2000, 1, 1),
                Email = "john.doe@example.com",
                PhoneNumber = "1234567890",
                Address = "123 Main St"
            };

            var student = new Student
            {
                StudentId = 1,
                StudentName = command.StudentName,
                Dob = command.Dob,
                Email = command.Email,
                PhoneNumber = command.PhoneNumber,
                Address = command.Address
            };

            _repositoryMock.Setup(r => r.AddStudentAsync(It.IsAny<Student>(), It.IsAny<CancellationToken>()))
                .Callback<Student, CancellationToken>((s, ct) => s.StudentId = student.StudentId)
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(student.StudentId, result.StudentId);
            Assert.Equal(student.StudentName, result.StudentName);
            Assert.Equal(student.Dob, result.Dob);
            Assert.Equal(student.Email, result.Email);
            Assert.Equal(student.PhoneNumber, result.PhoneNumber);
            Assert.Equal(student.Address, result.Address);
            _repositoryMock.Verify(r => r.AddStudentAsync(It.IsAny<Student>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_InvalidRequest_ShouldThrowValidationException()
        {
            // Arrange
            var command = new CreateStudentCommand
            {
                StudentName = "",
                Dob = new DateOnly(2000, 1, 1),
                Email = "invalid-email",
                PhoneNumber = "1234567890",
                Address = "123 Main St"
            };

            var validator = new CreateStudentCommandValidator();
            var results = await validator.ValidateAsync(command, CancellationToken.None);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
            Assert.Equal(results.Errors, exception.Errors);
        }
    }
}
