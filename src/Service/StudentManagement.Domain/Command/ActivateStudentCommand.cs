using MediatR;

namespace StudentManagement.Domain.Command
{
    public class ActivateStudentCommand : IRequest
    {
        public int StudentId { get; set; }
    }
}
