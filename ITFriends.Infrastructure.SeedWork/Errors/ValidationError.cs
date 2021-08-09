namespace ITFriends.Infrastructure.SeedWork.Errors
{
    public class ValidationError : IError
    {
        public string ErrorCode { get; }
        public string Message { get; }
        public string PropertyName { get; }
        
        public ValidationError(string statusCode, string message, string propertyName)
        {
            ErrorCode = statusCode;
            Message = message;
            PropertyName = propertyName;
        }
    }
}