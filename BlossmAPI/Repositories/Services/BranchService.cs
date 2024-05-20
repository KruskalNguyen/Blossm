using BlossmAPI.Attributes;
using BlossmAPI.Models;
using BlossmAPI.Models.Geocode;
using BlossmAPI.Models.ModelsExtention;
using BlossmAPI.ModelViews;
using BlossmAPI.Patterns.Singleton;
using BlossmAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc.Razor.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Stripe.Climate;

namespace BlossmAPI.Repositories.Services
{
    [BlossmAuthorize]
    public class BranchService : IBranchService
    {
        private readonly BlossmContext _context;
        private readonly HttpClient _httpClient;
        public BranchService(BlossmContext context, HttpClient httpClient)
        {

            _context = context;
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Branch>> GetAllBranch()
        {
            var result = await _context.Branches
                .Include(b => b.IdAreaNavigation)
                .Include(b => b.ManagerNavigation)
                .Where(b => b.Latlng != null)
                .ToListAsync();

            foreach(var branch in result)
            {
                branch.IdAreaNavigation.Branches = null;
                branch.ManagerNavigation.Branches = null;
            }

            return result;
        }
        public async Task<bool> CreateBranch(BranchView mVBranch)
        {
            //is user has permission???

            Branch branch = new Branch();
            branch.Address = mVBranch.Address;
            branch.Manager = mVBranch.Manager;
            branch.IdArea = mVBranch.IdArea;

            string apiGeoCode = "https://rsapi.goong.io/geocode" +
                    "?address=" + mVBranch.Address +
                    "&api_key=" + StaticValue.goongKey;
            Geocode geocode = await _httpClient.GetFromJsonAsync<Geocode>(apiGeoCode);

            string lat = geocode.results[0].geometry.location.lat.ToString();
            string lng = geocode.results[0].geometry.location.lng.ToString();

            string location = $"{lat},{lng}";

            branch.Latlng = location;

            try
            {
                _context.Add(branch);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> UpdateBranch(BranchView mVBranch)
        {
            var branch = await _context.Branches.FirstOrDefaultAsync(i => i.Id == mVBranch.Id);

            if(branch != null)
            {
                branch.Address = mVBranch.Address;
                branch.Manager = mVBranch.Manager;
                branch.IdArea = mVBranch.IdArea;

                string apiGeoCode = "https://rsapi.goong.io/geocode" +
                    "?address=" + mVBranch.Address +
                    "&api_key=" + StaticValue.goongKey;
                Geocode geocode = await _httpClient.GetFromJsonAsync<Geocode>(apiGeoCode);

                string lat = geocode.results[0].geometry.location.lat.ToString();
                string lng = geocode.results[0].geometry.location.lng.ToString();

                string location = $"{lat},{lng}";

                branch.Latlng = location;
                try
                {
                    _context.Branches.Update(branch); //Code cũ là Add ->> Update
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
        public async Task<bool> DeleteBranch(int id)
        {
            var branch = await _context.Branches.FirstOrDefaultAsync(i => i.Id == id);
            if (branch != null)
            {
                try
                {
                    _context.Branches.Remove(branch);
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
        public BranchView GetBranch(int id)
        {
            var branch = _context.Branches.FirstOrDefault(i => i.Id == id);
            if(branch != null )
            {
                BranchView mVBranch = new BranchView();
                mVBranch.Id = id;
                mVBranch.Address = branch.Address;
                mVBranch.IdArea = branch.IdArea;
                mVBranch.Manager = branch.Manager;
                mVBranch.Latlng = branch.Latlng;
                return mVBranch;
            }
            return new BranchView();
        }

        public async Task<int> GetQuantityPVinBranch(int idPV, int idBranch)
        {
            var branchPV = await _context.BranchProductVariants.FirstOrDefaultAsync(bpv => bpv.IdBranch == idBranch && bpv.IdProductVariant == idPV);
            if (branchPV == null)
                return 0;
            if (branchPV.InventoryQuantity == null)
                return 0;
            return (int)branchPV.InventoryQuantity;
        }
    }
}
