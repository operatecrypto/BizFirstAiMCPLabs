using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YourNamespace.Data;
using YourNamespace.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        // [HttpGet]
        // public IActionResult Get() => Ok("It works!");

        private readonly AppDbContext _context;

        public CustomersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }

        // GET: api/customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
                return NotFound();
            return customer;
        }

        // POST: api/customers
        [HttpPost]
        public async Task<ActionResult<Customer>> AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer);
        }
        // // GET: api/customers/5
        // [HttpGet("{id}")]
        // public async Task<ActionResult<IEnumerable<Customer>>> GetCustomer(int id)
        // {
        //     return await _context.Customers.ToListAsync();
        // }

        // // POST: api/customers/5
        // [HttpPost]
        // public async Task<ActionResult<Customer>> AddCustomer(Customer customer)
        // {
        //     _context.Customers.Add(customer);
        //     await _context.SaveChangesAsync();
        //     return CreatedAtAction(nameof(GetCustomers), new { id = customer.Id }, customer);
        // }
    }
}
