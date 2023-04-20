using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Application.CanonicalCustomer.Queries.GetById
{
    public class GetCanonicalCustomerByIdRequest : IRequest<GetCanonicalCustomerByIdResponse>
    {
        public GetCanonicalCustomerByIdRequest(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
