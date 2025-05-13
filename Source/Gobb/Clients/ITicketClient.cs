using Gobb.Data;

namespace Gobb.Clients
{
    /// <summary>
    /// A client that handles all API requests to retrieve ticket data.
    /// </summary>
    public interface ITicketClient
    {
        /// <summary>
        /// Asynchronously retrieves a ticket by its Id.
        /// </summary>
        /// <param name="ticketId">The ticket's Id</param>
        /// <returns>A <see cref="Task"/> containing <see cref="ITicketData"/> data on success</returns>
        public Task<ITicketData> GetTicketAsync(string ticketId);
    }
}
