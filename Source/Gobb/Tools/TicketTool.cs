using Gobb.Providers;
using ModelContextProtocol.Server;
using System.ComponentModel;

namespace Gobb.Tools
{
    [McpServerToolType]
    public sealed class TicketTool
    {
        private readonly ITicketProvider _ticketProvider;
        public record Input(string TicketId);
        public record Output(string Summary, string Description);

        public TicketTool(ITicketProvider ticketProvider)
        {
            _ticketProvider = ticketProvider ?? throw new ArgumentNullException(nameof(ticketProvider));
        }

        [McpServerTool, Description("Retrieves a ticket's summary and description asynchronously")]
        public async Task<Output> GetTicketDataAsync(Input input)
        {
            var ticketData = await _ticketProvider.GetTicketSummaryAndDescriptionAsync(input.TicketId);
            return new Output(ticketData.Summary, ticketData.Description);
        }
    }
}
