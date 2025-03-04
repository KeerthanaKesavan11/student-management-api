using FluentValidation.TestHelper;
using StudentManagement.Domain.Command;
using StudentManagement.Domain.Validators;

namespace StudentManagement.UnitTests.Validators
{
    public class ActivateStudentCommandValidatorTests
    {
        private readonly ActivateStudentCommandValidator _validator;

        public ActivateStudentCommandValidatorTests()
        {
            _validator = new ActivateStudentCommandValidator();
        }

        [Fact]
        public void Validate_StudentIdIsZero_ShouldHaveValidationError()
        {
            var command = new ActivateStudentCommand { StudentId = 0 };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.StudentId);
        }

        [Fact]
        public void Validate_StudentIdIsNegative_ShouldHaveValidationError()
        {
            var command = new ActivateStudentCommand { StudentId = -1 };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.StudentId);
        }

        [Fact]
        public void Validate_StudentIdIsValid_ShouldNotHaveValidationError()
        {
            var command = new ActivateStudentCommand { StudentId = 1 };
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.StudentId);
        }
    }
}
