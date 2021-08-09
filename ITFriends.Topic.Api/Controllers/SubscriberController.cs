using System.Security.Claims;
using System.Threading.Tasks;
using ITFriends.Topic.Api.Responses;
using ITFriends.Topic.Core.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITFriends.Topic.Api.Controllers
{
    [Authorize]
    public class SubscriberController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SubscriberController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet]
        [Route("subscriptions")]
        public async Task<IActionResult> GetAppUserSubscriptions()
        {
            var result = await _mediator.Send(new GetAppUserSubscriptionsQuery {AppUserId = User.FindFirstValue("sub")});
        
            return Ok(new GetAppUserSubscriptionsResponse {Subscriptions = result.Subscriptions});
        }
    }
}