using MediatR;

namespace StudentManagement.Domain.Command
{
    public class RemoveStudentCommand : IRequest<bool>
    {
        public int StudentId { get; set; }
    }
}

