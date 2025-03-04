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
    public class UpdateStudentCommandHandlerTests
    {
        private readonly Mock<IStudentRepository> _repositoryMock;
        private readonly Mock<IValidator<UpdateStudentCommand>> _validatorMock;
        private readonly UpdateStudentCommandHandler _handler;

        public UpdateStudentCommandHandlerTests()
        {
            _repositoryMock = new Mock<IStudentRepository>();
            _validatorMock = new Mock<IValidator<UpdateStudentCommand>>();
            _handler = new UpdateStudentCommandHandler(_repositoryMock.Object, _validatorMock.Object);
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

            var studentModel = new StudentModel
            {
                StudentId = 1,
                StudentName = "John Doe",
                Dob = new DateOnly(2000, 1, 1),
                Email = "john.doe@example.com",
                PhoneNumber = "1234567890",
                Address = "123 Main St",
                IsActive = true
            };

            _validatorMock.Setup(v => v.ValidateAsync(command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());
            _repositoryMock.Setup(r => r.GetStudentAsync(command.StudentId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(studentModel);
            _repositoryMock.Setup(r => r.UpdateStudentAsync(It.IsAny<Student>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(studentModel.StudentId, result.StudentId);
            Assert.Equal(studentModel.StudentName, result.StudentName);
            Assert.Equal(studentModel.Dob, result.Dob);
            Assert.Equal(studentModel.Email, result.Email);
            Assert.Equal(studentModel.PhoneNumber, result.PhoneNumber);
            Assert.Equal(studentModel.Address, result.Address);
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

            var validationFailures = new List<ValidationFailure> { new ValidationFailure("StudentName", "Invalid student name") };
            var validationResult = new ValidationResult(validationFailures);

            _validatorMock.Setup(v => v.ValidateAsync(command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(validationResult);

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

            _validatorMock.Setup(v => v.ValidateAsync(command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());
            _repositoryMock.Setup(r => r.GetStudentAsync(command.StudentId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((StudentModel)null);

            await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}
