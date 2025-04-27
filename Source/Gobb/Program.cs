using Gobb.Options;
using Gobb.Providers;
using Gobb.Tools;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public sealed class Program
{
    public static async Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        await host.RunAsync();
    }

    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                var env = context.HostingEnvironment;

                config.SetBasePath(Directory.GetCurrentDirectory())
                      .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                      .AddEnvironmentVariables();
            })
            .ConfigureServices((context, services) =>
            {
                ConfigureLogging(context.Configuration, services);
                ConfigureTicketProvider(context.Configuration, services);
                ConfigureMcp(context.Configuration, services);
            });

    private static void ConfigureLogging(IConfiguration configuration, IServiceCollection services)
    {
        services.AddLogging(b =>
            b.AddDebug()
             .AddConsole()
             .AddConfiguration(configuration.GetSection("Logging")));
    }

    private static void ConfigureTicketProvider(IConfiguration configuration, IServiceCollection services)
    {
        services.Configure<JiraClientOptions>(
            configuration.GetSection("JiraClientOptions"));

        services.AddSingleton<JiraClient>();
        services.AddSingleton<ITicketProvider, JiraTicketProvider>();
    }

    private static void ConfigureMcp(IConfiguration configuration, IServiceCollection services)
    {
        services.AddMcpServer()
               .WithStdioServerTransport()
               .WithTools<TicketTool>();
    }
}