using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using ITFriends.Identity.Api.Requests;
using ITFriends.Identity.Api.Responses;
using ITFriends.Identity.Core.Commands;
using ITFriends.Identity.Core.Queries;
using ITFriends.Infrastructure.SeedWork.BaseResponses;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITFriends.Identity.Api.Controllers
{
    [Authorize]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public IdentityController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
        {
            var command = _mapper.Map<RegisterUserCommand>(request);
            
            var result = await _mediator.Send(command);
        
            return Ok(new GuidCreateResourceResponse(result.AppUserId));
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var command = _mapper.Map<LoginCommand>(request);

            var result = await _mediator.Send(command);

            var response = _mapper.Map<TokenResponse>(result);
            
            if (response.HasError)
                return Unauthorized(response);

            return Ok(response);
        }
        
        [HttpDelete("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return Ok();
        }

        [HttpPost("token/refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            var command = _mapper.Map<RefreshTokenCommand>(request);

            var result = await _mediator.Send(command);
            
            var response = _mapper.Map<TokenResponse>(result);
            
            if (response.HasError)
                return Unauthorized(response);

            return Ok(response);
        }
        
        [HttpPut("password/change")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            var command = _mapper.Map<ChangePasswordCommand>(request);
            command.UserId = User.FindFirstValue("sub");

            await _mediator.Send(command);

            return Ok();
        }
        
        [HttpPut("email/confirm")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailRequest request)
        {
            var command = _mapper.Map<ConfirmEmailCommand>(request);

            await _mediator.Send(command);

            return Ok();
        }
        
        [HttpPost("password/forgot")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            var command = _mapper.Map<ForgotPasswordCommand>(request);

            var result = await _mediator.Send(command);

            var response = _mapper.Map<ForgotPasswordResponse>(result);

            return Ok(response);
        }

        [HttpPut("password/restore")]
        [AllowAnonymous]
        public async Task<IActionResult> RestorePassword([FromBody] RestorePasswordRequest request)
        {
            var command = _mapper.Map<RestorePasswordCommand>(request);

            await _mediator.Send(command);

            return Ok();
        }

        [HttpPut("telegram/bind")]
        [AllowAnonymous]
        public async Task<IActionResult> BingTelegramAccount([FromBody] ConfirmTelegramBindingRequest request)
        {
            var command = _mapper.Map<ConfirmTelegramBindingCommand>(request);

            await _mediator.Send(command);

            return Ok();
        }
        
        [HttpGet("user")]
        public async Task<IActionResult> GetUser()
        {
            var query = new GetUserQuery {AppUserId = User.FindFirstValue("sub")};

            var result = await _mediator.Send(query);

            return Ok(new GetUserResponse {User = result.User});
        }
    }
}