using Application.CanonicalCustomer.Contracts;
using AutoMapper;
using CanonicalCustomerData;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
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

        public async Task<CanonicalCustomerModel> GetCustomerById(int id)
        {
            var customer = await _context.CanonicalCustomers.SingleAsync(c => c.Id == id);
            _logger.LogInformation($"got {nameof(CanonicalCustomer)} by {id}");

            var mappedCustomer = _mapper.Map<CanonicalCustomerModel>(customer);
            _logger.LogInformation($"mapped {nameof(CanonicalCustomer)} to {nameof(CanonicalCustomerModel)}");

            return mappedCustomer;
        }

        public async Task<CanonicalCustomerModel> InsertCustomer(CanonicalCustomerModel canonicalCustomer)
        {
            _logger.LogInformation($"creating {nameof(CanonicalCustomer)}");
            var customer = _mapper.Map<CanonicalCustomerData.Models.CanonicalCustomer>(canonicalCustomer);
            
            await _context.CanonicalCustomers.AddAsync(customer);
            _logger.LogInformation($"adding {nameof(CanonicalCustomer)} to database");

            if (_context.SaveChanges() > 0) _logger.LogInformation($"added {nameof(CanonicalCustomer)} with id {customer.Id}");

            var canonicalCustomerModel = _mapper.Map<CanonicalCustomerModel>(customer);

            return canonicalCustomerModel;
        }

        public async Task UpdateCustomerAsync(CanonicalCustomerModel requestCustomer)
        {
            _logger.LogInformation($"checking for customer with id {requestCustomer.Id}...");
            bool exists = await _context.CanonicalCustomers.AnyAsync(c => c.Id == requestCustomer.Id);

            if (!exists)
                throw new InvalidOperationException(
                    $"no {nameof(CanonicalCustomer)} with id {requestCustomer.Id} found");

            var mappedCustomer = _mapper.Map<CanonicalCustomerData.Models.CanonicalCustomer>(requestCustomer);
            _logger.LogInformation($"mapped {nameof(CanonicalCustomerModel)} to {nameof(CanonicalCustomer)}");
            
            _context.ChangeTracker.Clear();
            _context.Update(mappedCustomer);
            _logger.LogInformation($"{nameof(CanonicalCustomer)} has been updated");

            var canonicalCustomerModel = _mapper.Map<CanonicalCustomerModel>(requestCustomer);
            _logger.LogInformation($"mapped {nameof(CanonicalCustomer)} to {nameof(CanonicalCustomerModel)}");

            await _context.SaveChangesAsync();
        }
    }
}
