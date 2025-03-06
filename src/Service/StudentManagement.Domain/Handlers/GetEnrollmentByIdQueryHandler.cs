using FluentValidation;
using MediatR;
using StudentManagement.API.Exception;
using StudentManagement.Domain.Queries;
using StudentManagement.Domain.Validators;
using StudentManagement.Models.Models.DTOs;
using StudentManagement.Repository.Interfaces;

namespace StudentManagement.Domain.Handlers
{
    public class GetEnrollmentByIdQueryHandler : IRequestHandler<GetEnrollmentByIdQuery, List<EnrollmentModel>>
    {
        private readonly IEnrollmentRepository _repository;
        private readonly IStudentRepository _studentRepository;

        public GetEnrollmentByIdQueryHandler(IEnrollmentRepository repository, IStudentRepository studentRepository)
        {
            _repository = repository;
            _studentRepository = studentRepository;
        }

        public async Task<List<EnrollmentModel>> Handle(GetEnrollmentByIdQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetEnrollmentByIdQueryValidator();
            var results = await validator.ValidateAsync(request, cancellationToken);
            if (!results.IsValid)
            {
                throw new ValidationException(results.Errors);
            }
            var student = await _studentRepository.GetStudentsByIdAsync(request.StudentId, cancellationToken);
            if (student == null )
            {
                throw new NotFoundException("Student not found.");
            }

            var enrollments = await _repository.GetEnrollmentByIdAsync(request.StudentId, cancellationToken);
            if (enrollments == null || !enrollments.Any())
            {
                throw new NotFoundException("No active enrollments found for the given student ID.");
            }

            return enrollments.Select(e => new EnrollmentModel
            {
                StudentName = e.StudentName,
                Course = e.Course,
                Duration = e.Duration,
                EnrollmentDate = e.EnrollmentDate,
                Grade = e.Grade
            }).ToList();
        }
    }
}
