using Moq;
using Xunit;
using System.Threading;
using System.Threading.Tasks;
using StudentManagement.Domain.Handlers;
using StudentManagement.Domain.Queries;
using StudentManagement.Models.Models.DTOs;
using StudentManagement.Repository.Interfaces;
using StudentManagement.API.Exception;
using System.Collections.Generic;

namespace StudentManagement.UnitTests.Handlers
{
    public class GetEnrollmentByIdQueryHandlerTests
    {
        private readonly Mock<IEnrollmentRepository> _enrollmentRepositoryMock;
        private readonly Mock<IStudentRepository> _studentRepositoryMock;
        private readonly GetEnrollmentByIdQueryHandler _handler;

        public GetEnrollmentByIdQueryHandlerTests()
        {
            _enrollmentRepositoryMock = new Mock<IEnrollmentRepository>();
            _studentRepositoryMock = new Mock<IStudentRepository>();
            _handler = new GetEnrollmentByIdQueryHandler(_enrollmentRepositoryMock.Object, _studentRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ReturnsEnrollments_WhenEnrollmentsExist()
        {
           
            var studentId = 1;
            var student = new StudentModel { StudentId = studentId, IsActive = true };
            var enrollments = new List<EnrollmentModel>
            {
                new EnrollmentModel
                {
                    StudentName = "John Doe",
                    Course = "Math",
                    Duration = "3 months",
                    EnrollmentDate = DateOnly.FromDateTime(DateTime.Now),
                    Grade = "A"
                }
            };

            _studentRepositoryMock.Setup(repo => repo.GetStudentsByIdAsync(studentId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(student);
            _enrollmentRepositoryMock.Setup(repo => repo.GetEnrollmentByIdAsync(studentId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(enrollments);

            var query = new GetEnrollmentByIdQuery { StudentId = studentId };

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(enrollments[0].StudentName, result[0].StudentName);
        }

        [Fact]
        public async Task Handle_ThrowsNotFoundException_WhenStudentDoesNotExist()
        {
            
            var studentId = 1;
            _studentRepositoryMock.Setup(repo => repo.GetStudentsByIdAsync(studentId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((StudentModel)null);

            var query = new GetEnrollmentByIdQuery { StudentId = studentId };

            await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(query, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ThrowsNotFoundException_WhenStudentIsInactive()
        {
           
            var studentId = 1;
            var student = new StudentModel { StudentId = studentId, IsActive = false };
            _studentRepositoryMock.Setup(repo => repo.GetStudentsByIdAsync(studentId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(student);

            var query = new GetEnrollmentByIdQuery { StudentId = studentId };

            await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(query, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ThrowsNotFoundException_WhenNoEnrollmentsFound()
        {
           
            var studentId = 1;
            var student = new StudentModel { StudentId = studentId, IsActive = true };
            _studentRepositoryMock.Setup(repo => repo.GetStudentsByIdAsync(studentId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(student);
            _enrollmentRepositoryMock.Setup(repo => repo.GetEnrollmentByIdAsync(studentId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<EnrollmentModel>());

            var query = new GetEnrollmentByIdQuery { StudentId = studentId };

            await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(query, CancellationToken.None));
        }
    }
}
