namespace Gobb.Data
{
    /// <summary>
    /// A template for ticket data.
    /// </summary>
    public interface ITicketData
    {
        /// <summary>
        /// The ticket's summary.
        /// </summary>
        public string Summary { get; init; }

        /// <summary>
        /// The ticket's description.
        /// </summary>
        public string Description { get; init; }
    }
}
