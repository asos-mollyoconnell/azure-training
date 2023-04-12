using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Customers.Queries.Contracts
{
    public interface IGetCustomersQuery
    {
        public record GetCustomersQuery : IRequest<IEnumerable<CustomerModel>>;
    }
}
