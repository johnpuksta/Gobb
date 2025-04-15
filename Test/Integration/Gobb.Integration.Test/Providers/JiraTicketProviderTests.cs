using Gobb.Options;
using System.Text.Json;

namespace Gobb.Integration.Test.Providers
{
    [TestFixture]
    public class JiraTicketProviderTests
    {
        private JiraTicketProviderOptions options;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var json = File.ReadAllText("appsettings.json");
            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;
            var optionsSection = root.GetProperty("JiraContextProviderSettings");
            options = optionsSection.Deserialize<JiraTicketProviderOptions>() ?? throw new InvalidDataException("appsettings could not be deserialized");
        }

        [Test]
        public async Task GetTicketSummaryAndDescriptionAsync_WithValidTicketKey_ReturnsExpected()
        {
            var testTicket = "GOBB-1";
            var jiraTicketProvider = new JiraTicketProvider(options.Url, options.Username, options.ApiToken);
            
            var result = await jiraTicketProvider.GetTicketSummaryAndDescriptionAsync(testTicket);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Summary, Is.Not.Null);
            Assert.That(result.Description, Is.Not.Null);
        }
    }
}