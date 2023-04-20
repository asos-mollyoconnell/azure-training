using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Application.Customers.GetById
{
    public class GetCustomerByIdRequest:IRequest<GetCustomerByIdResponse>
    {
        public int Id { get; set; }

        public GetCustomerByIdRequest(int id)
        {
            Id = id;
        }
    }
}
