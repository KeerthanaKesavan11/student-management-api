using System;
using System.Collections.Generic;

namespace StudentManagement.API.Models;

public partial class FeesDetail
{
    public int FeesDetailId { get; set; }

    public int EnrollmentId { get; set; }

    public int AmountPaid { get; set; }

    public string PaymentStatus { get; set; } = null!;

    public bool? Isactive { get; set; }

    public virtual CourseDetail Enrollment { get; set; } = null!;
}
