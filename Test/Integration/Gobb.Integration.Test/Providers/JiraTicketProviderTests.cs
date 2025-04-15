using Gobb.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using MsOptions = Microsoft.Extensions.Options.Options;

namespace Gobb.Integration.Test.Providers
{
    [TestFixture]
    public class JiraTicketProviderTests
    {
        private IOptions<JiraClientOptions> options;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var settingsSection = config.GetSection("JiraClientOptions");
            options = MsOptions.Create(settingsSection.Get<JiraClientOptions>() ?? throw new InvalidDataException("Jira Client Options not found in appsettings"));
        }

        [Test]
        public async Task GetTicketSummaryAndDescriptionAsync_WithValidTicketKey_ReturnsExpected()
        {
            var testTicket = "GOBB-1";
            var jiraClientLogger = new Mock<ILogger<JiraClient>>();
            var jiraClient = new JiraClient(jiraClientLogger.Object, options);

            var jiraTicketProviderLogger = new Mock<ILogger<JiraTicketProvider>>();
            var jiraTicketProvider = new JiraTicketProvider(jiraTicketProviderLogger.Object, jiraClient);
            
            var result = await jiraTicketProvider.GetTicketSummaryAndDescriptionAsync(testTicket);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Summary, Is.Not.Null);
                Assert.That(result.Description, Is.Not.Null);
            });
        }
    }
}