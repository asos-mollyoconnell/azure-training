using Microsoft.EntityFrameworkCore.Query;
using MediatR;
using Domain.Models;

namespace Application.Customers.Queries
{
    public record GetAllCustomersQuery : IRequest<IEnumerable<CustomerModel>>;
}