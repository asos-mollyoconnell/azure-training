using Application.Customers.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CustomerWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {

        // private readonly IMediator _mediator;

        //private readonly ISender _sender;

        //public CustomerController( ISender sender)
        //{
        //    _sender = sender;
        //}

        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // public CustomerController(ISender sender) => _sender = sender;

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {

            var request = new GetCustomerByIdRequest(id);
            var response = await _mediator.Send(request);

            return Ok(response);

        }


        //[HttpPost]
        //public async Task<ActionResult<List<Customer>>> AddCustomer(Customer customer)
        //{
        //    _context.Customers.Add(customer);
        //    await _context.SaveChangesAsync();

        //    return Ok(await _context.Customers.ToListAsync());
        //}


        //[HttpGet]
        //public async Task<ActionResult> GetCustomers()
        //{
        //    var customers = await _sender.Send(new IGetCustomersQuery.GetCustomersQuery());

        //    return Ok(customers);
        //}


        //public readonly CustomerDbContext _context;

        //public CustomerController(CustomerDbContext context)
        //{
        //    _context = context;
        //}


        //[HttpGet]
        //public async Task<IEnumerable<CustomerModel>> Get()
        //{
        //    return await _context.Customers.ToListAsync();
        //}

        //[HttpGet("id")]
        //[ProducesResponseType(typeof(CustomerModel), StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> GetById(int id)
        //{
        //    var customer = await _context.Customers.FindAsync(id);

        //    return customer == null ? NotFound() : Ok(customer);
        //}
    }
}
