using Microsoft.EntityFrameworkCore.Query;
using MediatR;
using Domain.Models;

namespace Application.Customers.GetAllCustomers
{
    public record GetAllCustomersQuery : IRequest<IEnumerable<CustomerModel>>;
}