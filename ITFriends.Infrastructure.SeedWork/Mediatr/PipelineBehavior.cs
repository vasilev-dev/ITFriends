using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using ITFriends.Infrastructure.SeedWork.BusinessLogicValidators;
using MediatR;

namespace ITFriends.Infrastructure.SeedWork.Mediatr
{
    public class PipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly IEnumerable<IBusinessLogicValidator<TRequest>> _securityValidators;

        public PipelineBehavior(
            IEnumerable<IValidator<TRequest>> validators,
            IEnumerable<IBusinessLogicValidator<TRequest>> securityValidators)
        {
            _validators = validators;
            _securityValidators = securityValidators;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            await ValidateRequestAndThrow(request, cancellationToken);
            await ValidateBusinessLogicAndThrow(request, cancellationToken);

            return await next();
        }

        private Task ValidateRequestAndThrow(TRequest request, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);
            
            var failures = _validators
                .Select(async x => await x.ValidateAsync(context, cancellationToken))
                .Select(t => t.Result)
                .SelectMany(x => x.Errors)
                .Where(x => x != null)
                .ToList();
            
            if (failures.Any())
                throw new ValidationException(failures);

            return Task.CompletedTask;
        }

        private async Task ValidateBusinessLogicAndThrow(TRequest request, CancellationToken cancellationToken)
        {
            foreach (var securityValidator in _securityValidators)
            {
                await securityValidator.ValidateAndThrow(request);
            }
        }
    }
}