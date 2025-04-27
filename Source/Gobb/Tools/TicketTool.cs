using Gobb.Providers;
using ModelContextProtocol.Server;
using Microsoft.Extensions.Logging;
using System.ComponentModel;

namespace Gobb.Tools
{
    [McpServerToolType]
    public sealed class TicketTool
    {
        private readonly ITicketProvider _ticketProvider;
        private readonly ILogger<TicketTool> _logger;
        public record Input(string TicketId);
        public record Output(string Summary, string Description);

        public TicketTool(ITicketProvider ticketProvider, ILogger<TicketTool> logger)
        {
            _ticketProvider = ticketProvider ?? throw new ArgumentNullException(nameof(ticketProvider));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _logger.LogDebug("TicketTool initialized.");
        }

        [McpServerTool, Description("Retrieves a ticket's summary and description asynchronously")]
        public async Task<Output> GetTicketDataAsync(Input input)
        {
            _logger.LogDebug("Fetching ticket data for TicketId: {TicketId}", input.TicketId);
            var ticketData = await _ticketProvider.GetTicketSummaryAndDescriptionAsync(input.TicketId);
            _logger.LogDebug("Successfully fetched ticket data for TicketId: {TicketId}", input.TicketId);
            return new Output(ticketData.Summary, ticketData.Description);
        }
    }
}
