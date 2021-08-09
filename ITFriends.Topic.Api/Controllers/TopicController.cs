using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using ITFriends.Infrastructure.SeedWork.BaseResponses;
using ITFriends.Topic.Api.Requests;
using ITFriends.Topic.Api.Responses;
using ITFriends.Topic.Core.Commands;
using ITFriends.Topic.Core.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITFriends.Topic.Api.Controllers
{
    [Authorize]
    [ApiController]
    public class TopicController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        
        public TopicController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }
        
        [HttpPost]
        [Route("topic/create")]
        [Authorize(Roles = "admin,moderator")]
        public async Task<IActionResult> CreateTopic([FromBody] CreateTopicRequest request)
        {
            var command = _mapper.Map<CreateTopicCommand>(request);
            
            var result = await _mediator.Send(command);

            return Ok(new LongCreateResourceResponse(result.CreatedResourceId));
        }

        [HttpGet]
        [Route("topics")]
        public async Task<IActionResult> GetRootTopics()
        {
            var query = new GetRootTopicsQuery();

            var result = await _mediator.Send(query);

            return Ok(new GetRootTopicsResponse {Topics = result.RootTopics});
        }
        
        [HttpGet]
        [Route("topic/{topicId:int}")]
        public async Task<IActionResult> GetTopic([FromRoute] int topicId)
        {
            var query = new GetTopicQuery {TopicId = topicId};

            var result = await _mediator.Send(query);

            return Ok(new GetTopicResponse {Topic = result.Topic});
        }

        [HttpPut]
        [Route("topic/{topicId:int}/edit")]
        [Authorize(Roles = "admin,moderator")]
        public async Task<IActionResult> EditTopic([FromRoute] int topicId, [FromBody] EditTopicRequest request)
        {
            var command = new EditTopicCommand {TopicId = topicId, Title = request.Title};

            await _mediator.Send(command);

            return Ok();
        }

        [HttpDelete]
        [Route("topic/{topicId:int}/delete")]
        [Authorize(Roles = "admin,moderator")]
        public async Task<IActionResult> DeleteTopic([FromRoute] int topicId)
        {
            var command = new DeleteTopicCommand {TopicId = topicId};

            await _mediator.Send(command);

            return Ok();
        }

        [HttpPost]
        [Route("topic/{topicId:int}/subscribe")]
        public async Task<IActionResult> Subscribe([FromRoute] int topicId)
        {
            var command = new SubscribeToTopicCommand {
                TopicId = topicId, 
                AppUserId = User.FindFirstValue("sub")
            };

            await _mediator.Send(command);

            return Ok();
        }

        [HttpDelete]
        [Route("topic/{topicId:int}/unsubscribe")]
        public async Task<IActionResult> Unsubscribe([FromRoute] int topicId)
        {
            var command = new UnsubscribeFromTopicCommand {
                TopicId = topicId, 
                AppUserId = User.FindFirstValue("sub")
            };

            await _mediator.Send(command);

            return Ok();
        }
        
        [HttpGet]
        [Route("topic/{topicId:int}/subscribers")]
        public async Task<IActionResult> GetTopicSubscribers([FromRoute] int topicId)
        {
            var result = await _mediator.Send(new GetTopicSubscribersQuery {TopicId = topicId});
        
            return Ok(new GetTopicSubscribersResponse {SubscriberIds = result.AppUserIds});
        }
    }
}