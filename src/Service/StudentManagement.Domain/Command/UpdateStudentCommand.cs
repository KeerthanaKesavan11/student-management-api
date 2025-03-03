using MediatR;
using StudentManagement.Models.Models.DTOs;
using System.Text.Json.Serialization;

namespace StudentManagement.Domain.Command
{
    public class UpdateStudentCommand : IRequest<StudentModel>
    {
       // [JsonIgnore]
        public int StudentId { get; set; }
        public string StudentName { get; set; } = null!;
        public DateOnly Dob { get; set; }
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Address { get; set; } = null!;
    }
}
