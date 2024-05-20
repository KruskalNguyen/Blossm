using BlossmAPI.Models;
using BlossmAPI.ModelViews;

namespace BlossmAPI.Repositories.Interfaces
{
    public interface IBranchService
    {
        public Task<IEnumerable<Branch>> GetAllBranch();
        public Task<bool> CreateBranch(BranchView mVBranch);
        public Task<bool> UpdateBranch(BranchView mVBranch);
        public Task<bool> DeleteBranch(int id);
        public Task<int> GetQuantityPVinBranch(int idPV, int idBranch);
    }
}
