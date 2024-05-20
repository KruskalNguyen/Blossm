using BlossmAPI.Models;
using BlossmAPI.ModelViews;
using BlossmAPI.Repositories.Interfaces;
using BlossmAPI.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace BlossmAPI.Repositories.Services
{
    public class VoucherServices : IVoucherServices
    {
        private readonly BlossmContext _context;
        public VoucherServices(BlossmContext context)
        {
            _context = context;
        }
        public async Task<ApiResponse<bool>> Create(VoucherView view)
        {
            try
            {
                Voucher voucher = new Voucher();
                voucher.Id = view.id;
                voucher.Name = view.name.ToUpper();
                voucher.CreateDate = DateTime.Now;
                voucher.StartDate = view.startDate;
                voucher.EndDate = view.endDate;
                voucher.DiscountAmount = view.amount;
                voucher.DiscountPercentage = view.isPercentage;
                voucher.Active = false;
                voucher.Limit = view.limit;
                voucher.Condition = view.condition;

                await _context.Vouchers.AddAsync(voucher);
                await _context.SaveChangesAsync();

                ApiResponse<bool> response = new ApiResponse<bool>();
                response.Success = true;
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<ApiResponse<bool>> AddToUser(string idUser, string idVoucher)
        {
            var user = await _context.AspNetUsers.FirstOrDefaultAsync(u => u.Id == idUser);
            var voucher = await _context.Vouchers.FirstOrDefaultAsync(v => v.Id == idVoucher);
            try
            {
                user.IdVouchers.Add(voucher);

                _context.AspNetUsers.Update(user);
                await _context.SaveChangesAsync();

                ApiResponse<bool> response = new ApiResponse<bool>();
                response.Success = true;

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ApiResponse<bool>> RemoveFromUser(string idUser, string idVoucher)
        {
            var user = await _context.AspNetUsers
                .Include(u => u.IdVouchers)
                .FirstOrDefaultAsync(u => u.Id == idUser);
            var voucher = await _context.Vouchers.FirstOrDefaultAsync(v => v.Id == idVoucher);
            try
            {
                user.IdVouchers.Remove(voucher);

                _context.AspNetUsers.Update(user);
                await _context.SaveChangesAsync();

                ApiResponse<bool> response = new ApiResponse<bool>();
                response.Success = true;

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ApiResponse<bool>> Update(VoucherView view)
        {
            try
            {
                var voucher = await _context.Vouchers.FirstOrDefaultAsync(v => v.Id == view.id);
                if(voucher != null)
                {
                    voucher.Id = view.id;
                    voucher.Name = view.name.ToUpper();
                    voucher.StartDate = view.startDate;
                    voucher.EndDate = view.endDate;
                    voucher.DiscountAmount = view.amount;
                    voucher.DiscountPercentage = view.isPercentage;
                    voucher.Active = view.active;
                    voucher.Limit = view.limit;
                    voucher.Condition = view.condition;

                    _context.Vouchers.Update(voucher);
                    await _context.SaveChangesAsync();
                }

                ApiResponse<bool> response = new ApiResponse<bool>();
                response.Success = true;
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<ApiResponse<List<Voucher>>> GetAll()
        {
            var rs = await _context.Vouchers.ToListAsync();

            ApiResponse<List<Voucher>> response = new ApiResponse<List<Voucher>>();
            response.Success = true;
            response.Data = rs;
            return response;
        }
        public async Task<ApiResponse<List<Voucher>>> GetVoucherByUserId(string id)
        {
            var rs = (List<Voucher>)_context.AspNetUsers
                            .Include(u => u.IdVouchers)
                            .FirstOrDefaultAsync(u => u.Id == id).Result.IdVouchers;

            foreach (var voucher in rs)
            {
                voucher.IdUsers = null;
            }

            ApiResponse<List<Voucher>> response = new ApiResponse<List<Voucher>>();
            response.Success = true;
            response.Data = rs;
            return response;
        }
        public async Task<ApiResponse<bool>> Remove(string id)
        {
            try
            {
                var voucher = await _context.Vouchers.FirstOrDefaultAsync(v => v.Id == id);
                _context.Vouchers.Remove(voucher);
                await _context.SaveChangesAsync();

                ApiResponse<bool> response = new ApiResponse<bool>();
                response.Success = true;

                return response;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        } 
    }
}
