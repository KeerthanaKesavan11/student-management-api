using MediatR;
using StudentManagement.Models.Models.DTOs;

namespace StudentManagement.Domain.Command
{
    public class UpdateStudentCommand : IRequest<(bool Success, string? ErrorMessage ,StudentModel? UpdatedStudent)>
    {
       public int StudentId { get; set; }
        public string StudentName { get; set; } = null!;
        public DateOnly Dob { get; set; }
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Address { get; set; } = null!;
    }
}
