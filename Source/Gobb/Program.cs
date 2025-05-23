﻿using Gobb.Clients;
using Gobb.Options;
using Gobb.Tools;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

/// <summary>
/// A program that sets up and runs a host for the Gobb application.
/// </summary>
public sealed class Program
{
    /// <summary>
    /// Main entry point for the application.
    /// </summary>
    public static async Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        await host.RunAsync();
    }

    /// <summary>
    /// Creates a host builder for the application.
    /// </summary>
    /// <returns>An <see cref="IHostBuilder"/></returns>
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
                ConfigureMcp(services);
            });

    /// <summary>
    /// Configures logging for the application.
    /// </summary>
    /// <param name="configuration">The <see cref="IConfiguration"/> object holding appsettings</param>
    /// <param name="services">The <see cref="IServiceCollection"/> used for dependency injection</param>
    private static void ConfigureLogging(IConfiguration configuration, IServiceCollection services)
    {
        services.AddLogging(b =>
            b.AddDebug()
             .AddConsole()
             .AddConfiguration(configuration.GetSection("Logging")));
    }

    /// <summary>
    /// Configures the ticket provider for the application.
    /// </summary>
    /// <param name="configuration">The <see cref="IConfiguration"/> object holding appsettings</param>
    /// <param name="services">The <see cref="IServiceCollection"/> used for dependency injection</param>
    private static void ConfigureTicketProvider(IConfiguration configuration, IServiceCollection services)
    {
        var ticketProviderType = configuration["TicketClient:Type"];

        if (ticketProviderType == "Jira")
        {
            services.Configure<JiraClientOptions>(
                configuration.GetSection("JiraClientOptions"));
            services.AddSingleton<ITicketClient, JiraClient>();
        }
        else if (ticketProviderType == "GitHub")
        {
            services.Configure<GitHubClientOptions>(
                configuration.GetSection("GitHubClientOptions"));
            services.AddSingleton<ITicketClient, GitHubClient>();
        }
        else
        {
            throw new InvalidOperationException("Invalid TicketClient type specified in configuration.");
        }
    }

    /// <summary>
    /// Configures the exposed Mcp server tools for the application.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> used for dependency injection</param>
    private static void ConfigureMcp(IServiceCollection services)
    {
        services.AddMcpServer()
               .WithStdioServerTransport()
               .WithTools<TicketTool>();
    }
}