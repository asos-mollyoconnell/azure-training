﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Application.Customers.Contracts
{
    public interface ICustomerRepository
    {
        CustomerModel GetCustomerById(int id);
    }
}
