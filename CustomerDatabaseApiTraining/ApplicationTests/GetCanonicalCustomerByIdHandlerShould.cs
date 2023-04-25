using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.CanonicalCustomer.Contracts;
using Application.CanonicalCustomer.Queries.GetById;
using Application.Exceptions;
using CanonicalCustomerData.Models;
using Domain.Models;
using Microsoft.Extensions.Logging;
using Moq;

namespace ApplicationTests
{
    public class GetCanonicalCustomerByIdHandlerShould
    {
        private readonly Mock<ICanonicalCustomerRepository> _stubRepository;
        private readonly Mock<ILogger<GetCanonicalCustomerByIdHandler>> _stubLogger;

        private readonly GetCanonicalCustomerByIdHandler _sut;

        public GetCanonicalCustomerByIdHandlerShould()
        {
            _stubRepository = new Mock<ICanonicalCustomerRepository>();
            _stubLogger = new Mock<ILogger<GetCanonicalCustomerByIdHandler>>();

            _sut = new GetCanonicalCustomerByIdHandler(_stubRepository.Object, _stubLogger.Object);
        }

        [Fact]
        public void GetValidCanonicalCustomer_WhenHandlerCalled_GivenValidId()
        {
            // arrange 
            int id = 1;

            var expectedCustomer = new CanonicalCustomerModel { Id = 1, FullName = "Molly O'Connell" };

            _stubRepository.Setup(r => r.GetCustomerById(expectedCustomer.Id)).ReturnsAsync(expectedCustomer);

            var request = new GetCanonicalCustomerByIdRequest(id);

            // act 
            var actualResult = _sut.Handle(request, default);

            //  assert
            Assert.Equal(expectedCustomer.FullName, actualResult.Result.Customer.FullName);
            Assert.Equal(TaskStatus.RanToCompletion, actualResult.Status);
        }

        [Fact]
        public void ReturnEntityNotFoundExceptionAndLog_WhenHandlerCalled_GivenInvalidId()
        {
            // arrange
            int id = 12;
            var request = new GetCanonicalCustomerByIdRequest(id);

            _stubRepository.Setup(r => r.GetCustomerById(id))
                .ThrowsAsync(new InvalidOperationException("invalid id", new Exception()));

            // act
            Task task() => _sut.Handle(request, CancellationToken.None);

            // arrange 
            Assert.ThrowsAsync<EntityNotFoundException>(task);

            _stubLogger.Verify(l => l.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, _) => v.ToString().Contains($"no canonical customer found with Id {request.Id}")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>())
                , Times.Once);
        }

        [Fact]
        public void LogInformation_WhenHandlerCalled_GivenValidRequest()
        {
            // arrange 
            var expectedCustomer = new CanonicalCustomerModel { Id = 1, FullName = "Molly O'Connell" };

            _stubRepository.Setup(r => r.GetCustomerById(expectedCustomer.Id)).ReturnsAsync(expectedCustomer);

            var request = new GetCanonicalCustomerByIdRequest(expectedCustomer.Id);

            // act 
            var actualResult = _sut.Handle(request, default);

            //  assert
            _stubLogger.Verify(l => l.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, _) => v.ToString().Equals($"getting canonical customer with id {request.Id}")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>())
                , Times.Once);

            _stubLogger.Verify(l => l.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, _) => v.ToString().Equals($"canonical customer with id {request.Id} found")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>())
                , Times.Once);
        }
    }
}
