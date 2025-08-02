using ModelContextProtocol.Server;
using YourNamespace.Data;
using YourNamespace.Models;
using System.Collections.Generic;
using System.Linq;

namespace YourNamespace.Tools
{
    [McpServerToolType]
    public class CustomerTools
    {
        private readonly AppDbContext _context;
        public CustomerTools(AppDbContext context) => _context = context;

        [McpServerTool]
        public IEnumerable<Customer> GetCustomers() => _context.Customers.ToList();

        [McpServerTool]
        public Customer? GetCustomer(int id) => _context.Customers.Find(id);

        [McpServerTool]
        public Customer AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return customer;
        }
    }
}


