using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Application.CanonicalCustomer.Contracts
{
    public interface ICanonicalCustomerRepository
    {
         CanonicalCustomerModel GetCustomerById(int id);
         CanonicalCustomerModel InsertCustomer(CanonicalCustomerModel canonicalCustomer);
    }
}
