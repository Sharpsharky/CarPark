using FluentValidation;
using MediatR;

namespace CarPark.Application.Common.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators) => _validators = validators;

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var errors = (await Task.WhenAll(
                        _validators.Select(v => v.ValidateAsync(context, cancellationToken))))
                    .SelectMany(r => r.Errors)
                    .Where(f => f is not null)
                    .ToList();

                if (errors.Count > 0)
                {
                    var message = string.Join("; ", errors.Select(e => e.ErrorMessage));
                    throw new ValidationException(message, errors);
                }
            }

            return await next();
        }
    }
}
