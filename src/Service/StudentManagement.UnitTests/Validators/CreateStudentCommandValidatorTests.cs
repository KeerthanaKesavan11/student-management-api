using System;
using FluentValidation.TestHelper;
using StudentManagement.Domain.Command;
using StudentManagement.Domain.Validators;
using Xunit;

namespace StudentManagement.UnitTests.Validators
{
    public class CreateStudentCommandValidatorTests
    {
        private readonly CreateStudentCommandValidator _validator;

        public CreateStudentCommandValidatorTests()
        {
            _validator = new CreateStudentCommandValidator();
        }

        [Fact]
        public void Validate_StudentNameIsEmpty_ShouldHaveValidationError()
        {
            var command = new CreateStudentCommand { StudentName = "" };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.StudentName);
        }

        [Fact]
        public void Validate_StudentNameIsInvalid_ShouldHaveValidationError()
        {
            var command = new CreateStudentCommand { StudentName = "string" };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.StudentName);
        }

        [Fact]
        public void Validate_DobIsEmpty_ShouldHaveValidationError()
        {
            var command = new CreateStudentCommand { Dob = default };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Dob);
        }

        [Fact]
        public void Validate_DobIsInTheFuture_ShouldHaveValidationError()
        {
            var command = new CreateStudentCommand { Dob = DateOnly.FromDateTime(DateTime.Now.AddDays(1)) };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Dob);
        }

        [Fact]
        public void Validate_EmailIsEmpty_ShouldHaveValidationError()
        {
            var command = new CreateStudentCommand { Email = "" };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public void Validate_EmailIsInvalid_ShouldHaveValidationError()
        {
            var command = new CreateStudentCommand { Email = "invalid-email" };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public void Validate_PhoneNumberIsEmpty_ShouldHaveValidationError()
        {
            var command = new CreateStudentCommand { PhoneNumber = "" };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.PhoneNumber);
        }

        [Fact]
        public void Validate_PhoneNumberIsInvalid_ShouldHaveValidationError()
        {
            var command = new CreateStudentCommand { PhoneNumber = "string" };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.PhoneNumber);
        }

        [Fact]
        public void Validate_AddressIsEmpty_ShouldHaveValidationError()
        {
            var command = new CreateStudentCommand { Address = "" };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Address);
        }

        [Fact]
        public void Validate_AddressIsInvalid_ShouldHaveValidationError()
        {
            var command = new CreateStudentCommand { Address = "string" };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Address);
        }

        [Fact]
        public void Validate_ValidCommand_ShouldNotHaveValidationError()
        {
            var command = new CreateStudentCommand
            {
                StudentName = "John Doe",
                Dob = new DateOnly(2000, 1, 1),
                Email = "john.doe@example.com",
                PhoneNumber = "1234567890",
                Address = "123 Main St"
            };
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}




