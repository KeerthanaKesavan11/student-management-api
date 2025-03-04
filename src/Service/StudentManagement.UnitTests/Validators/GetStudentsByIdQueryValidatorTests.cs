using FluentValidation.TestHelper;
using StudentManagement.Domain.Queries;
using StudentManagement.Domain.Validators;

namespace StudentManagement.UnitTests.Validators
{
    public class GetStudentsByIdQueryValidatorTests
    {
        private readonly GetStudentsByIdQueryValidator _validator;

        public GetStudentsByIdQueryValidatorTests()
        {
            _validator = new GetStudentsByIdQueryValidator();
        }

        [Fact]
        public void Validate_StudentIdIsZero_ShouldHaveValidationError()
        {
            var query = new GetStudentsByIdQuery { StudentId = 0 };
            var result = _validator.TestValidate(query);
            result.ShouldHaveValidationErrorFor(x => x.StudentId);
        }

        [Fact]
        public void Validate_StudentIdIsNegative_ShouldHaveValidationError()
        {
            var query = new GetStudentsByIdQuery { StudentId = -1 };
            var result = _validator.TestValidate(query);
            result.ShouldHaveValidationErrorFor(x => x.StudentId);
        }

        [Fact]
        public void Validate_StudentIdIsValid_ShouldNotHaveValidationError()
        {
            var query = new GetStudentsByIdQuery { StudentId = 1 };
            var result = _validator.TestValidate(query);
            result.ShouldNotHaveValidationErrorFor(x => x.StudentId);
        }
    }
}
