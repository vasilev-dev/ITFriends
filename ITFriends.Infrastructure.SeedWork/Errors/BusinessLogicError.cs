namespace ITFriends.Infrastructure.SeedWork.Errors
{
    public struct BusinessLogicErrors
    {
        public static readonly BusinessLogicError UserNotFoundError = new("UserNotFound", "User not found");
        public static readonly BusinessLogicError UserAlreadyRegisteredError = new("UserAlreadyRegistered", "User already registered");
        public static readonly BusinessLogicError CannotChangePasswordError = new("CannotChangePasswordError", "Wrong old password");
        public static readonly BusinessLogicError InvalidRestoreTokenError = new("InvalidRestoreToken", "Invalid restore token");
        public static readonly BusinessLogicError EmailAlreadyConfirmedError = new("EmailAlreadyConfirmed", "Email already confirmed");
        public static readonly BusinessLogicError WrongTokenForEmailConfirmationError = new("WrongTokenForEmailConfirmation", "Wrong token for email confirmation");
        public static readonly BusinessLogicError EmailNotConfirmedError = new("EmailNotConfirmed", "Email not confirmed");
        public static readonly BusinessLogicError ResourceNotFoundError = new("ResourceNotFound", "Resource not found");
        public static readonly BusinessLogicError UserHasNotPermissionsError = new("UserHasNotPermissions", "User has not permissions");
        public static readonly BusinessLogicError CannotDeleteTopicError = new("CannotDeleteTopic", "Cannot delete topic");
        public static readonly BusinessLogicError AlreadySubscribedToTopicError = new("AlreadySubscribedToTopic", "User already subscribed to this topic");
        public static readonly BusinessLogicError NotSubscribedToTopicError = new("NotSubscribedToTopic", "User not subscribed to this topic");
        public static readonly BusinessLogicError WrongUserNameOrPasswordError = new("WrongUserNameOrPassword", "Wrong username or password");
        public static readonly BusinessLogicError TelegramBindingAlreadyConfirmedError = new("TelegramBindingAlreadyConfirmed", "Telegram binding already confirmed");
        public static readonly BusinessLogicError CannotSetNotUserRoleError = new("CannotSetNotUserRoleError", "Cannot set not user role");
        public static readonly BusinessLogicError CannotSetAdminRoleError = new("CannotSetAdminRole", "Setting admin role is only available using database tools");
    }

    public class BusinessLogicError : IError
    {
        public string ErrorCode { get; }
        public string Description { get; }
        public string Message { get; set; }

        public BusinessLogicError(string errorCode, string description, string message = null)
        {
            ErrorCode = errorCode;
            Description = description;
            Message = message;
        }
    }
}