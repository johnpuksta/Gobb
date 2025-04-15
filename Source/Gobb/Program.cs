using Gobb.Options;
using Gobb.Providers;
using Gobb.Tools;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = Host.CreateApplicationBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
builder.Logging.AddConsole();
builder.Services.AddLogging();

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

builder.Services.Configure<JiraClientOptions>(
    builder.Configuration.GetSection("JiraClientOptions"));

builder.Services.AddTransient<JiraClient>();
builder.Services.AddTransient<ITicketProvider, JiraTicketProvider>();

builder.Services
    .AddMcpServer()
    .WithStdioServerTransport()
    .WithTools<TicketTool>();

await builder.Build().RunAsync();