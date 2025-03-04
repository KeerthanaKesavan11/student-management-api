using MediatR;
using StudentManagement.Domain.Command;
using StudentManagement.Repository.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using StudentManagement.Models.Models.DTOs;
using StudentManagement.Models.Models;

namespace StudentManagement.Domain.Handlers
{
    public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand, StudentModel>
    {
        private readonly IStudentRepository _repository;
        private readonly IValidator<UpdateStudentCommand> _validator;

        public UpdateStudentCommandHandler(IStudentRepository repository, IValidator<UpdateStudentCommand> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<StudentModel> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
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
                StudentId = request.StudentId,
                StudentName = request.StudentName,
                Dob = request.Dob,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Address = request.Address,
                IsActive = true
            };

            await _repository.UpdateStudentAsync(updatedStudent, cancellationToken);

            return new StudentModel
            {
                StudentId = student.StudentId,
                StudentName = student.StudentName,
                Dob = student.Dob,
                Email = student.Email,
                PhoneNumber = student.PhoneNumber,
                Address = student.Address,
      
            };
        }
    }
}
