using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Customers.Contracts;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Customers.GetById
{
    public class GetCustomerByIdHandler : IRequestHandler<GetCustomerByIdRequest, GetCustomerByIdResponse>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger<GetCustomerByIdHandler> _logger;

        public GetCustomerByIdHandler(ICustomerRepository customerRepository, ILogger<GetCustomerByIdHandler> logger)
        {
            _customerRepository = customerRepository;
            _logger = logger;
        }


        public async Task<GetCustomerByIdResponse> Handle(GetCustomerByIdRequest request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"getting customer with id {request.Id}");
                var customer = _customerRepository.GetCustomerById(request.Id);

                _logger.LogInformation($"customer with id {request.Id} found");

                return new GetCustomerByIdResponse(customer);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"no customer found with Id {request.Id}", ex.Message);
                throw ex;
            }
        }
    }
}
