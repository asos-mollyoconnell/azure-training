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
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerDbContext _customerDbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<CustomerRepository> _logger;

        public CustomerRepository(CustomerDbContext customerDbContext, IMapper mapper, ILogger<CustomerRepository> logger)
        {
            _customerDbContext = customerDbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public CustomerModel GetCustomerById(int id)
        {
            var customer = _customerDbContext.Customers
                .Include(c => c.Address)
                .Include(c => c.Contact)
                .Single(c => c.Id == id);
            _logger.LogInformation($"got customers by {id}");

            var customerModel = _mapper.Map<CustomerModel>(customer);
            _logger.LogInformation($"mapped customer to {nameof(CustomerModel)}");

            return customerModel;
        }
    }
}
