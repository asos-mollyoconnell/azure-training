using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Customers.Queries;
using Domain.Models;
using Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers
{
    public class GetAllCustomersHandler : IRequestHandler<GetAllCustomersQuery, IEnumerable<CustomerModel>>
    {
        private readonly CustomerDbContext _context;

        public GetAllCustomersHandler(CustomerDbContext context) => _context = context;

        public async Task<IEnumerable<CustomerModel>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            return await _context.Customers.ToListAsync();
        }

    }
}
