using FluentValidation;
using StudentManagement.Domain.Queries;

namespace StudentManagement.Domain.Validators
{
    public class GetEnrollmentByIdQueryValidator : AbstractValidator<GetEnrollmentByIdQuery>
    {
        public GetEnrollmentByIdQueryValidator()
        {
            RuleFor(x => x.StudentId).GreaterThan(0).WithMessage("Please enter valid Student ID");
        }
    }
}
