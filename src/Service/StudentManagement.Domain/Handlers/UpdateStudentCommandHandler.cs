using MediatR;
using StudentManagement.Domain.Command;
using StudentManagement.Repository.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using StudentManagement.Models.Models.DTOs;
using StudentManagement.Models.Models;
using StudentManagement.Domain.Validators;

namespace StudentManagement.Domain.Handlers
{
    public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand, (bool Success, string? ErrorMessage, StudentModel? UpdatedStudent)>
    {
        private readonly IStudentRepository _repository;
        private readonly IValidator<UpdateStudentCommand> _validator;

        public UpdateStudentCommandHandler(IStudentRepository repository, IValidator<UpdateStudentCommand> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<(bool Success, string? ErrorMessage, StudentModel? UpdatedStudent)> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateStudentCommandValidator();
            ValidationResult result = await _validator.ValidateAsync(request, cancellationToken);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }


            var student = await _repository.GetStudentAsync(request.StudentId, cancellationToken);
            if (student == null)
            {
                return (false, "Student does not exist.", null);
            }
            if (student.IsActive == false)
            {
                return (false, "Student record is in Inactive status", null);
            }

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

            var updatedStudentModel = new StudentModel
            {
                StudentId = updatedStudent.StudentId,
                StudentName = updatedStudent.StudentName,
                Dob = updatedStudent.Dob,
                Email = updatedStudent.Email,
                PhoneNumber = updatedStudent.PhoneNumber,
                Address = updatedStudent.Address,
            };

            return (true, null, updatedStudentModel);
        }
    }
}
