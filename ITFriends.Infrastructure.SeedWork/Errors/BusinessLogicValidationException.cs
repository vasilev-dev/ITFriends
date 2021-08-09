using System;
using System.Collections.Generic;

namespace ITFriends.Infrastructure.SeedWork.Errors
{
    public class BusinessLogicValidationException : Exception
    {
        public List<BusinessLogicError> Errors { get; }
        public string GlobalErrorMessage { get; }

        public BusinessLogicValidationException(BusinessLogicError error, string message = null) : base(message)
        {
            error.Message = message;
            Errors = new List<BusinessLogicError> {error};
        }
        
        public BusinessLogicValidationException(List<BusinessLogicError> error, string globalErrorMessage = null) : base(globalErrorMessage)
        {
            GlobalErrorMessage = globalErrorMessage;
            Errors = error;
        }
    }
}