using Gobb.Data;

namespace Gobb.Providers
{
    public interface ITicketProvider
    {
        public Task<ITicketData> GetTicketSummaryAndDescriptionAsync(string ticketKey);
    }
}
