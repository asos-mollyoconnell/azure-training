using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.CanonicalCustomer.Commands.InsertCanonicalCustomer;
using Application.CanonicalCustomer.Contracts;
using Application.CanonicalCustomer.Queries.GetById;
using Application.Exceptions;
using Domain.Models;
using Microsoft.Extensions.Logging;
using Moq;

namespace ApplicationTests
{
    public class InsertCanonicalCustomerHandlerShould
    {
        private readonly Mock<ICanonicalCustomerRepository> _mockRepository;
        private readonly Mock<ILogger<InsertCanonicalCustomerHandler>> _mockLogger;

        private readonly InsertCanonicalCustomerHandler _sut;

        public InsertCanonicalCustomerHandlerShould()
        {
            _mockLogger = new Mock<ILogger<InsertCanonicalCustomerHandler>>();
            _mockRepository = new Mock<ICanonicalCustomerRepository>();

            _sut = new InsertCanonicalCustomerHandler(_mockRepository.Object, _mockLogger.Object);
        }

        [Fact]
        public void InsertCustomerToDatabase_WhenHandlerCalled_GivenValidId()
        {
            // arrange 
            var customer = new CanonicalCustomerModel { Id = 1, FullName = "Molly O'Connell" };

            var request = new InsertCanonicalCustomerRequest(customer);

            _mockRepository.Setup(r => r.InsertCustomer(request.Customer)).ReturnsAsync(customer);

            // act 
            var actualResult = _sut.Handle(request, default);

            //  assert
            Assert.Equal(customer.FullName, actualResult.Result.CanonicalCustomer.FullName);
            Assert.Equal(TaskStatus.RanToCompletion, actualResult.Status);

        }

        [Fact]
        public void ThrowEntityAlreadyExistsExceptionAndLogError_WhenHandler_GivenIdAlreadyExists()
        {
            // arrange 
            var request = new InsertCanonicalCustomerRequest(new CanonicalCustomerModel { Id = 1, FullName = "Molly" });

            _mockRepository.Setup(r => r.InsertCustomer(request.Customer))
                .ThrowsAsync(new InvalidOperationException("error", new Exception()));

            // act 
            Task task() => _sut.Handle(request, CancellationToken.None);

            // assert
            Assert.ThrowsAsync<EntityAlreadyExistsException>(task);

            _mockLogger.Verify(l => l.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, _) => v.ToString().Contains($"there was a problem adding customer with id {request.Customer.Id}")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>())
                , Times.Once);
        }

        [Fact]
        public void LogInformation_WhenHandlerCalled_GivenValidRequest()
        {
            // arrange 
            var customer = new CanonicalCustomerModel { Id = 1, FullName = "Molly O'Connell" };

            var request = new InsertCanonicalCustomerRequest(customer);

            _mockRepository.Setup(r => r.InsertCustomer(request.Customer)).ReturnsAsync(customer);

            // act 
            var actualResult = _sut.Handle(request, default);

            //  assert
            _mockLogger.Verify(l => l.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, _) => v.ToString().Contains($"adding customer with id {request.Customer.Id} begun at")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>())
                , Times.Once);

            _mockLogger.Verify(l => l.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, _) => v.ToString().Contains($"customer added at ")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>())
                , Times.Once);
        }
    }
}
