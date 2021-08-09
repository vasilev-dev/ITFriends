using System;
using System.Linq;
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
    [Route("message")]
    public class TopicMessageController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public TopicMessageController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        
        [HttpGet]
        [Route("{topicMessageId:int}")]
        public async Task<IActionResult> GetTopicMessage([FromRoute] int topicMessageId)
        {
            var query = new GetTopicMessageQuery {TopicMessageId = topicMessageId};

            var topicMessage = await _mediator.Send(query);

            return Ok(new GetTopicMessageResponse {TopicMessage = topicMessage});
        }
        
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateTopicMessage([FromBody] CreateTopicMessageRequest request)
        {
            var command = _mapper.Map<CreateTopicMessageCommand>(request);
            command.CreatorAppUserId = User.FindFirstValue("sub");

            var result = await _mediator.Send(command);

            return Ok(new LongCreateResourceResponse(result.CreatedResourceId));
        }
        
        [HttpPut]
        [Route("{topicMessageId:int}/title/edit")]
        public async Task<IActionResult> EditTopicMessageTitle([FromRoute] int topicMessageId, [FromBody] EditTopicMessageTitleRequest request)
        {
            var command = new EditTopicMessageTitleCommand
            {
                TopicMessageId = topicMessageId, 
                Title = request.Title,
                EditorAppUserId = User.FindFirstValue("sub")
            };

            await _mediator.Send(command);

            return Ok();
        }
        
        [HttpPut]
        [Route("{topicMessageId:int}/html/edit")]
        public async Task<IActionResult> EditTopicMessageHtml([FromRoute] int topicMessageId, [FromBody] EditTopicMessageHtmlRequest request)
        {
            var command = new EditTopicMessageHtmlCommand
            {
                TopicMessageId = topicMessageId, 
                Html = request.Html, 
                EditorAppUserId = User.FindFirstValue("sub")
            };

            await _mediator.Send(command);

            return Ok();
        }

        
        [HttpDelete]
        [Route("{topicMessageId:int}/delete")]
        public async Task<IActionResult> DeleteTopicMessage([FromRoute] int topicMessageId)
        {
            var command = new DeleteTopicMessageCommand
            {
                TopicMessageId = topicMessageId,
                EditorAppUserId =  User.FindFirstValue("sub")
            };

            await _mediator.Send(command);

            return Ok();
        }
    }
}