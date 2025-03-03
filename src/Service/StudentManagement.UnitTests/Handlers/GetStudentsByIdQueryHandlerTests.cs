using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using StudentManagement.API.Exception;
using StudentManagement.Domain.Handlers;
using StudentManagement.Domain.Queries;
using StudentManagement.Domain.Validators;
using StudentManagement.Models.Models;
using StudentManagement.Models.Models.DTOs;
using StudentManagement.Repository.Interfaces;
using Xunit;

namespace StudentManagement.UnitTests.Handlers
{
    public class GetStudentsByIdQueryHandlerTests
    {
        private readonly Mock<IStudentRepository> _repositoryMock;
        private readonly GetStudentsByIdQueryHandler _handler;

        public GetStudentsByIdQueryHandlerTests()
        {
            _repositoryMock = new Mock<IStudentRepository>();
            _handler = new GetStudentsByIdQueryHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ValidRequest_ShouldReturnStudent()
        {
            // Arrange
            var query = new GetStudentsByIdQuery { StudentId = 1 };
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

            _repositoryMock.Setup(r => r.GetStudentsByIdAsync(query.StudentId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(studentModel);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(studentModel.StudentId, result.StudentId);
            Assert.Equal(studentModel.StudentName, result.StudentName);
            Assert.Equal(studentModel.Dob, result.Dob);
            Assert.Equal(studentModel.Email, result.Email);
            Assert.Equal(studentModel.PhoneNumber, result.PhoneNumber);
            Assert.Equal(studentModel.Address, result.Address);
            Assert.Equal(studentModel.IsActive, result.IsActive);
            _repositoryMock.Verify(r => r.GetStudentsByIdAsync(query.StudentId, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_InvalidRequest_ShouldThrowValidationException()
        {
            // Arrange
            var query = new GetStudentsByIdQuery { StudentId = 0 };
            var validator = new GetStudentsByIdQueryValidator();
            var results = await validator.ValidateAsync(query, CancellationToken.None);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(query, CancellationToken.None));
            Assert.Equal(results.Errors, exception.Errors);
        }

        [Fact]
        public async Task Handle_StudentNotFound_ShouldThrowNotFoundException()
        {
            // Arrange
            var query = new GetStudentsByIdQuery { StudentId = 1 };

            _repositoryMock.Setup(r => r.GetStudentsByIdAsync(query.StudentId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((StudentModel)null);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(query, CancellationToken.None));
        }
    }
}

