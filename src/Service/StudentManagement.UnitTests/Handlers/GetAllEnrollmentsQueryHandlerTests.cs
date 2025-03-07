using Moq;
using StudentManagement.Domain.Handlers;
using StudentManagement.Domain.Queries;
using StudentManagement.Models.Models.DTOs;
using StudentManagement.Repository.Interfaces;
using StudentManagement.API.Exception;

namespace StudentManagement.UnitTests.Handlers
{
    public class GetAllEnrollmentsQueryHandlerTests
    {
        private readonly Mock<IEnrollmentRepository> _enrollmentRepositoryMock;
        private readonly GetAllEnrollmentsQueryHandler _handler;

        public GetAllEnrollmentsQueryHandlerTests()
        {
            _enrollmentRepositoryMock = new Mock<IEnrollmentRepository>();
            _handler = new GetAllEnrollmentsQueryHandler(_enrollmentRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ReturnsEnrollments_WhenEnrollmentsExist()
        {
            
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

            _enrollmentRepositoryMock.Setup(repo => repo.GetAllEnrollmentsAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(enrollments);

            var query = new GetAllEnrollmentsQuery();

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(enrollments[0].StudentName, result[0].StudentName);
        }

        [Fact]
        public async Task Handle_ThrowsNotFoundException_WhenNoEnrollmentsFound()
        {
            
            _enrollmentRepositoryMock.Setup(repo => repo.GetAllEnrollmentsAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<EnrollmentModel>());

            var query = new GetAllEnrollmentsQuery();

            await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(query, CancellationToken.None));
        }
    }
}
