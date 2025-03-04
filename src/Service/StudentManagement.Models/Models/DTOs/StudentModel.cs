using System.Text.Json.Serialization;

namespace StudentManagement.Models.Models.DTOs
{
    public class StudentModel
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; } = null!;
        public DateOnly Dob { get; set; }
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Address { get; set; } = null!;
        [JsonIgnore]
        public bool IsActive { get; set; }
    }    
}
