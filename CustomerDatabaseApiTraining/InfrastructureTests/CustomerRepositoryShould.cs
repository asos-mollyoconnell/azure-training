using AutoMapper;
using CustomerData;
using Infrastructure.Customer;
using Microsoft.Extensions.Logging;
using Moq;

namespace InfrastructureTests
{
    public class CustomerRepositoryShould
    {
        private readonly Mock<CustomerDbContext> _stubContext;
        private readonly Mock<IMapper> _stubMapper;
        private readonly Mock<ILogger<CustomerRepository>> _stubLogger;

        private readonly CustomerRepository _sut;

        public CustomerRepositoryShould()
        {
            _stubContext = new Mock<CustomerDbContext>();
            _stubMapper = new Mock<IMapper>();
            _stubLogger = new Mock<ILogger<CustomerRepository>>();

            _sut = new CustomerRepository(_stubContext.Object, _stubMapper.Object, _stubLogger.Object);
        }

        [Fact]
        public void CallMapper_whenGetCustomerById_GivenValidId()
        {
            // arrange 


            // act 


            // assert

        }
    }
}