using System.Threading.Tasks;

namespace ITFriends.Infrastructure.SeedWork.BusinessLogicValidators
{
    public interface IBusinessLogicValidator<in T>
    {
        Task ValidateAndThrow(T instance);
    }
}