using System.Threading.Tasks;
using AutoMapper;
using ITFriends.Identity.Api.Requests;
using ITFriends.Identity.Api.Responses;
using ITFriends.Identity.Core.Commands;
using ITFriends.Identity.Core.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITFriends.Identity.Api.Controllers
{
    [Authorize(Roles = "admin")]
    [ApiController]
    [Route("admin/users")]
    public class AdminController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AdminController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var query = new GetAllUsersQuery();

            var result = await _mediator.Send(query);

            return Ok(new GetAllUsersResponse {Users = result.Users});
        }
        
        [HttpPost("role/change")]
        public async Task<IActionResult> ChangeRole([FromBody] ChangeRoleRequest request)
        {
            var command = _mapper.Map<ChangeRoleCommand>(request);

            await _mediator.Send(command);

            return Ok();
        }
    }
}