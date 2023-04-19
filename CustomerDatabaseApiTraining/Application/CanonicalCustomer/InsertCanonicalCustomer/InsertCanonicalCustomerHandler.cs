using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.CanonicalCustomer.Contracts;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.CanonicalCustomer.InsertCanonicalCustomer
{
    public class InsertCanonicalCustomerHandler : IRequestHandler<InsertCanonicalCustomerRequest, InsertCanonicalCustomerResponse>
    {
        private ICanonicalCustomerRepository _repository;
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
                _logger.LogInformation($"adding customer with id {request.Customer.Id}...");
                var customer = _repository.InsertCustomer(request.Customer);

                _logger.LogInformation($"customer added");
                return new InsertCanonicalCustomerResponse(customer);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"there was a problem adding customer with id {request.Customer.Id}", ex.Message);
                throw;
            }
        }
    }
}
