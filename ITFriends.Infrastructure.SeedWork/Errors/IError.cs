namespace ITFriends.Infrastructure.SeedWork.Errors
{
    public interface IError
    {
        string ErrorCode { get; }
        string Message { get; }
    }
}