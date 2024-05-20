using BlossmAPI.Models;
using BlossmAPI.ModelViews;
using BlossmAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlossmAPI.Repositories.Services
{
    public class SizeServices : ISizeServices
    {
        private readonly BlossmContext _context;
        public SizeServices(BlossmContext context)
        {
            this._context = context;
        }
        public async Task<IEnumerable<SizeView>> GetAll()
        {
            try
            {
                var sizes = await _context.Sizes.ToListAsync();
                List<SizeView> result = new List<SizeView>();
                foreach (var size in sizes)
                {
                    SizeView rs = new SizeView();
                    rs.id = size.Id;
                    rs.name = size.Name;
                    result.Add(rs);
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> Create(SizeView new_size)
        {
            if (new_size != null)
            {
                try
                {
                    Size size = new Size();
                    size.Name = new_size.name;

                    _context.Sizes.Add(size);
                    await _context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> Update(SizeView updated_size)
        {
            if (updated_size.id == 0)
            {
                return false;
            }
            var size = await _context.Sizes.FirstOrDefaultAsync(u => u.Id == updated_size.id);
            try
            {
                size.Name = updated_size.name;
                _context.Sizes.Update(size);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> Delete(int id)
        {
            var size = _context.Sizes.FirstOrDefault(u => u.Id == id);
            if (size != null)
            {
                try
                {
                    _context.Sizes.Remove(size);
                    await _context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                return false;
            }
        }
    }
}