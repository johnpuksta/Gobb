using Gobb.Data;

namespace Gobb.Providers
{
    /// <summary>
    /// A service that provides ticket data.
    /// </summary>
    public interface ITicketProvider
    {
        /// <summary>
        /// Retrieves a ticket's summary and description asynchronously.
        /// </summary>
        /// <param name="ticketId">The ticket's Id</param>
        /// <returns>An <see cref="ITicketData"/> object holding the ticket's summary and description</returns>
        public Task<ITicketData> GetTicketSummaryAndDescriptionAsync(string ticketId);
    }
}
