using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using StudentManagement.Domain.Handlers;
using StudentManagement.Domain.Queries;
using StudentManagement.Models.Models;
using StudentManagement.Models.Models.DTOs;
using StudentManagement.Repository.Interfaces;
using Xunit;

namespace StudentManagement.UnitTests.Handlers
{
    public class GetStudentsQueryHandlerTests
    {
        private readonly Mock<IStudentRepository> _repositoryMock;
        private readonly GetStudentsQueryHandler _handler;

        public GetStudentsQueryHandlerTests()
        {
            _repositoryMock = new Mock<IStudentRepository>();
            _handler = new GetStudentsQueryHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnAllStudents()
        {
            // Arrange
            var students = new List<StudentModel>
            {
                new StudentModel
                {
                    StudentId = 1,
                    StudentName = "John Doe",
                    Dob = new DateOnly(2000, 1, 1),
                    Email = "john.doe@example.com",
                    PhoneNumber = "1234567890",
                    Address = "123 Main St",
                    IsActive = true
                },
                new StudentModel
                {
                    StudentId = 2,
                    StudentName = "Jane Doe",
                    Dob = new DateOnly(2001, 2, 2),
                    Email = "jane.doe@example.com",
                    PhoneNumber = "0987654321",
                    Address = "456 Elm St",
                    IsActive = true
                }
            };

            _repositoryMock.Setup(r => r.GetAllStudentsAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(students);

            // Act
            var result = await _handler.Handle(new GetStudentsQuery(), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Equal(students, result);
            _repositoryMock.Verify(r => r.GetAllStudentsAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_NoStudents_ShouldReturnEmptyList()
        {
            // Arrange
            var students = new List<StudentModel>();

            _repositoryMock.Setup(r => r.GetAllStudentsAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(students);

            // Act
            var result = await _handler.Handle(new GetStudentsQuery(), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            _repositoryMock.Verify(r => r.GetAllStudentsAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}


