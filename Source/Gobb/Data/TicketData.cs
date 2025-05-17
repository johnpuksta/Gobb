namespace Gobb.Data
{
    /// <summary>
    /// A basic implementation of <see cref="ITicketData"/> to store ticket data."/>
    /// </summary>
    public class TicketData: ITicketData
    {
        /// <summary>
        /// Constructor for <see cref="TicketData"/>
        /// </summary>
        /// <param name="summary">The ticket's summary</param>
        /// <param name="description">The ticket's description</param>
        /// <param name="comments">The ticket's comments</param>
        public TicketData(string summary, string description, IList<string> comments)
        {
            Summary = summary;
            Description = description;
            Comments = comments;
        }

        /// <inheritdoc/>
        public string Summary { get; init; }

        /// <inheritdoc/>
        public string Description { get; init; }

        /// <inheritdoc/>
        public IList<string> Comments { get; init; }
    }
}
