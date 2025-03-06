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
    public class UpdateStudentCommandHandlerTests
    {
        private readonly Mock<IStudentRepository> _repositoryMock;
        private readonly UpdateStudentCommandHandler _handler;

        public UpdateStudentCommandHandlerTests()
        {
            _repositoryMock = new Mock<IStudentRepository>();
            _handler = new UpdateStudentCommandHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ValidRequest_ShouldUpdateStudent()
        {
            var command = new UpdateStudentCommand
            {
                StudentId = 1,
                StudentName = "John Doe",
                Dob = new DateOnly(2000, 1, 1),
                Email = "john.doe@example.com",
                PhoneNumber = "1234567890",
                Address = "123 Main St"
            };

            var student = new Student
            {
                StudentId = 1,
                StudentName = "John Doe",
                Dob = new DateOnly(2000, 1, 1),
                Email = "john.doe@example.com",
                PhoneNumber = "1234567890",
                Address = "123 Main St",
                IsActive = true
            };

            _repositoryMock.Setup(r => r.GetStudentAsync(command.StudentId, It.IsAny<CancellationToken>()));
                
            _repositoryMock.Setup(r => r.UpdateStudentAsync(It.IsAny<Student>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            await _handler.Handle(command, CancellationToken.None);

            _repositoryMock.Verify(r => r.UpdateStudentAsync(It.Is<Student>(s => s.StudentId == command.StudentId), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_InvalidRequest_ShouldThrowValidationException()
        {
            var command = new UpdateStudentCommand
            {
                StudentId = 1,
                StudentName = "",
                Dob = new DateOnly(2000, 1, 1),
                Email = "invalid-email",
                PhoneNumber = "1234567890",
                Address = "123 Main St"
            };

            var validator = new UpdateStudentCommandValidator();
            var validationResult = await validator.ValidateAsync(command);

            _repositoryMock.Setup(r => r.GetStudentAsync(command.StudentId, It.IsAny<CancellationToken>()));
                
            var exception = await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));

            Assert.Equal(validationResult.Errors, exception.Errors);
        }

        [Fact]
        public async Task Handle_StudentNotFound_ShouldThrowValidationException()
        {
            var command = new UpdateStudentCommand
            {
                StudentId = 1,
                StudentName = "John Doe",
                Dob = new DateOnly(2000, 1, 1),
                Email = "john.doe@example.com",
                PhoneNumber = "1234567890",
                Address = "123 Main St"
            };

            _repositoryMock.Setup(r => r.GetStudentAsync(command.StudentId, It.IsAny<CancellationToken>()));
               
            var exception = await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));

            Assert.Contains(exception.Errors, e => e.PropertyName == nameof(command.StudentId) && e.ErrorMessage == "Student does not exist.");
        }

        [Fact]
        public async Task Handle_StudentInactive_ShouldThrowValidationException()
        {
            var command = new UpdateStudentCommand
            {
                StudentId = 1,
                StudentName = "John Doe",
                Dob = new DateOnly(2000, 1, 1),
                Email = "john.doe@example.com",
                PhoneNumber = "1234567890",
                Address = "123 Main St"
            };

            var student = new Student
            {
                StudentId = 1,
                StudentName = "John Doe",
                Dob = new DateOnly(2000, 1, 1),
                Email = "john.doe@example.com",
                PhoneNumber = "1234567890",
                Address = "123 Main St",
                IsActive = false
            };

            _repositoryMock.Setup(r => r.GetStudentAsync(command.StudentId, It.IsAny<CancellationToken>()));
              
            var exception = await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));

            Assert.Contains(exception.Errors, e => e.PropertyName == nameof(command.StudentId) && e.ErrorMessage == "Student record is in Inactive status");
        }
    }
}
