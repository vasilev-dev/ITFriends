using System;
using ITFriends.Identity.Core.Dto;
using ITFriends.Infrastructure.Domain.Common;
using MediatR;

namespace ITFriends.Identity.Core.Commands
{
    public class RegisterUserCommand : IRequest<RegisterUserDto>
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Role { get; set; }
    }
}