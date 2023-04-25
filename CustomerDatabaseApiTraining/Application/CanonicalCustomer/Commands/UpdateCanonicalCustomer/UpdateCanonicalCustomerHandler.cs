using Application.CanonicalCustomer.Contracts;
using Application.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.CanonicalCustomer.Commands.UpdateCanonicalCustomer
{
    public class UpdateCanonicalCustomerHandler : IRequestHandler<UpdateCanonicalCustomerRequest>
    {
        private readonly ICanonicalCustomerRepository _repository;
        private readonly ILogger<UpdateCanonicalCustomerHandler> _logger;

        public UpdateCanonicalCustomerHandler(ICanonicalCustomerRepository repository, ILogger<UpdateCanonicalCustomerHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateCanonicalCustomerRequest request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation(
                    $"updating customer with id {request.CanonicalCustomer.Id} begun {DateTime.Now.ToShortTimeString()} ");
                await _repository.UpdateCustomerAsync(request.CanonicalCustomer);
                _logger.LogInformation(
                    $"customer with id {request.CanonicalCustomer.Id} has been updated {DateTime.Now.ToShortTimeString()}");

                return Unit.Value;
            }
            catch (InvalidOperationException ex)
            {
                string message = $"there was no {nameof(CanonicalCustomer)} with id {request.CanonicalCustomer.Id} found";
                _logger.LogError(message, ex);
                throw new EntityNotFoundException(message, ex);
            }
        }
    }
}
