using MediatR;
using StudentManagement.API.Exception;
using StudentManagement.Domain.Queries;
using StudentManagement.Models.Models.DTOs;
using StudentManagement.Repository.Interfaces;

namespace StudentManagement.Domain.Handlers
{
    public class GetAllEnrollmentsQueryHandler : IRequestHandler<GetAllEnrollmentsQuery, List<EnrollmentModel>>
    {
        private readonly IEnrollmentRepository _repository;

        public GetAllEnrollmentsQueryHandler(IEnrollmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<EnrollmentModel>> Handle(GetAllEnrollmentsQuery request, CancellationToken cancellationToken)
        {
            var enrollments = await _repository.GetAllEnrollmentsAsync(cancellationToken);
            if (enrollments == null || !enrollments.Any())
            {
                throw new NotFoundException("No active enrollments found.");
            }
            return enrollments.Select(e => new EnrollmentModel
            {
                StudentName = e.StudentName,
                Course = e.Course,
                Duration = e.Duration,
                EnrollmentDate = e.EnrollmentDate,
                Grade = e.Grade
            }).ToList();
        }
    }
}
