namespace StudentManagement.Models.Models;

public partial class CourseDetail
{
    public int EnrollmentId { get; set; }

    public int StudentId { get; set; }

    public int CourseId { get; set; }

    public DateOnly EnrollmentDate { get; set; }

    public char Grade { get; set; }

    public bool? Isactive { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual ICollection<FeesDetail> FeesDetails { get; set; } = new List<FeesDetail>();

    public virtual Grade GradeNavigation { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
