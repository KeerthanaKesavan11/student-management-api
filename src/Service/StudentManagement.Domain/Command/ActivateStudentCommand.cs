using MediatR;

namespace StudentManagement.Domain.Command
{
    public class ActivateStudentCommand : IRequest<bool>
    {
        public int StudentId { get; set; }
    }
}
