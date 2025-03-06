using MediatR;

namespace StudentManagement.Domain.Command
{
    public class RemoveStudentCommand : IRequest
    {
        public int StudentId { get; set; }
    }
}
