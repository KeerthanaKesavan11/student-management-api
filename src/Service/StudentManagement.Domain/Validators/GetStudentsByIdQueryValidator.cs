using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
