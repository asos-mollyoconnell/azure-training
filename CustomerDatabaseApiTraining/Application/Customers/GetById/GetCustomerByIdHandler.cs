using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Customers.Contracts;
using MediatR;

namespace Application.Customers.GetById
{
    public class GetCustomerByIdHandler : IRequestHandler<GetCustomerByIdRequest, GetCustomerByIdResponse>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetCustomerByIdHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }


        public async Task<GetCustomerByIdResponse> Handle(GetCustomerByIdRequest request, CancellationToken cancellationToken)
        {

                var customer = _customerRepository.GetCustomerById(request.Id);
                return new GetCustomerByIdResponse(customer); 

        }
    }
}
