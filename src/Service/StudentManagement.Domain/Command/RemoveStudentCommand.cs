using MediatR;

namespace StudentManagement.Domain.Command
{
    public class RemoveStudentCommand : IRequest<(bool Success, string? ErrorMessage)>
    {
        public int StudentId { get; set; }
    }
}
