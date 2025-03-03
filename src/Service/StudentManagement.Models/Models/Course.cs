using System;
using System.Collections.Generic;

namespace StudentManagement.Models.Models;

public partial class Course
{
    public int CourseId { get; set; }

    public string CourseName { get; set; } = null!;

    public string Duration { get; set; } = null!;

    public bool? Isactive { get; set; }

    public decimal Fees { get; set; }

    public virtual ICollection<CourseDetail> CourseDetails { get; set; } = new List<CourseDetail>();
}
