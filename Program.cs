using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using ModelContextProtocol.Server;

// Make sure you have these "using" statements for your own code:
using YourNamespace.Data; // Where AppDbContext is
using YourNamespace.Tools; // Where Customer Tools, Product Tools and Correalted Tools is
using YourNamespace.Service;

var builder = Host.CreateDefaultBuilder(args);
// var builder = Host.CreateDefaultBuilder(args)
//     .ConfigureAppConfiguration((hostingContext, config) =>
//     {
//         config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
//         config.AddEnvironmentVariables();
//     });

builder.ConfigureServices((context, services) =>
{
    services.AddDbContext<AppDbContext>(options =>
        options.UseSqlite("Data Source=customer.db"));

    services.AddSingleton<AiService>();

    services.AddMcpServer()
        .WithStdioServerTransport()  // Use STDIO transport for MCP Inspector
        .WithTools<CustomerTools>()
        .WithTools<ProductTools>()
        .WithTools<CorrelatedTools>();
});

await builder.Build().RunAsync();

// Here I have created a mcp server in MCPController in controllers folders

// using Microsoft.EntityFrameworkCore;
// using YourNamespace.Data;

// var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddControllers();
// builder.Services.AddDbContext<AppDbContext>(options =>
//     options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

// // Add JsonRpc support
// // builder.Services.AddJsonRpc();

// var app = builder.Build();

// app.UseSwagger();
// app.UseSwaggerUI();

// app.UseHttpsRedirection();
// app.UseAuthorization();
// app.MapControllers();

// // Add the JsonRpc endpoint
// // app.MapJsonRpc("/mcp");

// app.Run();

// Here the program is running and you will see it in swagger

// using Microsoft.EntityFrameworkCore;
// using YourNamespace.Data;

// var builder = WebApplication.CreateBuilder(args);

// // Register services
// builder.Services.AddControllers();
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();


// // Register DbContext
// // builder.Services.AddDbContext<AppDbContext>(options =>
// //     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// builder.Services.AddDbContext<AppDbContext>(options =>
//     options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));


// builder.Services.AddControllers();

// var app = builder.Build();

// // app.UseDeveloperExceptionPage();


// if (app.Environment.IsDevelopment())
// {
//     // app.UseDeveloperExceptionPage();
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }
// // else
// // {
// //     app.UseSwagger();
// //     app.UseSwaggerUI();
// // }


// app.MapControllers();
// app.UseHttpsRedirection();
// app.UseAuthorization();
// app.Run();

// This is initial code provided at the starting while you created the project

// var builder = WebApplication.CreateBuilder(args);

// // Add services to the container.
// // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// builder.Services.AddOpenApi();

// var app = builder.Build();

// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.MapOpenApi();
// }

// app.UseHttpsRedirection();

// var summaries = new[]
// {
//     "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
// };

// app.MapGet("/weatherforecast", () =>
// {
//     var forecast =  Enumerable.Range(1, 5).Select(index =>
//         new WeatherForecast
//         (
//             DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//             Random.Shared.Next(-20, 55),
//             summaries[Random.Shared.Next(summaries.Length)]
//         ))
//         .ToArray();
//     return forecast;
// })
// .WithName("GetWeatherForecast");

// app.Run();

// record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
// {
//     public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
// }
