using Application.CanonicalCustomer.GetById;
using Application.CanonicalCustomer.InsertCanonicalCustomer;
using Application.Customers.GetById;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CanonicalCustomerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CanonicalCustomerController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly ILogger<CanonicalCustomerController> _logger;

        public CanonicalCustomerController(ISender sender, ILogger<CanonicalCustomerController> logger)
        {
            _sender = sender;
            _logger = logger;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            try
            {
                _logger.LogInformation($"getting customer by id begun {DateTime.Now}");
                var request = new GetCanonicalCustomerByIdRequest(id);
                var response = await _sender.Send(request);

                if (response.Customer is null)
                {
                    _logger.LogWarning($"no customer with id {id} found");
                    return NotFound();
                }

                _logger.LogInformation($"got customer at {DateTime.Now}");
                return Ok(response.Customer);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"problem finding canonical customer with id {id}");
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> InsertCanonicalCustomer(CanonicalCustomerModel customer)
        {
            _logger.LogInformation($"inserting customer request begun at {DateTime.Now}");
            var request = new InsertCanonicalCustomerRequest(customer);

            var response = await _sender.Send(request);

            _logger.LogInformation($"customer add at {DateTime.Now}");
            return Ok(response);
        }
    }
}
