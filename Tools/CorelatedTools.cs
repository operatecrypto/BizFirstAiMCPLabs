using ModelContextProtocol.Server;
using YourNamespace.Data;
using YourNamespace.Models;
using YourNamespace.Service;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourNamespace.Tools
{
    [McpServerToolType]
    public class CorrelatedTools
    {
        private readonly AppDbContext _context;
        private readonly AiService _aiService;

        public CorrelatedTools(AppDbContext context, AiService aiService)
        {
            _context = context;
            _aiService = aiService;
        }

        [McpServerTool]
        public async Task<string> AskAiAboutCustomersAndProducts(string question)
        {
            var customers = _context.Customers
                .Select(c => $"Customer: {c.Name}, Email: {c.Email}")
                .ToList();

            var products = _context.Products
                .Select(p => $"Product: {p.Name}, Price: {p.Price:C}")
                .Take(10)  // Only first 10 products if prompt is too large!
                .ToList();

            var prompt = new StringBuilder();
            prompt.AppendLine("Here is the current database info:");
            prompt.AppendLine(string.Join("\n", customers));
            prompt.AppendLine(string.Join("\n", products));
            prompt.AppendLine();
            prompt.AppendLine($"Question: {question}");
            prompt.AppendLine("Answer as helpfully as you can:");

            return await _aiService.AskGeminiAsync(prompt.ToString());
            // var customers = _context.Customers.Select(c => $"Customer: {c.Name}, Email: {c.Email}").ToList();
            // var products = _context.Products.Select(p => $"Product: {p.Name}, Price: {p.Price:C}").ToList();

            // var prompt = new StringBuilder();
            // prompt.AppendLine("Here is the current database info:");
            // prompt.AppendLine(string.Join("\n", customers));
            // prompt.AppendLine(string.Join("\n", products));
            // prompt.AppendLine();
            // prompt.AppendLine($"Question: {question}");
            // prompt.AppendLine("Answer as helpfully as you can:");

            // return await _aiService.AskGeminiAsync(prompt.ToString());
        }
    }
}
