using FluentValidation;
using FluentValidation.Results;

namespace StudentManagement.Domain.Extensions
{
    public static class ValidationExtensions
    {
        public static void ThrowIfInvalid(bool condition, string propertyName, string errorMessage)
        {
            if (condition)
            {
                throw new ValidationException(new List<ValidationFailure>
                {
                    new ValidationFailure(propertyName, errorMessage)
                });
            }
        }
    }
}
