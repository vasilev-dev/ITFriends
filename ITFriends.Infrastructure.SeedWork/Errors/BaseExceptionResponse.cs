using System.Collections.Generic;

namespace ITFriends.Infrastructure.SeedWork.Errors
{
    public class BaseExceptionResponse<T> where T : IError
    {
        public List<T> Errors { get; }
        public string GlobalStatusCode { get; }
        public string GlobalMessage { get; }

        public BaseExceptionResponse(List<T> errors, string globalMessage = null)
        {
            GlobalStatusCode = typeof(T).Name;
            Errors = errors;
            GlobalMessage = globalMessage;
        }
    }
    
    public class BaseExceptionResponse
    {
        public string GlobalStatusCode { get; }
        public string GlobalMessage { get; }

        public BaseExceptionResponse(string globalStatusCode, string globalMessage = null)
        {
            GlobalStatusCode = globalStatusCode;
            GlobalMessage = globalMessage;
        }
    }
}