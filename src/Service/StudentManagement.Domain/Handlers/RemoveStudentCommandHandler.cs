using MediatR;
using StudentManagement.Domain.Command;
using StudentManagement.Repository.Interfaces;
using FluentValidation;
using StudentManagement.Domain.Validators;
using StudentManagement.Models.Models;
using StudentManagement.Domain.Extensions;

namespace StudentManagement.Domain.Handlers
{
    public class RemoveStudentCommandHandler : IRequestHandler<RemoveStudentCommand>
    {
        private readonly IStudentRepository _repository;

        public RemoveStudentCommandHandler(IStudentRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(RemoveStudentCommand request, CancellationToken cancellationToken)
        {
            var validator = new RemoveStudentCommandValidator();
            var result = await validator.ValidateAsync(request, cancellationToken);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }

            var student = await _repository.GetStudentAsync(request.StudentId, cancellationToken);
            ValidationExtensions.ThrowIfInvalid(student == null, nameof(request.StudentId), "Student does not exist.");
            ValidationExtensions.ThrowIfInvalid(student?.IsActive == false, nameof(request.StudentId), "Student record is already in InActive status");

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
                    IsActive = false
                };

                await _repository.DeleteStudentAsync(updatedStudent, cancellationToken);
            }
        }
    }
}
