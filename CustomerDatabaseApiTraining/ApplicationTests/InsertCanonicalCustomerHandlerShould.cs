using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.CanonicalCustomer.Commands.InsertCanonicalCustomer;
using Application.CanonicalCustomer.Contracts;
using Application.CanonicalCustomer.Queries.GetById;
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
            Assert.Equal(expectedCustomer.FullName, actualResult.Result.Customer.FullName);
        }
    }
}
