using ModelContextProtocol.Server;
using YourNamespace.Data;
using YourNamespace.Models;
using System.Collections.Generic;
using System.Linq;

namespace YourNamespace.Tools
{
    [McpServerToolType]
    public class ProductTools
    {
        private readonly AppDbContext _context;
        public ProductTools(AppDbContext context) => _context = context;

        [McpServerTool]
        public IEnumerable<Product> GetProducts() => _context.Products.ToList();

        [McpServerTool]
        public Product? GetProduct(int id) => _context.Products.Find(id);

        [McpServerTool]
        public Product AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return product;
        }
    }
}
