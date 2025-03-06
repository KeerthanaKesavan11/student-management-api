using FluentValidation;
using FluentValidation.Results;
using Moq;
using StudentManagement.Domain.Command;
using StudentManagement.Domain.Handlers;
using StudentManagement.Models.Models;
using StudentManagement.Repository.Interfaces;
using Xunit;

namespace StudentManagement.UnitTests.Handlers
{
    public class RemoveStudentCommandHandlerTests
    {
        private readonly Mock<IStudentRepository> _repositoryMock;
        private readonly RemoveStudentCommandHandler _handler;

        public RemoveStudentCommandHandlerTests()
        {
            _repositoryMock = new Mock<IStudentRepository>();
            _handler = new RemoveStudentCommandHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ValidRequest_ShouldRemoveStudent()
        {
            var command = new RemoveStudentCommand { StudentId = 1 };
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
            _repositoryMock.Setup(r => r.DeleteStudentAsync(It.IsAny<Student>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            await _handler.Handle(command, CancellationToken.None);

            _repositoryMock.Verify(r => r.DeleteStudentAsync(It.Is<Student>(s => s.StudentId == command.StudentId), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_InvalidRequest_ShouldThrowValidationException()
        {
            var command = new RemoveStudentCommand { StudentId = 0 };
            var validationFailures = new List<ValidationFailure> { new ValidationFailure("StudentId", "Invalid student ID") };
            var validationResult = new ValidationResult(validationFailures);

            var validatorMock = new Mock<IValidator<RemoveStudentCommand>>();
            validatorMock.Setup(v => v.ValidateAsync(command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(validationResult);

            var handler = new RemoveStudentCommandHandler(_repositoryMock.Object);

            await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_StudentNotFound_ShouldThrowValidationException()
        {
            var command = new RemoveStudentCommand { StudentId = 1 };

            _repositoryMock.Setup(r => r.GetStudentAsync(command.StudentId, It.IsAny<CancellationToken>()));
               
            var exception = await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));

            Assert.Contains(exception.Errors, e => e.PropertyName == nameof(command.StudentId) && e.ErrorMessage == "Student does not exist.");
        }

        [Fact]
        public async Task Handle_StudentAlreadyInactive_ShouldThrowValidationException()
        {
            var command = new RemoveStudentCommand { StudentId = 1 };
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

            Assert.Contains(exception.Errors, e => e.PropertyName == nameof(command.StudentId) && e.ErrorMessage == "Student record is already in InActive status");
        }
    }
}
