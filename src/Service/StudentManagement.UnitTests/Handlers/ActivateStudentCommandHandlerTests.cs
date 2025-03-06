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
    public class ActivateStudentCommandHandlerTests
    {
        private readonly Mock<IStudentRepository> _repositoryMock;
        private readonly ActivateStudentCommandHandler _handler;

        public ActivateStudentCommandHandlerTests()
        {
            _repositoryMock = new Mock<IStudentRepository>();
            _handler = new ActivateStudentCommandHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ValidRequest_ShouldActivateStudent()
        {
            var command = new ActivateStudentCommand { StudentId = 1 };
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
            _repositoryMock.Setup(r => r.ActivateStudentAsync(It.IsAny<Student>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            await _handler.Handle(command, CancellationToken.None);

            _repositoryMock.Verify(r => r.ActivateStudentAsync(It.Is<Student>(s => s.StudentId == command.StudentId && s.IsActive == true), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_InvalidRequest_ShouldThrowValidationException()
        {
            var command = new ActivateStudentCommand { StudentId = 1 };
            var validator = new ActivateStudentCommandValidator();
            var validationResult = await validator.ValidateAsync(command);

            _repositoryMock.Setup(r => r.GetStudentAsync(command.StudentId, It.IsAny<CancellationToken>()));
                
            var exception = await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));

            Assert.Equal(validationResult.Errors, exception.Errors);
        }

        [Fact]
        public async Task Handle_StudentNotFound_ShouldThrowValidationException()
        {
            var command = new ActivateStudentCommand { StudentId = 1 };

            _repositoryMock.Setup(r => r.GetStudentAsync(command.StudentId, It.IsAny<CancellationToken>()));
               
            var exception = await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));

            Assert.Contains(exception.Errors, e => e.PropertyName == nameof(command.StudentId) && e.ErrorMessage == "Student does not exist.");
        }

        [Fact]
        public async Task Handle_StudentAlreadyActive_ShouldThrowValidationException()
        {
            var command = new ActivateStudentCommand { StudentId = 1 };
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
                
            var exception = await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));

            Assert.Contains(exception.Errors, e => e.PropertyName == nameof(command.StudentId) && e.ErrorMessage == "Student record is already in active status");
        }
    }
}


