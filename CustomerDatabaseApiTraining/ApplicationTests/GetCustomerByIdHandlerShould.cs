using Application.Customers.Contracts;
using Application.Customers.GetById;
using CustomerData.Models;
using Domain.Models;
using Microsoft.Extensions.Logging;
using Moq;

namespace ApplicationTests
{
    public class GetCustomerByIdHandlerShould
    {
        private readonly Mock<ICustomerRepository> _stubCustomerRepository;
        private readonly Mock<ILogger<GetCustomerByIdHandler>> _stubLogger;

        private readonly GetCustomerByIdHandler _sut;

        public GetCustomerByIdHandlerShould()
        {
            _stubLogger = new Mock<ILogger<GetCustomerByIdHandler>>();
            _stubCustomerRepository = new Mock<ICustomerRepository>();
            _sut = new GetCustomerByIdHandler(_stubCustomerRepository.Object, _stubLogger.Object);
        }

        
        [Fact]
        public void GetValidCustomer_WhenHandlerCalled_GivenValidId()
        {
            // arrange 
            int id = 1;

            var address = new AddressModel()
                { Id = 1, City = "new york", Country = "america", Line1 = "22 corneila street" };

            var contact = new ContactModel() { Id = 1, CustomerId = 1, HomeNumber = 1234567890 };

            var expectedCustomer = new CustomerModel()
                { Id = 1, Address = address, Contact = contact, DateOfBirth = DateTime.Parse("13/12/1989") };


            _stubCustomerRepository.Setup(x => x.GetCustomerById(id)).Returns(expectedCustomer);

            var request = new GetCustomerByIdRequest(id);

            // act 
            var actulResult = _sut.Handle(request, CancellationToken.None);

            // assert
            Assert.Equal(expectedCustomer, actulResult.Result.Customer);
        }

        [Fact]
        public void ReturnArgumentException_WhenHandlerCalled_GivenInvalidId()
        {
            var id = -1;

            var request = new GetCustomerByIdRequest(id);

            _stubCustomerRepository.Setup(x => x.GetCustomerById(id)).Throws(new InvalidOperationException("exception"));

            Action action = () => _sut.Handle(request, CancellationToken.None);

            Assert.Throws<InvalidOperationException>(action);
        }
    }
}