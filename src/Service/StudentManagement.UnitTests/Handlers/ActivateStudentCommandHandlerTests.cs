using FluentValidation;
using FluentValidation.Results;
using Moq;
using StudentManagement.Domain.Command;
using StudentManagement.Domain.Handlers;
using StudentManagement.Models.Models;
using StudentManagement.Models.Models.DTOs;
using StudentManagement.Repository.Interfaces;

namespace StudentManagement.UnitTests.Handlers
{
    public class ActivateStudentCommandHandlerTests
    {
        private readonly Mock<IStudentRepository> _repositoryMock;
        private readonly Mock<IValidator<ActivateStudentCommand>> _validatorMock;
        private readonly ActivateStudentCommandHandler _handler;

        public ActivateStudentCommandHandlerTests()
        {
            _repositoryMock = new Mock<IStudentRepository>();
            _validatorMock = new Mock<IValidator<ActivateStudentCommand>>();
            _handler = new ActivateStudentCommandHandler(_repositoryMock.Object, _validatorMock.Object);
        }

        [Fact]
        public async Task Handle_ValidRequest_ShouldActivateStudent()
        {
           var command = new ActivateStudentCommand { StudentId = 1 };
            var studentModel = new StudentModel
            {
                StudentId = 1,
                StudentName = "John Doe",
                Dob = new DateOnly(2000, 1, 1),
                Email = "john.doe@example.com",
                PhoneNumber = "1234567890",
                Address = "123 Main St",
                IsActive = false
            };

            _validatorMock.Setup(v => v.ValidateAsync(command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());
            _repositoryMock.Setup(r => r.GetStudentAsync(command.StudentId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(studentModel);
            _repositoryMock.Setup(r => r.ActivateStudentAsync(It.IsAny<Student>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var result = await _handler.Handle(command, CancellationToken.None);
             Assert.True(result);
            _repositoryMock.Verify(r => r.ActivateStudentAsync(It.Is<Student>(s => s.IsActive == true), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_InvalidRequest_ShouldThrowValidationException()
        {
            var command = new ActivateStudentCommand { StudentId = 1 };
            var validationFailures = new List<ValidationFailure> { new ValidationFailure("StudentId", "Invalid student ID") };
            var validationResult = new ValidationResult(validationFailures);

            _validatorMock.Setup(v => v.ValidateAsync(command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(validationResult);
            await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_StudentNotFound_ShouldThrowValidationException()
        {
           
            var command = new ActivateStudentCommand { StudentId = 1 };

            _validatorMock.Setup(v => v.ValidateAsync(command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());
            _repositoryMock.Setup(r => r.GetStudentAsync(command.StudentId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((StudentModel)null);

           
            await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}
