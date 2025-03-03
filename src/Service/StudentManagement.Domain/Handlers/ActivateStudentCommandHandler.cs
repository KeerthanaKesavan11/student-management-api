using MediatR;
using StudentManagement.Domain.Command;
using StudentManagement.Repository.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using StudentManagement.Models.Models.DTOs;
using StudentManagement.Models.Models;

namespace StudentManagement.Domain.Handlers
{
    public class ActivateStudentCommandHandler : IRequestHandler<ActivateStudentCommand, bool>
    {
        private readonly IStudentRepository _repository;
        private readonly IValidator<ActivateStudentCommand> _validator;

        public ActivateStudentCommandHandler(IStudentRepository repository, IValidator<ActivateStudentCommand> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<bool> Handle(ActivateStudentCommand request, CancellationToken cancellationToken)
        {
            ValidationResult result = await _validator.ValidateAsync(request, cancellationToken);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }

            var student = await _repository.GetStudentAsync(request.StudentId, cancellationToken);
            if (student == null)
            {
                throw new ValidationException("Student does not exist.");
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
            return true;
        }
    }
}
