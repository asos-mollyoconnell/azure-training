using System.Net;
using Application.Customers.GetById;
using Application.Exceptions;
using Castle.Core.Logging;
using CustomerWebApi.Controllers;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace CustomerWebApiTests
{
    public class CustomerControllerShould
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly Mock<ILogger<CustomerController>> _mockLogger;

        private readonly CustomerController _sut;

        public CustomerControllerShould()
        {
            _mockMediator = new Mock<IMediator>();
            _mockLogger = new Mock<ILogger<CustomerController>>();

            _sut = new CustomerController(_mockMediator.Object, _mockLogger.Object);

            _sut.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext()
            };
        }

        [Fact]
        public async Task ReturnOkResponse_WhenGetCustomerById_GivenValidId()
        {
            //arrange
            int id = 1;
            var request = new GetCustomerByIdRequest(id);
            var customer = new CustomerModel()
            {
                Forename = "Molly"
            };
            _mockMediator.Setup(x => x.Send(request, default)).ReturnsAsync(new GetCustomerByIdResponse(customer));

            //act
            var response = await _sut.GetCustomerById(id) as OkObjectResult;
            //assert 
            Assert.Equal((int)HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task ThrowEntityNotFoundException_WhenGetCustomerById_GivenEntityNotFound()
        {
            //arrange 
            int id = 6;
            var request = new GetCustomerByIdRequest(id);
            _mockMediator.Setup(x => x.Send(request, default))
                .ThrowsAsync(new EntityNotFoundException("Error", new Exception()));

            //act
            Task task() => _sut.GetCustomerById(id);

            //assert
            Assert.ThrowsAsync<EntityNotFoundException>(task);

        }
        [Fact]
        public async Task ThrowException_WhenGetCustomerById_GivenAnyOtherException()
        {
            //arrange 
            int id = 6;
            var request = new GetCustomerByIdRequest(id);
            _mockMediator.Setup(x => x.Send(request, default))
                .ThrowsAsync(new Exception());

            //act
            Task task() => _sut.GetCustomerById(id);

            //assert
            Assert.ThrowsAsync<Exception>(task);

        }

    }
}