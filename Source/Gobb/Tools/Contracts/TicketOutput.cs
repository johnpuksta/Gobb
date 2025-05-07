namespace Gobb.Tools.Contracts
{
    /// <summary>
    /// Represents the output of a ticket retrieval operation.
    /// </summary>
    public class TicketOutput
    {
        /// <summary>
        /// The ticket's summary.
        /// </summary>
        public string Summary { get; init; }

        /// <summary>
        /// The ticket's description.
        /// </summary>
        public string Description { get; init; }

        /// <summary>
        /// Constructor for <see cref="TicketOutput"/>
        /// </summary>
        /// <param name="summary">The ticket's summary</param>
        /// <param name="description">The ticket's description</param>
        public TicketOutput(string summary, string description)
        {
            Summary = summary;
            Description = description;
        }
    }
}
