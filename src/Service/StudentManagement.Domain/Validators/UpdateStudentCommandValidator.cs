using FluentValidation;
using StudentManagement.Domain.Command;

namespace StudentManagement.Domain.Validators
{
    public class UpdateStudentCommandValidator : AbstractValidator<UpdateStudentCommand>
    {
        public UpdateStudentCommandValidator()
        {
            //RuleFor(x => x.StudentId)
            //    .GreaterThan(0).WithMessage("Please enter a valid Student ID.");

            RuleFor(x => x.StudentName)
                .NotEmpty().WithMessage("Student name is required.")
                .Must(BeAValidName).WithMessage("Student name is required.");

            RuleFor(x => x.Dob)
                .NotEmpty().WithMessage("Date of birth is required.")
                .Must(BeAValidDate).WithMessage("Date of birth must be a valid date.")
                .Must(BeInThePast).WithMessage("Date of birth cannot be in the future.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .Must(BeAValidEmail).WithMessage("Email is required.")
                .EmailAddress().WithMessage("Valid email is required.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Must(BeAValidPhoneNumber).WithMessage("Phone number is required.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required.")
                .Must(BeAValidAddress).WithMessage("Address is required.");
        }

        private bool BeAValidName(string name)
        {
            return !string.Equals(name, "string", StringComparison.OrdinalIgnoreCase);
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

        private bool BeAValidEmail(string email)
        {
            return !string.Equals(email, "string", StringComparison.OrdinalIgnoreCase);
        }
    }
}
