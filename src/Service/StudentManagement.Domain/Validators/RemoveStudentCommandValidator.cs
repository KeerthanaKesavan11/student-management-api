using FluentValidation;
using StudentManagement.Domain.Command;

namespace StudentManagement.Domain.Validators
{
    public class RemoveStudentCommandValidator : AbstractValidator<RemoveStudentCommand>
    {
        public RemoveStudentCommandValidator()
        {
            RuleFor(x => x.StudentId)
                .GreaterThan(0).WithMessage("Please enter a valid Student ID.");
        }
    }
}

