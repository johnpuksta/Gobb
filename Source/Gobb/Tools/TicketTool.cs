using Gobb.Providers;
using ModelContextProtocol.Server;
using Microsoft.Extensions.Logging;
using System.ComponentModel;
using Gobb.Tools.Contracts;

namespace Gobb.Tools
{
    /// <summary>
    /// A MCP server tool used for retrieving ticket data from a ticketing system.
    /// </summary>
    public sealed class TicketTool
    {
        private readonly ITicketProvider _ticketProvider;
        private readonly ILogger<TicketTool> _logger;

        /// <summary>
        /// Constructor for <see cref="TicketTool"/>
        /// </summary>
        /// <param name="ticketProvider">The <see cref="ITicketProvider"/> used to provide ticket data</param>
        /// <param name="logger">The <see cref="ILogger"/> used for logging</param>
        public TicketTool(ITicketProvider ticketProvider, ILogger<TicketTool> logger)
        {
            _ticketProvider = ticketProvider ?? throw new ArgumentNullException(nameof(ticketProvider));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _logger.LogDebug("TicketTool initialized.");
        }

        /// <summary>
        /// Retrieves a ticket's summary and description asynchronously.
        /// </summary>
        /// <param name="input">The <see cref="TicketInput"/> record containing a ticket's ID.</param>
        /// <returns></returns>
        [McpServerTool, Description("Retrieves a ticket's summary and description asynchronously")]
        public async Task<TicketOutput> GetTicketDataAsync(TicketInput input)
        {
            _logger.LogDebug("Fetching ticket data for TicketId: {TicketId}", input.TicketId);
            var ticketData = await _ticketProvider.GetTicketSummaryAndDescriptionAsync(input.TicketId);
            _logger.LogDebug("Successfully fetched ticket data for TicketId: {TicketId}", input.TicketId);
            return new TicketOutput(ticketData.Summary, ticketData.Description);
        }
    }
}
