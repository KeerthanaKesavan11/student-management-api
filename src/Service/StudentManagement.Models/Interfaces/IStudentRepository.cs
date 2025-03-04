using StudentManagement.Models.Models;
using StudentManagement.Models.Models.DTOs;

namespace StudentManagement.Repository.Interfaces
{
    public interface IStudentRepository
    {
        Task<IEnumerable<StudentModel>> GetAllStudentsAsync(CancellationToken cancellationToken);
        Task<StudentModel?> GetStudentsByIdAsync(int studentId, CancellationToken cancellationToken);
        Task AddStudentAsync(Student student, CancellationToken cancellationToken);
        Task UpdateStudentAsync(Student student, CancellationToken cancellationToken);
        Task ActivateStudentAsync(Student student, CancellationToken cancellationToken);
        Task DeleteStudentAsync(int studentId, CancellationToken cancellationToken);
        Task<StudentModel?> GetStudentAsync(int studentId, CancellationToken cancellationToken);
        void Attach(Student student);
    }
}
