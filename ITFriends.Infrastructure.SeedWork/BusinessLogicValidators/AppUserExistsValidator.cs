using System;
using System.Threading.Tasks;
using ITFriends.Infrastructure.Data.Write.Contexts;
using ITFriends.Infrastructure.SeedWork.Errors;
using Microsoft.EntityFrameworkCore;

namespace ITFriends.Infrastructure.SeedWork.BusinessLogicValidators
{
    public class AppUserExistsValidator : IAppUserExistsValidator
    {
        private readonly AppDbContext _appDbContext;

        public AppUserExistsValidator(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task ValidateThatUserWithIdExistsAndThrow(string userId)
        {
            if (userId == null)
                throw new NullReferenceException();

            var exists = await _appDbContext.Users.AnyAsync(u => u.Id == userId);
            
            if (!exists)
                throw new BusinessLogicValidationException(BusinessLogicErrors.UserNotFoundError);
        }

        public async Task ValidateThatUserWithNameExistsAndThrow(string userName)
        {
            if (userName == null)
                throw new NullReferenceException();
            
            var exists = await _appDbContext.Users.AnyAsync(u => u.UserName == userName);
            
            if (!exists)
                throw new BusinessLogicValidationException(BusinessLogicErrors.UserNotFoundError);
        }
    }
}