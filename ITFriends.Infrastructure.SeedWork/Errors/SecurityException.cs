using System;

namespace ITFriends.Infrastructure.SeedWork.Errors
{
    public class SecurityException : BusinessLogicValidationException
    {
        public string AppUserId { get; }
        public string ResourceName { get; }
        public int ResourceId { get; }
        public override string Message { get; }

        public SecurityException(string appUserId, string resourceName, int resourceId, string message = null) :
            base(BusinessLogicErrors.UserHasNotPermissionsError)
        {
            AppUserId = appUserId;
            ResourceName = resourceName;
            ResourceId = resourceId;
            Message = message;
        }
    }
}