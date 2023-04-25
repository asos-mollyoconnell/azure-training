using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.CanonicalCustomer.Contracts;
using Application.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.CanonicalCustomer.Commands.InsertCanonicalCustomer
{
    public class InsertCanonicalCustomerHandler : IRequestHandler<InsertCanonicalCustomerRequest, InsertCanonicalCustomerResponse>
    {
        private readonly ICanonicalCustomerRepository _repository;
        private readonly ILogger<InsertCanonicalCustomerHandler> _logger;

        public InsertCanonicalCustomerHandler(ICanonicalCustomerRepository repository, ILogger<InsertCanonicalCustomerHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<InsertCanonicalCustomerResponse> Handle(InsertCanonicalCustomerRequest request,
            CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"adding customer with id {request.Customer.Id} begun at {DateTime.Now.ToShortTimeString()}");
                var customer = await _repository.InsertCustomer(request.Customer);

                _logger.LogInformation($"customer added at {DateTime.Now.ToShortTimeString()}");
                return new InsertCanonicalCustomerResponse(customer);
            }
            catch (InvalidOperationException ex)
            {
                string message =$"there was a problem adding customer with id {request.Customer.Id}";
                _logger.LogError(message, ex);
                throw new EntityAlreadyExistsException(message, ex);
            }
        }
    }
}
