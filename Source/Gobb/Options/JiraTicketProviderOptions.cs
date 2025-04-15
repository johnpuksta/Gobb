namespace Gobb.Options
{
    public class JiraTicketProviderOptions
    {
        public required string Url { get; set; }
        public required string Username {get; set;}
        public required string ApiToken {get; set;}
    }
}
