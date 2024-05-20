using BlossmAPI.Models;
using BlossmAPI.ModelViews;
using BlossmAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BlossmAPI.Repositories.Services
{
    public class AreaService:IAreaService
    {
        private readonly BlossmContext _context;
        public AreaService(BlossmContext context)
        {
            _context = context;
        }
        public async Task<List<Area>> GetAllArea()
        {
            var rs = await _context.Areas.ToListAsync();
            return rs;
        }
        public async Task<bool> CreateArea(AreaView mVArea)
        {
            Area area = new Area();
            area.Name = mVArea.Name;
            try
            {
                _context.Areas.Add(area);
                await _context.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> UpdateArea(AreaView mVArea)
        {
            var area = await _context.Areas.FirstOrDefaultAsync(i => i.Id == mVArea.Id);
            if (area != null)
            {
                
                area.Name = mVArea.Name;
                try
                {
                    _context.Areas.Update(area);
                    await _context.SaveChangesAsync();
                    return true;
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }
            return false;
        }
        public async Task<bool> DeleteArea(int area_id)
        {
            var area = await _context.Areas.FirstOrDefaultAsync(i => i.Id == area_id);
            if (area != null)
            {
                try
                {
                    _context.Areas.Remove(area);
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
        public AreaView getArea(int id)
        {
            var area = _context.Areas.FirstOrDefault(i => i.Id == id);
            if(area != null)
            {
                AreaView mVArea = new AreaView();
                mVArea.Name = area.Name;
                mVArea.Id = area.Id;
                return mVArea;
            }
            return new AreaView();
        }
    }
}
