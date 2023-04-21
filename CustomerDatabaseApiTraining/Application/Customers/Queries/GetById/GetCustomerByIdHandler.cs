using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Customers.Contracts;
using Application.Exceptions;
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
                var customer = await _customerRepository.GetCustomerById(request.Id);

                _logger.LogInformation($"customer with id {request.Id} found");

                return new GetCustomerByIdResponse(customer);
            }
            catch (InvalidOperationException ex)
            {
                string message = $"no customer found with Id {request.Id}";
                _logger.LogError(message, ex);
                throw new EntityNotFoundException(message, ex);
            }
        }
    }
}
