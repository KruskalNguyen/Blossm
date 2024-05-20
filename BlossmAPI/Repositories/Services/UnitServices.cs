using BlossmAPI.Models;
using BlossmAPI.ModelViews;
using BlossmAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlossmAPI.Repositories.Services
{
    public class UnitServices: IUnitServices
    {
        private readonly BlossmContext _context;
        public UnitServices(BlossmContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<UnitView>> GetAll()
        {
            try
            {
                var units = await _context.Units.ToListAsync();
                List<UnitView> result = new List<UnitView>();
                foreach (var unit in units)
                {
                    UnitView view = new UnitView();
                    view.id = unit.Id;
                    view.name = unit.Name;
                    result.Add(view);
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> Create(UnitView new_unit)
        {
            if (new_unit == null)
            {
                return false;
            }
            try
            {
                Unit unit = new Unit();
                unit.Name = new_unit.name;

                _context.Units.Add(unit);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> Update(UnitView updated_unit)
        {
            if(updated_unit.id == 0)
            {
                return false;
            }
            var unit = await _context.Units.FirstOrDefaultAsync(u => u.Id == updated_unit.id);
            try
            {
                unit.Name = updated_unit.name;
                _context.Units.Update(unit);
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
            var unit = _context.Units.FirstOrDefault(u => u.Id == id);
            if(unit != null)
            {
                try
                {
                    _context.Units.Remove(unit);
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
