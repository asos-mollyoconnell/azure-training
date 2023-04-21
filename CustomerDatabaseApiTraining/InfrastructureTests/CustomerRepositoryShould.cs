using AutoMapper;
using CustomerData;
using CustomerData.Models;
using Domain.Models;
using Infrastructure.Customer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace InfrastructureTests
{
    public class CustomerRepositoryShould
    {
        private readonly CustomerDbContext _stubContext;
        private readonly IMapper _stubMapper;
        private readonly Mock<ILogger<CustomerRepository>> _stubLogger;

        private readonly CustomerRepository _sut;

        public CustomerRepositoryShould()
        {
            var config = new MapperConfiguration(m => m.CreateMap<Customer, CustomerModel>());
            _stubContext = CreateContext();
            _stubMapper = new Mapper(config);
            _stubLogger = new Mock<ILogger<CustomerRepository>>();
        }

        [Fact]
        public void ReturnCustomer_whenGetCustomerById_GivenValidId()
        {
            // arrange 
            int id = 1;

            var customers = new List<Customer>
            {
                new Customer() { Id = 1, Forename = "Molly" },
                new Customer() { Id = 2, Forename = "Miranda"}
            };

            var _sut = CreateRepository(customers);

            // act 
            var actualResult = _sut.GetCustomerById(id);

            // assert
            Assert.Equal("Molly", actualResult.Result.Forename);
        }



        private CustomerDbContext CreateContext()
        {
            var dbOptions = new DbContextOptionsBuilder<CustomerDbContext>().UseInMemoryDatabase(new Guid().ToString()).Options;

            return new CustomerDbContext(dbOptions);
        }

        private CustomerRepository CreateRepository(IEnumerable<Customer> customers)
        {
            _stubContext.AddRange(customers);
            _stubContext.SaveChanges();
            return new CustomerRepository(_stubContext, _stubMapper, _stubLogger.Object);
        }
    }
}