using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.CanonicalCustomer.Contracts;
using AutoMapper;
using CanonicalCustomerData;
using CustomerData.Models;
using Domain.Models;
using Microsoft.Extensions.Logging;

namespace Infrastructure.CanonicalCustomer
{
    public class CanonicalCustomerRepository : ICanonicalCustomerRepository
    {
        private CanonicalCustomerDbContext _context;
        private IMapper _mapper;
        private ILogger<CanonicalCustomerRepository> _logger;

        public CanonicalCustomerRepository(CanonicalCustomerDbContext context, IMapper mapper, ILogger<CanonicalCustomerRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public CanonicalCustomerModel GetCustomerById(int id)
        {
            var customer = _context.CanonicalCustomers.Single(c => c.Id == id);
            _logger.LogInformation($"got canonical customers by {id}");

            var mappedCustomer = _mapper.Map<CanonicalCustomerModel>(customer);
            _logger.LogInformation($"mapped canonical customer to {nameof(CustomerModel)}");

            return mappedCustomer;
        }

        public CanonicalCustomerModel InsertCustomer(CanonicalCustomerModel canonicalCustomer)
        {
            var customer = _mapper.Map<CanonicalCustomerData.Models.CanonicalCustomer>(canonicalCustomer);
            _logger.LogInformation($"creating canonical customer");

            _context.CanonicalCustomers.Add(customer);
            _logger.LogInformation($"adding customer to database");

            if (_context.SaveChanges() > 0) _logger.LogInformation($"added customer with id {customer.Id}");

            var mappedCustomer = _mapper.Map<CanonicalCustomerModel>(customer);

            return mappedCustomer;
        }
    }
}
