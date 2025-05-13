using Gobb.Clients;
using Gobb.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using MsOptions = Microsoft.Extensions.Options.Options;

namespace Gobb.Integration.Test.Clients
{
    [TestFixture]
    public class GitHubClientTests
    {
        private IOptions<GitHubClientOptions> options;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var settingsSection = config.GetSection("GitHubClientOptions");
            options = MsOptions.Create(settingsSection.Get<GitHubClientOptions>() ?? throw new InvalidDataException("Jira Client Options not found in appsettings"));
        }

        [Test]
        public async Task GetTicketAsync_WithValidTicketId_ReturnsExpected()
        {
            var testTicketId = "2";
            var mockLogger = new Mock<ILogger<GitHubClient>>();
            var gitHubClient = new GitHubClient(options, mockLogger.Object);

            var result = await gitHubClient.GetTicketAsync(testTicketId);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Summary, Is.Not.Null);
                Assert.That(result.Description, Is.Not.Null);
            });
        }
    }
}
