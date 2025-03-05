using Microsoft.EntityFrameworkCore;
using StudentManagement.Models.Models;
using StudentManagement.Models.Models.DTOs;
using StudentManagement.Repository.Interfaces;

namespace StudentManagement.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly StudentManagementContext _context;
        public StudentRepository(StudentManagementContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StudentModel>> GetAllStudentsAsync(CancellationToken cancellationToken)
        {
            return await _context.Students
                .Where(student => student.IsActive == true)
                .Select(student => new StudentModel
                {
                    StudentId = student.StudentId,
                    StudentName = student.StudentName,
                    Dob = student.Dob,
                    Email = student.Email,
                    PhoneNumber = student.PhoneNumber,
                    Address = student.Address
                })
                .ToListAsync(cancellationToken);
        }

        public async Task<StudentModel?> GetStudentsByIdAsync(int studentId, CancellationToken cancellationToken)
        {
            var student = await _context.Students
                .Where(s => s.StudentId == studentId && s.IsActive == true)
                .FirstOrDefaultAsync(cancellationToken);

            if (student == null)
            {
                return null;
            }

            return new StudentModel
            {
                StudentId = student.StudentId,
                StudentName = student.StudentName,
                Dob = student.Dob,
                Email = student.Email,
                PhoneNumber = student.PhoneNumber,
                Address = student.Address
            };
        }

        public async Task<StudentModel?> GetStudentAsync(int studentId, CancellationToken cancellationToken)
        {
            var student = await _context.Students
                .AsNoTracking()
                .Where(s => s.StudentId == studentId)
                .FirstOrDefaultAsync(cancellationToken);

            if (student == null)
            {
                return null;
            }

            return new StudentModel
            {
                StudentId = student.StudentId,
                StudentName = student.StudentName,
                Dob = student.Dob,
                Email = student.Email,
                PhoneNumber = student.PhoneNumber,
                Address = student.Address,
                IsActive = student.IsActive ?? false
            };
        }

        public async Task AddStudentAsync(Student student, CancellationToken cancellationToken)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateStudentAsync(Student student, CancellationToken cancellationToken)
        {
            _context.Students.Update(student);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task ActivateStudentAsync(Student student, CancellationToken cancellationToken)
        {
            _context.Students.Update(student);
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task DeleteStudentAsync(Student student, CancellationToken cancellationToken)
        {
            _context.Students.Update(student);
            await _context.SaveChangesAsync(cancellationToken);
        }

    }


}
