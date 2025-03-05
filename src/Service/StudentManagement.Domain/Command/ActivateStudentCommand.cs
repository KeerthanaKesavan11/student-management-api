using MediatR;

namespace StudentManagement.Domain.Command
{
    public class ActivateStudentCommand : IRequest<(bool Success, string? ErrorMessage)>
    {
        public int StudentId { get; set; }
    }
}
