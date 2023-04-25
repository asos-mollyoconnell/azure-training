using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using MediatR;

namespace Application.CanonicalCustomer.Commands.UpdateCanonicalCustomer
{
    public class UpdateCanonicalCustomerRequest : IRequest
    {
        public UpdateCanonicalCustomerRequest(CanonicalCustomerModel canonicalCustomer)
        {
            CanonicalCustomer = canonicalCustomer;
        }

        public CanonicalCustomerModel CanonicalCustomer { get; set; }
    }
}
