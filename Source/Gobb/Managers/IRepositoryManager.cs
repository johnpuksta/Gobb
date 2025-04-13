using LibGit2Sharp;

namespace Gobb.Managers
{
    public interface IRepositoryManager
    {
        public void CreateBranchAndCheckout(string branchName);

        public void StageAndCommit(string commitMessage);

        public void Push(string branchName);
    }
}
