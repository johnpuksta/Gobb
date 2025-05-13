using Gobb.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using MsOptions = Microsoft.Extensions.Options.Options;

namespace Gobb.Integration.Test.Clients
{
    [TestFixture]
    public class JiraClientTests
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
        public async Task GetTicketAsync_WithValidTicketKey_ReturnsExpected()
        {
            var testTicket = "GOBB-1";
            var mockLogger = new Mock<ILogger<JiraClient>>();
            var jiraClient = new JiraClient(mockLogger.Object, options);

            var result = await jiraClient.GetTicketAsync(testTicket);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Summary, Is.Not.Null);
                Assert.That(result.Description, Is.Not.Null);
            });
        }
    }
}