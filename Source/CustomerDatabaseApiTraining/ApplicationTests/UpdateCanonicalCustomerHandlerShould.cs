using Application.CanonicalCustomer.Contracts;
using Domain.Models;
using Microsoft.Extensions.Logging;
using Moq;
using Application.CanonicalCustomer.Commands.UpdateCanonicalCustomer;
using Application.Exceptions;
using CanonicalCustomerData.Models;

namespace ApplicationTests
{
    public class UpdateCanonicalCustomerHandlerShould
    {
        private readonly Mock<ICanonicalCustomerRepository> _mockRepository;
        private readonly Mock<ILogger<UpdateCanonicalCustomerHandler>> _mockLogger;

        private readonly UpdateCanonicalCustomerHandler _sut;

        public UpdateCanonicalCustomerHandlerShould()
        {
            _mockLogger = new Mock<ILogger<UpdateCanonicalCustomerHandler>>();
            _mockRepository = new Mock<ICanonicalCustomerRepository>();

            _sut = new UpdateCanonicalCustomerHandler(_mockRepository.Object, _mockLogger.Object);
        }


        [Fact]
        public void UpdateCustomerToDatabase_WhenHandlerCalled_GivenValidId()
        {
            // arrange 
            var customer = new CanonicalCustomerModel { Id = 1, FullName = "Molly O'Connell" };

            var request = new UpdateCanonicalCustomerRequest(customer);

            _mockRepository.Setup(r => r.UpdateCustomerAsync(request.CanonicalCustomer));

            // act 
            var actualResult = _sut.Handle(request, default);


            //  assert
            Assert.Equal(TaskStatus.RanToCompletion, actualResult.Status);


            // should this be similar to the repository tests? fake list of customers then update the list ? 
            // decided no bc this will be covered in repository update tests 
        }

        [Fact]
        public void ThrowEntityNotFoundExceptionAndLogError_WhenHandler_GivenIdNotFound()
        {
            // arrange 
            var request =
                new UpdateCanonicalCustomerRequest(new CanonicalCustomerModel() { Id = 1, FullName = "Molly" });

            _mockRepository.Setup(r => r.UpdateCustomerAsync(request.CanonicalCustomer)).ThrowsAsync( new InvalidOperationException( "error", new Exception()));

            // act 
            Task task() => _sut.Handle(request, CancellationToken.None);

            // assert 
            Assert.ThrowsAsync<EntityNotFoundException>(task);

            _mockLogger.Verify(l => l.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, _) => v.ToString().Contains($"there was no {nameof(CanonicalCustomer)} with id {request.CanonicalCustomer.Id} found")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>())
                , Times.Once);
        }

        [Fact]
        void LogInformation_WhenHandlerCalled_GivenValidRequest()
        {
            // arrange 
            var customer = new CanonicalCustomerModel { Id = 1, FullName = "Molly O'Connell" };

            var request = new UpdateCanonicalCustomerRequest(customer);

            _mockRepository.Setup(r => r.UpdateCustomerAsync(request.CanonicalCustomer));

            // act 
            var actualResult = _sut.Handle(request, default);


            //  assert
            _mockLogger.Verify(l => l.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, _) => v.ToString().Contains($"updating customer with id {request.CanonicalCustomer.Id} begun")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>())
                , Times.Once);

            _mockLogger.Verify(l => l.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, _) => v.ToString().Contains($"customer with id {request.CanonicalCustomer.Id} has been updated ")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>())
                , Times.Once);
        }
    }
}
