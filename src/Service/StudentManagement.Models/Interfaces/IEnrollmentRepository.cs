using StudentManagement.Models.Models.DTOs;

namespace StudentManagement.Repository.Interfaces
{
    public interface IEnrollmentRepository
    {
        Task<List<EnrollmentModel>> GetAllEnrollmentsAsync(CancellationToken cancellationToken);
        Task<List<EnrollmentModel>> GetEnrollmentByIdAsync(int studentId, CancellationToken cancellationToken);
    }
}
