using MediatR;
using StudentManagement.Models.Models.DTOs;

namespace StudentManagement.Domain.Queries
{
    public class GetAllEnrollmentsQuery : IRequest<List<EnrollmentModel>> { }

    public class GetEnrollmentByIdQuery : IRequest<List<EnrollmentModel>>
    {
        public int StudentId { get; set; }
    }

}
