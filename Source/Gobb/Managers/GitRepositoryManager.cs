using Gobb.Managers;
using Gobb.Options;
using LibGit2Sharp;
using Microsoft.Extensions.Options;

public class GitRepositoryManager: IRepositoryManager
{
    private readonly string _repositoryPath;
    private readonly string _username;
    private readonly string _email;

    public GitRepositoryManager(IOptions<GitRepositoryManagerOptions> options)
    {
        _repositoryPath = options.Value.RepositoryPath ?? throw new ArgumentNullException(nameof(options.Value.RepositoryPath));
        _username = options.Value.Username ?? throw new ArgumentNullException(nameof(options.Value.Username));
        _email = options.Value.Email ?? throw new ArgumentNullException(nameof(options.Value.Email));
    }

    public void CreateBranchAndCheckout(string branchName)
    {
        using (var repo = new Repository(_repositoryPath))
        {
            var branch = repo.CreateBranch(branchName);
            Commands.Checkout(repo, branchName);
        }
    }

    public void StageAndCommit(string commitMessage)
    {
        using (var repo = new Repository(_repositoryPath))
        {
            Commands.Stage(repo, "*");
            var author = new Signature(_username, _email, DateTime.Now);
            repo.Commit(commitMessage, author, author);
        }
    }

    public void Push(string branchName)
    {
        using (var repo = new Repository(_repositoryPath))
        {
            var remote = repo.Network.Remotes["origin"];
            var branch = repo.Branches[branchName];
            var pushOptions = new PushOptions();
            repo.Network.Push(branch, pushOptions);
        }
    }

    private string GenerateCommitMessage(string jiraTicketKey, string changeSummary)
    {
        return $"[{jiraTicketKey}] {changeSummary}";
    }

    /* octokit
    public async Task CreatePullRequestAsync(string repoOwner, string repoName, string branchName, string baseBranch, string title, string body)
    {
        var github = new GitHubClient(new ProductHeaderValue("MCPServer"))
        {
            Credentials = new Octokit.Credentials("your_github_token")
        };

        var pr = new NewPullRequest(title, branchName, baseBranch)
        {
            Body = body
        };
        await github.PullRequest.Create(repoOwner, repoName, pr);
    }
    */
}