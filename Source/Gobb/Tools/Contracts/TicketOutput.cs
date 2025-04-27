namespace Gobb.Tools.Contracts
{
    /// <summary>
    /// A record to output the summary and description of a ticket.
    /// </summary>
    /// <param name="Summary">The ticket's summary</param>
    /// <param name="Description">The ticket's description</param>
    public record TicketOutput(string Summary, string Description);
}
