using System;
using System.Net;
using FluentValidation;
using ITFriends.Infrastructure.SeedWork.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ITFriends.Infrastructure.SeedWork.Errors
{
    public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order { get; } = int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            switch (context.Exception)
            {
                case BusinessLogicValidationException businessLogicException:
                    context.Result = new ObjectResult(new BaseExceptionResponse<BusinessLogicError>(businessLogicException.Errors, businessLogicException.GlobalErrorMessage))
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest
                    };
                    context.ExceptionHandled = true;
                    return;
                
                case ValidationException validationException:
                    context.Result = new ObjectResult(new BaseExceptionResponse<ValidationError>(validationException.ValidationErrors()))
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest
                    };
                    context.ExceptionHandled = true;
                    return;
                
                case { }:
                    context.Result = new ObjectResult(new BaseExceptionResponse(InternalError.ErrorCode, InternalError.Message))
                    {
                        StatusCode = (int)HttpStatusCode.InternalServerError
                    };
                    context.ExceptionHandled = true;
                    return;
            }
        }
    }
}