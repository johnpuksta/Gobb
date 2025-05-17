namespace Gobb.Data
{
    /// <summary>
    /// A template for ticket context.
    /// </summary>
    public interface ITicketContext
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
        /// The ticket's comments.
        /// </summary>
        public IList<string> Comments { get; init; }
    }
}
