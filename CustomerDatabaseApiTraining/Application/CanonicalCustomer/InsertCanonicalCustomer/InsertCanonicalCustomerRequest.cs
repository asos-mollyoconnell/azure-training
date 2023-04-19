using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using MediatR;

namespace Application.CanonicalCustomer.InsertCanonicalCustomer
{
    public class InsertCanonicalCustomerRequest : IRequest<InsertCanonicalCustomerResponse>
    {
        public InsertCanonicalCustomerRequest(CanonicalCustomerModel customer)
        {
            Customer = customer;
        }

        public CanonicalCustomerModel Customer { get; set; }
    }
}
