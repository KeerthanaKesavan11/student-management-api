namespace StudentManagement.API.Models;

public partial class Student
{
    public int StudentId { get; set; }

    public string StudentName { get; set; } = null!;

    public DateOnly Dob { get; set; }

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Address { get; set; } = null!;

    public bool IsActive { get; set; }

    public virtual ICollection<CourseDetail> CourseDetails { get; set; } = new List<CourseDetail>();
}
