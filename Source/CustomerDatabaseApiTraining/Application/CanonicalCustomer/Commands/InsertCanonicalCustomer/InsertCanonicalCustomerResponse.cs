﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Application.CanonicalCustomer.Commands.InsertCanonicalCustomer
{
    public class InsertCanonicalCustomerResponse
    {
        public InsertCanonicalCustomerResponse(CanonicalCustomerModel canonicalCustomer)
        {
            CanonicalCustomer = canonicalCustomer;
        }

        public CanonicalCustomerModel CanonicalCustomer { get; set; }


    }
}
