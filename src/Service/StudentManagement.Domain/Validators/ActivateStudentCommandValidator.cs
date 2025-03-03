using FluentValidation;
using StudentManagement.Domain.Command;

namespace StudentManagement.Domain.Validators
{
    public class ActivateStudentCommandValidator : AbstractValidator<ActivateStudentCommand>
    {
        public ActivateStudentCommandValidator()
        {
            RuleFor(x => x.StudentId)
                .GreaterThan(0).WithMessage("Please enter a valid Student ID.");
        }
    }
}