using FluentValidation;
using StudentManagement.Domain.Command;

namespace StudentManagement.Domain.Validators
{
    public class CreateStudentCommandValidator : AbstractValidator<CreateStudentCommand>
    {
        public CreateStudentCommandValidator()
        {
            RuleFor(x => x.StudentName)
                .NotEmpty().WithMessage("Student name is required.");               

            RuleFor(x => x.Dob)
                .NotEmpty().WithMessage("Date of birth is required.")
                .Must(BeAValidDate).WithMessage("Date of birth must be a valid date.")
                .Must(BeInThePast).WithMessage("Date of birth cannot be in the future.");

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Valid email is required")
                .NotEmpty().WithMessage("Email is required.");
                                   
            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Must(BeAValidPhoneNumber).WithMessage("Phone number is required.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required.")
                .Must(BeAValidAddress).WithMessage("Address is required.");
        }

        private bool BeAValidDate(DateOnly date)
        {
            return date != default;
        }

        private bool BeInThePast(DateOnly date)
        {
            return date <= DateOnly.FromDateTime(DateTime.Now);
        }

        private bool BeAValidPhoneNumber(string phoneNumber)
        {
            return !string.Equals(phoneNumber, "string", StringComparison.OrdinalIgnoreCase);
        }

        private bool BeAValidAddress(string address)
        {
            return !string.Equals(address, "string", StringComparison.OrdinalIgnoreCase);
        }

    }
}
