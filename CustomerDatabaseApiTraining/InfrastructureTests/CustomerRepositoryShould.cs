using AutoMapper;
using CustomerData;
using Infrastructure.Repositories;
using Moq;

namespace InfrastructureTests
{
    public class CustomerRepositoryShould
    {
        private readonly Mock<CustomerDbContext> _context;
        private readonly Mock<IMapper> _mapper;

        private readonly CustomerRepository _sut;

        public CustomerRepositoryShould()
        {
            _context = new Mock<CustomerDbContext>();
            _mapper = new Mock<IMapper>();

            _sut = new CustomerRepository(_context.Object, _mapper.Object);
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