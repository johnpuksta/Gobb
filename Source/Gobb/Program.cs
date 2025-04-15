using Gobb.Options;
using Gobb.Providers;
using Gobb.Tools;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = Host.CreateApplicationBuilder(args);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

builder.Services.AddLogging(b =>
    b
        .AddDebug()
        .AddConsole()
        .AddConfiguration(builder.Configuration.GetSection("Logging"))
        .SetMinimumLevel(LogLevel.Information)
);

builder.Services.Configure<JiraClientOptions>(
    builder.Configuration.GetSection("JiraClientOptions"));

builder.Services.AddSingleton<JiraClient>();
builder.Services.AddSingleton<ITicketProvider, JiraTicketProvider>();

builder.Services
    .AddMcpServer()
    .WithStdioServerTransport()
    .WithTools<TicketTool>();

await builder.Build().RunAsync();