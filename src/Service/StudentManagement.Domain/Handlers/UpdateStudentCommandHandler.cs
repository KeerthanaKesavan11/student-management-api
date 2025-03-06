using MediatR;
using StudentManagement.Domain.Command;
using StudentManagement.Repository.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using StudentManagement.Models.Models;
using StudentManagement.Domain.Validators;
using StudentManagement.Domain.Extensions;

namespace StudentManagement.Domain.Handlers
{
    public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand>
    {
        private readonly IStudentRepository _repository;
        
        public UpdateStudentCommandHandler(IStudentRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateStudentCommandValidator();
            ValidationResult result = await validator.ValidateAsync(request, cancellationToken);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }

            var student = await _repository.GetStudentAsync(request.StudentId, cancellationToken);
            ValidationExtensions.ThrowIfInvalid(student == null, nameof(request.StudentId), "Student does not exist.");
            ValidationExtensions.ThrowIfInvalid(student?.IsActive == false, nameof(request.StudentId), "Student record is in Inactive status");

            if (student != null)
            {
                var updatedStudent = new Student
                {
                    StudentId = request.StudentId,
                    StudentName = request.StudentName,
                    Dob = request.Dob,
                    Email = request.Email,
                    PhoneNumber = request.PhoneNumber,
                    Address = request.Address,
                    IsActive = true
                };

                await _repository.UpdateStudentAsync(updatedStudent, cancellationToken);
            }
        }
    }
}
