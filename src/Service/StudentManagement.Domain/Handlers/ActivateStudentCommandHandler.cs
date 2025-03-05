using MediatR;
using StudentManagement.Domain.Command;
using StudentManagement.Repository.Interfaces;
using StudentManagement.Models.Models;
using StudentManagement.Domain.Validators;
using FluentValidation;

namespace StudentManagement.Domain.Handlers
{
    public class ActivateStudentCommandHandler : IRequestHandler<ActivateStudentCommand, (bool Success, string? ErrorMessage)>
    {
        private readonly IStudentRepository _repository;

        public ActivateStudentCommandHandler(IStudentRepository repository, IValidator<ActivateStudentCommand> @object)
        {
            _repository = repository;
        }

        public async Task<(bool Success, string? ErrorMessage)> Handle(ActivateStudentCommand request, CancellationToken cancellationToken)
        {
            var validator = new ActivateStudentCommandValidator();
            var result = await validator.ValidateAsync(request, cancellationToken);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }


            var student = await _repository.GetStudentAsync(request.StudentId, cancellationToken);
            if (student == null)
            {
                return (false, "Student does not exist.");
            }
            if (student.IsActive == true)
            {
                return (false, "Student record is already in active status");
            }

            var updatedStudent = new Student
            {
                StudentId = student.StudentId,
                StudentName = student.StudentName,
                Dob = student.Dob,
                Email = student.Email,
                PhoneNumber = student.PhoneNumber,
                Address = student.Address,
                IsActive = true
            };

            await _repository.ActivateStudentAsync(updatedStudent, cancellationToken);
            return (true, null);
        }
    }
}
