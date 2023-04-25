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
        Task<CanonicalCustomerModel> GetCustomerById(int id);
        Task<CanonicalCustomerModel> InsertCustomer(CanonicalCustomerModel canonicalCustomer);
        Task UpdateCustomerAsync(CanonicalCustomerModel requestCustomer);
    }
}
