using MediatR;
using StudentManagement.Domain.Command;
using StudentManagement.Repository.Interfaces;
using StudentManagement.Models.Models;
using StudentManagement.Domain.Validators;
using FluentValidation;
using StudentManagement.Domain.Extensions;

namespace StudentManagement.Domain.Handlers
{
    public class ActivateStudentCommandHandler : IRequestHandler<ActivateStudentCommand>
    {
        private readonly IStudentRepository _repository;

        public ActivateStudentCommandHandler(IStudentRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(ActivateStudentCommand request, CancellationToken cancellationToken)
        {
            var validator = new ActivateStudentCommandValidator();
            var result = await validator.ValidateAsync(request, cancellationToken);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }

            var student = await _repository.GetStudentAsync(request.StudentId, cancellationToken);
            ValidationExtensions.ThrowIfInvalid(student == null, nameof(request.StudentId), "Student does not exist.");
            ValidationExtensions.ThrowIfInvalid(student?.IsActive == true, nameof(request.StudentId), "Student record is already in active status");

            if (student != null)
            {
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
            }
        }
    }
}
