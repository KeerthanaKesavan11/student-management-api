using MediatR;
using StudentManagement.Models.Models.DTOs;

namespace StudentManagement.Domain.Queries
{
    public class GetStudentsQuery : IRequest<IEnumerable<StudentModel>>
    {

    }
    public class GetStudentsByIdQuery : IRequest<StudentModel>
    {
        public int StudentId { get; set; }
    }
}
