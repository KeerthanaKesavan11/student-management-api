using FluentValidation.TestHelper;
using StudentManagement.Domain.Command;
using StudentManagement.Domain.Validators;
using Xunit;

namespace StudentManagement.UnitTests.Validators
{
    public class RemoveStudentCommandValidatorTests
    {
        private readonly RemoveStudentCommandValidator _validator;

        public RemoveStudentCommandValidatorTests()
        {
            _validator = new RemoveStudentCommandValidator();
        }

        [Fact]
        public void Validate_StudentIdIsZero_ShouldHaveValidationError()
        {
            var command = new RemoveStudentCommand { StudentId = 0 };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.StudentId);
        }

        [Fact]
        public void Validate_StudentIdIsNegative_ShouldHaveValidationError()
        {
            var command = new RemoveStudentCommand { StudentId = -1 };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.StudentId);
        }

        [Fact]
        public void Validate_StudentIdIsValid_ShouldNotHaveValidationError()
        {
            var command = new RemoveStudentCommand { StudentId = 1 };
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.StudentId);
        }
    }
}


