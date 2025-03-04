using Microsoft.EntityFrameworkCore;
using StudentManagement.Models.Models;
using StudentManagement.Models.Models.DTOs;
using StudentManagement.Repository.Interfaces;

namespace StudentManagement.Repository
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly StudentManagementContext _context;

        public EnrollmentRepository(StudentManagementContext context)
        {
            _context = context;
        }

        public async Task<List<EnrollmentModel>> GetAllEnrollmentsAsync(CancellationToken cancellationToken)
        {
            return await _context.CourseDetails
                .Where(cd => cd.IsActive == true)
                .Select(cd => new EnrollmentModel
                {
                    StudentName = cd.Student.StudentName,
                    Course = cd.Course.CourseName,
                    Duration = cd.Course.Duration,
                    EnrollmentDate = cd.EnrollmentDate,
                    Grade = cd.GradeNavigation.Grade1.ToString()
                })
                .ToListAsync(cancellationToken);
        }

        public async Task<List<EnrollmentModel>> GetEnrollmentByIdAsync(int studentId, CancellationToken cancellationToken)
        {
            return await _context.CourseDetails
                .Where(cd => cd.Student.StudentId == studentId && cd.IsActive == true)
                .Select(cd => new EnrollmentModel
                {
                    StudentName = cd.Student.StudentName,
                    Course = cd.Course.CourseName,
                    Duration = cd.Course.Duration,
                    EnrollmentDate = cd.EnrollmentDate,
                    Grade = cd.GradeNavigation.Grade1.ToString()
                })
                .ToListAsync(cancellationToken);
        }
    }
}
