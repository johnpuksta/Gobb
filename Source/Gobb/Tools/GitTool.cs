using Gobb.Managers;
using ModelContextProtocol.Server;
using System.ComponentModel;

namespace Gobb.Tools
{
    [McpServerToolType]
    public class GitTool
    {
        private readonly IRepositoryManager _repositoryManager;

        public GitTool(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager ?? throw new ArgumentNullException(nameof(repositoryManager));
        }

        [McpServerTool, Description("Creates a git branch if not already existant and performs a checkout to that branch.")]
        public void CreateBranchAndCheckout(string branchName)
        {
            _repositoryManager.CreateBranchAndCheckout(branchName);
        }

        [McpServerTool, Description("Stages all changes and creates a commit with a given message.")]
        public void StageAndCommit(string commitMessage)
        {
            _repositoryManager.StageAndCommit(commitMessage);
        }

        [McpServerTool, Description("Pushes commmits on a given branch to origin.")]
        public void Push(string branchName)
        {
            _repositoryManager.Push(branchName);
        }
    }
}
