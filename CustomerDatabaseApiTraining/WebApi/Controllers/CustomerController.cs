using Application.Customers.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CustomerWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(IMediator mediator, ILogger<CustomerController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            try
            {
                var request = new GetCustomerByIdRequest(id);
                var response = await _mediator.Send(request);

                if (response.Customer is null) return NotFound();
                

                return Ok(response.Customer);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"problem finding customer with id {id}");
                throw;
            }
        }
    }
}
