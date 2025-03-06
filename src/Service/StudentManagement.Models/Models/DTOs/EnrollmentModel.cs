namespace StudentManagement.Models.Models.DTOs
{
    public class EnrollmentModel
    {
        public required string StudentName { get; set; }
        public required string Course { get; set; }
        public required string Duration { get; set; }
        public DateOnly EnrollmentDate { get; set; }
        public required string Grade { get; set; }
       
    }

}
