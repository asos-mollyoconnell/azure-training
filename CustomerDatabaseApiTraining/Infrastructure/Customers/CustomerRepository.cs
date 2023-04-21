using Application.Customers.Contracts;
using AutoMapper;
using CustomerData;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Customer
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

        public async Task<CustomerModel> GetCustomerById(int id)
        {
             var customer = await _customerDbContext.Customers
                .Include(c => c.Address)
                .Include(c => c.Contact)
                .SingleAsync(c => c.Id == id);
            _logger.LogInformation($"got customers by {id}");

            var customerModel = _mapper.Map<CustomerModel>(customer);
            _logger.LogInformation($"mapped customer to {nameof(CustomerModel)}");

            return customerModel;
        }
    }
}
