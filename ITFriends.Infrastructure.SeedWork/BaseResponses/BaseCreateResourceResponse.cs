using System;

namespace ITFriends.Infrastructure.SeedWork.BaseResponses
{
    public abstract class BaseCreateResourceResponse<T>
    {
        public T CreatedId { get; set; }

        public BaseCreateResourceResponse(T createdId)
        {
            CreatedId = createdId;
        }
    }
    
    public class LongCreateResourceResponse : BaseCreateResourceResponse<long>
    {
        public LongCreateResourceResponse(long createdId) : base(createdId)
        {
        }
    }
        
    public class GuidCreateResourceResponse : BaseCreateResourceResponse<Guid>
    {
        public GuidCreateResourceResponse(Guid createdId) : base(createdId)
        {
        }
    }
}