using Gobb.Managers;
using Gobb.Options;
using Gobb.Providers;
using Gobb.Tools;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = Host.CreateApplicationBuilder(args);
builder.Logging.AddConsole(consoleLogOptions =>
{
    // Configure all logs to go to stderr
    consoleLogOptions.LogToStandardErrorThreshold = LogLevel.Trace;
});

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

builder.Services.Configure<JiraContextProviderOptions>(
    builder.Configuration.GetSection("JiraContextProviderSettings"));

builder.Services.Configure<GitRepositoryManagerOptions>(
    builder.Configuration.GetSection("GitRepositoryManagerOptions"));

builder.Services.AddTransient<ITicketProvider, JiraTicketProvider>();
builder.Services.AddTransient<IRepositoryManager, GitRepositoryManager>();

builder.Services
    .AddMcpServer()
    .WithStdioServerTransport()
    .WithTools<TicketTool>()
    .WithTools<GitTool>();

await builder.Build().RunAsync();