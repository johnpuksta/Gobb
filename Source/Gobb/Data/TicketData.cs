namespace Gobb.Data
{
    public class TicketData: ITicketData
    {
        public TicketData(string summary, string description)
        {
            Summary = summary;
            Description = description;
        }

        public string Summary { get; init; }
        public string Description { get; init; }
    }
}
