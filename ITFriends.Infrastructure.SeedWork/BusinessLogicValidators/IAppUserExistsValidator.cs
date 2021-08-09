using System.Threading.Tasks;

namespace ITFriends.Infrastructure.SeedWork.BusinessLogicValidators
{
    public interface IAppUserExistsValidator
    {
        Task ValidateThatUserWithIdExistsAndThrow(string userId);
        Task ValidateThatUserWithNameExistsAndThrow(string userName);
    }
}