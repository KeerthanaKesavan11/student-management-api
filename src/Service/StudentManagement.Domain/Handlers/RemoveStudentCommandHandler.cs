using MediatR;
using StudentManagement.Domain.Command;
using StudentManagement.Repository.Interfaces;
using FluentValidation;
using FluentValidation.Results;

namespace StudentManagement.Domain.Handlers
{
    public class RemoveStudentCommandHandler : IRequestHandler<RemoveStudentCommand, bool>
    {
        private readonly IStudentRepository _repository;
        private readonly IValidator<RemoveStudentCommand> _validator;

        public RemoveStudentCommandHandler(IStudentRepository repository, IValidator<RemoveStudentCommand> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<bool> Handle(RemoveStudentCommand request, CancellationToken cancellationToken)
        {
            ValidationResult result = await _validator.ValidateAsync(request, cancellationToken);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }

            var student = await _repository.GetStudentsByIdAsync(request.StudentId, cancellationToken);
            if (student == null)
            {
                throw new ValidationException("Student does not exist.");
            }

            student.IsActive = false;
            await _repository.DeleteStudentAsync(request.StudentId, cancellationToken);
            return true;
        }
    }
}
