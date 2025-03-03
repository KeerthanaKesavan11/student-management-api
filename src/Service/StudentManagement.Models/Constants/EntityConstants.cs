using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Constants
{
    public static class EntityConstants
    {
        public static class Course
        {
            public const string TableName = "course";
            public const string PrimaryKey = "course_pkey";
            public const string CourseId = "course_id";
            public const string CourseName = "course_name";
            public const string Duration = "duration";
            public const string Fees = "fees";
            public const string IsActive = "isactive";
        }

        public static class CourseDetail
        {
            public const string TableName = "course_detail";
            public const string PrimaryKey = "course_detail_pkey";
            public const string EnrollmentId = "enrollment_id";
            public const string CourseId = "course_id";
            public const string EnrollmentDate = "enrollment_date";
            public const string Grade = "grade";
            public const string IsActive = "isactive";
            public const string StudentId = "student_id";
            public const string CourseForeignKey = "course_detail_course_id_fkey";
            public const string GradeForeignKey = "course_detail_grade_fkey";
            public const string StudentForeignKey = "course_detail_student_id_fkey";
        }
        public static class FeesDetail
        {
            public const string TableName = "fees_detail";
            public const string PrimaryKey = "fees_detail_pkey";
            public const string FeesDetailId = "fees_detail_id";
            public const string AmountPaid = "amount_paid";
            public const string EnrollmentId = "enrollment_id";
            public const string IsActive = "isactive";
            public const string PaymentStatus = "payment_status";
            public const string EnrollmentForeignKey = "fees_detail_enrollment_id_fkey";
        }
        public static class Grade
        {
            public const string TableName = "grade";
            public const string PrimaryKey = "grade_pkey";
            public const string GradeId = "grade_id";
            public const string Grade1 = "grade";
            public const string ScoreRange = "score_range";
            public const string GradeKey = "grade_grade_key";
        }
        public static class Student
        {
            public const string TableName = "student";
            public const string PrimaryKey = "student_pkey";
            public const string StudentId = "student_id";
            public const string Address = "address";
            public const string Dob = "dob";
            public const string Email = "email";
            public const string IsActive = "isactive";
            public const string PhoneNumber = "phone_number";
            public const string StudentName = "student_name";
        }
    }
}
