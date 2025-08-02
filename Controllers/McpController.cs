using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using YourNamespace.Data;
using YourNamespace.Models;

[ApiController]
[Route("mcp")]
public class McpController : ControllerBase
{
    private readonly AppDbContext _context;

    public McpController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult Post([FromBody] JObject body)
    {
        string method = body.Value<string>("method");
        var id = body["id"];
        var result = (object)null;
        var error = (object)null;

        try
        {
            if (method == "get_customers")
            {
                result = _context.Customers.ToList();
            }
            else if (method == "get_customer")
            {
                int idParam = body["params"]?["id"]?.Value<int>() ?? 0;
                var customer = _context.Customers.Find(idParam);
                result = customer;
            }
            else if (method == "add_customer")
            {
                var customerObj = body["params"]?["customer"]?.ToObject<Customer>();
                if (customerObj == null) throw new Exception("Customer data missing");
                _context.Customers.Add(customerObj);
                _context.SaveChanges();
                result = customerObj;
            }
            else
            {
                error = new { code = -32601, message = "Method not found" };
            }
        }
        catch (Exception ex)
        {
            error = new { code = -32000, message = ex.Message };
        }

        var response = new JObject
        {
            ["jsonrpc"] = "2.0",
            ["id"] = id
        };

        if (error != null)
            response["error"] = JObject.FromObject(error);
        else
            response["result"] = result != null ? JToken.FromObject(result) : JValue.CreateNull();

        return Ok(response);
    }
}


// using JsonRpc.AspNetCore;
// using Microsoft.AspNetCore.Mvc;
// using YourNamespace.Data;
// using YourNamespace.Models;

// [Route("mcp")]
// public class McpController : JsonRpcController
// {
//     private readonly AppDbContext _context;

//     public McpController(AppDbContext context)
//     {
//         _context = context;
//     }

//     // MCP "get_customers" method
//     [JsonRpcMethod("get_customers")]
//     public IEnumerable<Customer> GetCustomers()
//     {
//         return _context.Customers.ToList();
//     }

//     // MCP "get_customer" method
//     [JsonRpcMethod("get_customer")]
//     public Customer GetCustomer(int id)
//     {
//         return _context.Customers.FirstOrDefault(c => c.Id == id);
//     }

//     // MCP "add_customer" method
//     [JsonRpcMethod("add_customer")]
//     public Customer AddCustomer(Customer customer)
//     {
//         _context.Customers.Add(customer);
//         _context.SaveChanges();
//         return customer;
//     }
// }
