using Gobb.Clients;
using System.Net.Http.Headers;

namespace Gobb.Integration.Test.Clients
{
    [TestFixture]
    public class GitHubClientTests
    {
        [Test]
        public async Task GetTicketSummaryAndDescriptionAsync_WithValidTicketId_ReturnsExpected()
        {
            var testTicketId = "2";
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("GobbApp", "1.0"));
            var gitHubClient = new GitHubClient(httpClient, "johnpuksta", "gobb");

            var result = await gitHubClient.GetTicketSummaryAndDescriptionAsync(testTicketId);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Summary, Is.Not.Null);
                Assert.That(result.Description, Is.Not.Null);
            });
        }
    }
}
