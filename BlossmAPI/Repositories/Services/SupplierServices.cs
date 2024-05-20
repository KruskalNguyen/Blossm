using BlossmAPI.Models;
using BlossmAPI.ModelViews;
using BlossmAPI.Repositories.Interfaces;
using BlossmAPI.Utilities;
using Microsoft.EntityFrameworkCore;

namespace BlossmAPI.Repositories.Services
{
    public class SupplierServices:ISupplierServices
    {
        private readonly BlossmContext _context;

        public SupplierServices(BlossmContext context)
        {
            _context = context;
        }

        public async Task<bool> AddSupplier(SupplierView mVSupplier)
        {
            Supplier supplier = new Supplier();

            supplier.Name = mVSupplier.Name;
            supplier.Address = mVSupplier.Address;
            supplier.Note = mVSupplier.Note;
            supplier.Rating = mVSupplier.Rating;

            try
            {
                await _context.Suppliers.AddAsync(supplier);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ApiResponse<object>> GetSupWithPVFromUser(string id_user)
        {
            ApiResponse<object> apiResponse = new ApiResponse<object>();
            try
            {
                var user = await _context.Employees.FirstOrDefaultAsync(u => u.IdUser == id_user);

                var listIdSupplier = await _context.TradeAgreements
                    .Where(b => b.IdBranch == user.IdBranch)
                    .Select(id => id.IdSupplier)
                    .ToListAsync();

                var listSupplier = await _context.Suppliers
                    .Include(s => s.IdProductVariants)
                    .ThenInclude(s => s.IdColorNavigation)
                    .Include(s => s.IdProductVariants)
                    .ThenInclude(s => s.IdProductNavigation)
                    .Include(s => s.IdProductVariants)
                    .ThenInclude(s => s.IdSizeNavigation)
                    .Include(s => s.IdProductVariants)
                    .ThenInclude(s => s.IdUnitNavigation)
                    .Include(s => s.IdProductVariants)
                    .ThenInclude(s => s.BranchProductVariants.Where(b => b.IdBranch == user.IdBranch))
                    .Where(b => listIdSupplier.Contains(b.Id))
                    .Select(b => new
                    {
                        IdSup = b.Id,
                        NameSup = b.Name,
                        b.Rating,
                        ListPV = b.IdProductVariants.Select(p => new
                        {
                            IdPV = p.Id,
                            NameProduct = p.IdProductNavigation.Name,
                            Color = p.IdColorNavigation.Name,
                            Unit = p.IdUnitNavigation.Name,
                            Size = p.IdSizeNavigation.Name,
                            Quantity = p.BranchProductVariants.Select(br => new {
                                br.InventoryQuantity,
                                br.IdBranch
                            }).FirstOrDefault(br => br.IdBranch == user.IdBranch).InventoryQuantity ?? 0
                        })
                    })  
                    .ToListAsync();

                return apiResponse.SuccessResult(listSupplier);
            }
            catch(Exception ex)
            {
                return apiResponse.FalseResult(ex.Message);
            }
        }
    }
}
