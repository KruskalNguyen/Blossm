using BlossmAPI.ModelViews;
using BlossmAPI.Utilities;
using System.Threading.Tasks;

namespace BlossmAPI.Repositories.Interfaces
{
    public interface IBranchProductVariantService
    {
        public Task<ApiResponse<bool>> CreateBranchProductVariant(BranchProductVariantView view);
        public Task<ApiResponse<bool>> AddMultipleBranchProductVariant(BranchProductVariantView view);
        public Task<ApiResponse<bool>> UpdateBranchProductVariant(string id_user, BranchProductVariantView view);
        public Task<bool> DeleteBranchProductVariant(int idBranch, int idProductVariant);
    }
}
