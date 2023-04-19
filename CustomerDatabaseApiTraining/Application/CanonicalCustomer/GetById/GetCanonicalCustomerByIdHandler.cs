using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.CanonicalCustomer.Contracts;
using Application.Customers.Contracts;
using Application.Customers.GetById;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.CanonicalCustomer.GetById
{
    public class GetCanonicalCustomerByIdHandler : IRequestHandler<GetCanonicalCustomerByIdRequest, GetCanonicalCustomerByIdResponse>
    {
        private ICanonicalCustomerRepository _canonicalCustomerRepository;
        private readonly ILogger<GetCanonicalCustomerByIdHandler> _logger;


        public GetCanonicalCustomerByIdHandler(ICanonicalCustomerRepository canonicalCustomerRepository, ILogger<GetCanonicalCustomerByIdHandler> logger)
        {
            _canonicalCustomerRepository = canonicalCustomerRepository;
            _logger = logger;
        }

        public async Task<GetCanonicalCustomerByIdResponse> Handle(GetCanonicalCustomerByIdRequest request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"getting canonical customer with id {request.Id}");
                var customer = _canonicalCustomerRepository.GetCustomerById(request.Id);

                _logger.LogInformation($"canonical customer with id {request.Id} found");

                return new GetCanonicalCustomerByIdResponse(customer);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"no canonical customer found with Id {request.Id}", ex.Message);
                throw ex;
            }
        }
    }
}
