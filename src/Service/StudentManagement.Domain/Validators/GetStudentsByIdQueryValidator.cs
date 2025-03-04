using FluentValidation;
using StudentManagement.Domain.Queries;

namespace StudentManagement.Domain.Validators
{
    public class GetStudentsByIdQueryValidator : AbstractValidator <GetStudentsByIdQuery>
    { 
        public GetStudentsByIdQueryValidator ()
        {
            RuleFor(x => x.StudentId).GreaterThan(0).WithMessage("Please enter valid Student ID");
        }
    }
}
