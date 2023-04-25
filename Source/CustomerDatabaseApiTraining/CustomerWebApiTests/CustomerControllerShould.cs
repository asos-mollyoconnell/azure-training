using Application.Customers.GetById;
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
        public async Task ReturnOkResponse_WhenGetCustomerByIdAsync_GivenValidId()
        {
            // arrange 
            int id = 1;

            _mockMediator.Setup(m => m.Send(It.Is<GetCustomerByIdRequest>(r => r.Id == id), default))
                .ReturnsAsync(new GetCustomerByIdResponse(new CustomerModel() { Forename = "Molly" }));

            // act 
            var response = await _sut.GetCustomerById(id) as OkObjectResult;

            var customer = response.Value as GetCustomerByIdResponse;

            // assert 
            Assert.Equal("Molly", customer.Customer.Forename);
        }

        [Fact]
        public async Task ReturnNotFoundResponse_WhenGetCustomerByIdAsync_GivenValidId()
        {
            // arrange 


            // act 


            // assert 

        }
    }
}