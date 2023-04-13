using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Customers.Contracts;
using AutoMapper;
using CustomerData;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerDbContext _customerDbContext;
        private readonly IMapper _mapper;

        public CustomerRepository(CustomerDbContext customerDbContext, IMapper mapper)
        {
            _customerDbContext = customerDbContext;
            _mapper = mapper;
        }

        public CustomerModel GetCustomerById(int id)
        {
            return _mapper.Map<CustomerModel>(_customerDbContext.Customers
                 .Include(c => c.Address)
                 .Include(c => c.Contact)
                 .Single(c => c.Id == id));
        }

    }
}
