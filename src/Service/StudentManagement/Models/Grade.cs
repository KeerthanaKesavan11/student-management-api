namespace StudentManagement.API.Models;

public partial class Grade
{
    public int GradeId { get; set; }

    public char Grade1 { get; set; }

    public string ScoreRange { get; set; } = null!;

    public virtual ICollection<CourseDetail> CourseDetails { get; set; } = new List<CourseDetail>();
}
