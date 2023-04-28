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
            { Id = 1, Address = address, Contact = contact, DateOfBirth = DateTime.Parse("12/13/1989") };


            _stubCustomerRepository.Setup(x => x.GetCustomerById(id)).ReturnsAsync(expectedCustomer);

            var request = new GetCustomerByIdRequest(id);

            // act 
            var actualResult = _sut.Handle(request, CancellationToken.None);

            // assert
            Assert.Equal(expectedCustomer, actualResult.Result.Customer);
        }

        [Fact]
        public void ReturnEntityNotFoundException_WhenHandlerCalled_GivenInvalidId()
        {
            var id = 4;

            var request = new GetCustomerByIdRequest(id);

            _stubCustomerRepository.Setup(x => x.GetCustomerById(request.Id)).ThrowsAsync(new InvalidOperationException("error", new Exception()));

            Task task() =>  _sut.Handle(request, default);

            Assert.ThrowsAsync<InvalidOperationException>(task);

            _stubLogger.Verify(l => l.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, _) => v.ToString().Contains($"no customer found with Id {id}")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>())
                , Times.Once);
        }

        [Fact]
        public async void LogInformation_WhenHandlerCalled_GivenValidRequest()
        {
            // arrange 
            int id = 1;

            var address = new AddressModel()
            { Id = 1, City = "new york", Country = "america", Line1 = "22 corneila street" };

            var contact = new ContactModel() { Id = 1, CustomerId = 1, HomeNumber = 1234567890 };

            var expectedCustomer = new CustomerModel()
            { Id = 1, Address = address, Contact = contact, DateOfBirth = DateTime.Parse("12/13/1989") };


            _stubCustomerRepository.Setup(x => x.GetCustomerById(id)).ReturnsAsync(expectedCustomer);

            var request = new GetCustomerByIdRequest(id);

            // act
            await _sut.Handle(request, CancellationToken.None);

            // assert

            _stubLogger.Verify(l => l.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, _) => v.ToString().Equals($"customer with id {request.Id} found")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>())
                , Times.Once);
        }
    }
}