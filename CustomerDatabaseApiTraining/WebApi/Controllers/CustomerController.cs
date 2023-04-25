using System.Net;
using Application.Customers.GetById;
using Application.Exceptions;
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
                var response = await _mediator.Send(request, HttpContext.RequestAborted);

               // if (response.Customer is null) return NotFound();


                return Ok(response.Customer);
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogError($"problem finding customer with id {id}");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("there was a problem getting a response");
                return StatusCode((int)HttpStatusCode.ServiceUnavailable);
            }
        }
        }
    }
