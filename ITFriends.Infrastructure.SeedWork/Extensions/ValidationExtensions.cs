using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using ITFriends.Infrastructure.SeedWork.Errors;

namespace ITFriends.Infrastructure.SeedWork.Extensions
{
    public static class ValidationExtensions
    {
        public static List<ValidationError> ValidationErrors(this ValidationException exception)
        {
            var validationError = new List<ValidationError>();

            if (exception.Errors != null)
            {
                validationError = exception.Errors.Select(failure => 
                    new ValidationError(failure.ErrorCode, failure.ErrorMessage, failure.PropertyName)).ToList();
            }

            return validationError;
        }
        
    }
}