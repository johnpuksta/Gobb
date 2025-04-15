using Gobb.Clients;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

class JiraClient
{
    private readonly HttpClient _httpClient;

    public JiraClient(string baseUrl, string email, string apiToken)
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri(baseUrl)
        };

        var authString = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{email}:{apiToken}"));

        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(baseUrl);
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authString);
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<JiraIssue> GetIssueAsync(string issueKey)
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"/rest/api/3/issue/{issueKey}");
        if (response.IsSuccessStatusCode)
        {
            string json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            return JsonSerializer.Deserialize<JiraIssue>(json, options);
        }
        else
        {
            throw new Exception($"Failed to retrieve issue: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
        }
    }
}