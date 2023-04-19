using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Application.CanonicalCustomer.GetById
{
    public class GetCanonicalCustomerByIdResponse
    {
        public GetCanonicalCustomerByIdResponse(CanonicalCustomerModel customer)
        {
            Customer = customer;
        }

        public CanonicalCustomerModel Customer { get; set; }
    }
}
