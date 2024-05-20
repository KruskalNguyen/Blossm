using BlossmAPI.Attributes;
using BlossmAPI.Models;
using BlossmAPI.ModelViews;
using BlossmAPI.Patterns.Singleton;
using BlossmAPI.Repositories.Interfaces;
using BlossmAPI.Utilities;
using Microsoft.AspNetCore.Mvc.Razor.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BlossmAPI.Repositories.Services
{
    public class BranchProductVariantService : IBranchProductVariantService
    {
        private readonly BlossmContext _context;
        public BranchProductVariantService(BlossmContext context)
        {
            _context = context;
        }
        public async Task<ApiResponse<bool>> CreateBranchProductVariant(BranchProductVariantView view)
        {
            ApiResponse<bool> response = new ApiResponse<bool>();
            try
            {
                BranchProductVariant variant = new BranchProductVariant();
                variant.IdBranch = view.IdBranch;
                variant.IdProductVariant = view.IdProductVariant;
                variant.InventoryQuantity = view.InventoryQuantity;

                _context.BranchProductVariants.Add(variant);
                await _context.SaveChangesAsync();

                return response.SuccessResult();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ApiResponse<bool>> AddMultipleBranchProductVariant(BranchProductVariantView view)
        {
            ApiResponse<bool> response = new ApiResponse<bool>();
            if (view.IdProductVariants.Count == 0)
            {
                return response.FalseResult("Please fill out the form");
            }
            foreach (var item in view.IdProductVariants)
            {
                BranchProductVariant branchProductVariant = new BranchProductVariant();
                branchProductVariant.IdBranch = view.IdBranch;
                branchProductVariant.IdProductVariant = item;
                branchProductVariant.InventoryQuantity = 1;

                _context.BranchProductVariants.Add(branchProductVariant);
                await _context.SaveChangesAsync();
            }
            return response.SuccessResult();

        }

        /*public async Task<bool> UpdateBranchProductVariant(BranchProductVariantView mVBranchProductVariant)
        {
            var branchProductVariant = _context.BranchProductVariants.FirstOrDefault(i => i.IdBranch == mVBranchProductVariant.IdBranch && i.IdProductVariant == mVBranchProductVariant.IdProductVariant);
            if(branchProductVariant != null)
            {
                branchProductVariant.InventoryQuantity = branchProductVariant.InventoryQuantity;
                try
                {
                    _context.BranchProductVariants.Update(branchProductVariant);
                    await _context.SaveChangesAsync();
                    return true;
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }
            return false;
        }*/

        public async Task<ApiResponse<bool>> UpdateBranchProductVariant(string id_user, BranchProductVariantView mVBranchProductVariant)
        {
            ApiResponse<bool> apiResponse = new ApiResponse<bool>();

            try
            {
                var checkBranch = await _context.Branches
                .FirstOrDefaultAsync(
                b => b.Id == mVBranchProductVariant.IdBranch &&
                b.Manager == id_user);

                if (checkBranch == null)
                {
                    apiResponse.Success = false;
                    apiResponse.ErrorMessage = "You ar  e not allowed to perform this action";
                    return apiResponse;
                }

                var branchProductVariant = _context.BranchProductVariants
                    .FirstOrDefault(
                    i => i.IdBranch == mVBranchProductVariant.IdBranch &&
                    i.IdProductVariant == mVBranchProductVariant.IdProductVariant);

                branchProductVariant.InventoryQuantity = branchProductVariant.InventoryQuantity;

                _context.BranchProductVariants.Update(branchProductVariant);
                await _context.SaveChangesAsync();
                apiResponse.Success = true;
                return apiResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteBranchProductVariant(int idBranch, int idProductVariant)
        {
            var branchProductVariant = _context.BranchProductVariants.FirstOrDefault(i => i.IdBranch == idBranch && i.IdProductVariant == idProductVariant);
            if (branchProductVariant != null)
            {
                try
                {
                    _context.BranchProductVariants.Remove(branchProductVariant);
                    await _context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return false;
        }


    }
}
