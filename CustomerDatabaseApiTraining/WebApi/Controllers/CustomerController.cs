using System.Runtime.CompilerServices;
using Application.Customers.Queries.Contracts;
using Domain.Models;
using Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        ////private readonly CustomerDbContext _context;
        //// private readonly IMediator _mediator;l

        //private readonly ISender _sender;
        //public CustomerController(ISender sender) => _sender = sender;

        //[HttpGet]
        //public async Task<ActionResult> GetCustomers()
        //{
        //    var customers = await _sender.Send(new IGetCustomersQuery.GetCustomersQuery());

        //    return Ok(customers);
        //}


        public readonly CustomerDbContext _context;

        public CustomerController(CustomerDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IEnumerable<CustomerModel>> Get()
        {
            return await _context.Customers.ToListAsync();
        }

        [HttpGet("id")]
        [ProducesResponseType(typeof(CustomerModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            return customer == null ? NotFound() : Ok(customer);
        }
    }
}
