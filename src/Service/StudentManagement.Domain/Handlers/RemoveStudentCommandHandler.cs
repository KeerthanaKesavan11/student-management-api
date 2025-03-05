using MediatR;
using StudentManagement.Domain.Command;
using StudentManagement.Repository.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using StudentManagement.Domain.Validators;
using StudentManagement.Models.Models;

namespace StudentManagement.Domain.Handlers
{
    public class RemoveStudentCommandHandler : IRequestHandler<RemoveStudentCommand, (bool Success, string? ErrorMessage)>
    {
        private readonly IStudentRepository _repository;
        public RemoveStudentCommandHandler(IStudentRepository repository)
        {
            _repository = repository;
        }

        public async Task<(bool Success, string? ErrorMessage)> Handle(RemoveStudentCommand request, CancellationToken cancellationToken)
        {
            var validator = new RemoveStudentCommandValidator();
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
            if (student.IsActive == false)
            {
                return (false, "Student record is already in InActive status");
            }

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
            return (true,null);
        }
    }
}
