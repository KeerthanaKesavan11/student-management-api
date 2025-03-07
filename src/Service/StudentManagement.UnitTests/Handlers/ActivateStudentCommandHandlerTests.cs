using FluentValidation;
using FluentValidation.Results;
using Moq;
using StudentManagement.Domain.Command;
using StudentManagement.Domain.Handlers;
using StudentManagement.Models.Models;
using StudentManagement.Models.Models.DTOs;
using StudentManagement.Repository.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

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

            _validatorMock.Setup(v => v.ValidateAsync(command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());

            _repositoryMock.Setup(r => r.GetStudentAsync(command.StudentId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new StudentModel
                {
                    StudentId = student.StudentId,
                    StudentName = student.StudentName,
                    Dob = student.Dob,
                    Email = student.Email,
                    PhoneNumber = student.PhoneNumber,
                    Address = student.Address,
                    IsActive = student.IsActive ?? false
                });
            _repositoryMock.Setup(r => r.ActivateStudentAsync(It.IsAny<Student>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            await _handler.Handle(command, CancellationToken.None);

            _repositoryMock.Verify(r => r.ActivateStudentAsync(It.Is<Student>(s => s.StudentId == command.StudentId && s.IsActive == true), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_InvalidRequest_ShouldThrowValidationException()
        {
            var command = new ActivateStudentCommand { StudentId = 1 };
            var validationFailures = new List<ValidationFailure>
            {
                new ValidationFailure("StudentId", "Student does not exist.")
            };

            _validatorMock.Setup(v => v.ValidateAsync(command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult(validationFailures));

            var exception = await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));

            Assert.NotEmpty(exception.Errors);
            Assert.Equal(validationFailures.Count, exception.Errors.Count());
            Assert.Equal(validationFailures[0].PropertyName, exception.Errors.First().PropertyName);
            Assert.Equal(validationFailures[0].ErrorMessage, exception.Errors.First().ErrorMessage);
        }

        [Fact]
        public async Task Handle_StudentNotFound_ShouldThrowValidationException()
        {
            var command = new ActivateStudentCommand { StudentId = 1 };

            _validatorMock.Setup(v => v.ValidateAsync(command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());

            _repositoryMock.Setup(r => r.GetStudentAsync(command.StudentId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((StudentModel)null);

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

            _validatorMock.Setup(v => v.ValidateAsync(command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());

            _repositoryMock.Setup(r => r.GetStudentAsync(command.StudentId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new StudentModel
                {
                    StudentId = student.StudentId,
                    StudentName = student.StudentName,
                    Dob = student.Dob,
                    Email = student.Email,
                    PhoneNumber = student.PhoneNumber,
                    Address = student.Address,
                    IsActive = student.IsActive ?? false
                });

            var exception = await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));

            Assert.Contains(exception.Errors, e => e.PropertyName == nameof(command.StudentId) && e.ErrorMessage == "Student record is already in active status");
        }
    }
}
