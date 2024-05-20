using BlossmAPI.Models;
using BlossmAPI.ModelViews;
using BlossmAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BlossmAPI.Repositories.Services
{
    public class ColorServices: IColorServices
    {
        private readonly BlossmContext _context;
        public ColorServices(BlossmContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ColorView>> GetAll()
        {
            try
            {
                var colors = await _context.Colors.ToListAsync();

                List<ColorView> views = new List<ColorView>();
                foreach (var color in colors)
                {
                    ColorView view = new ColorView();
                    view.id = color.Id;
                    view.name = color.Name;
                    view.hex = color.Hex;
                    views.Add(view);
                }

                return views;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> Create(ColorView new_color)
        {
            Color color = new Color();
            color.Name = new_color.name;
            color.Hex = new_color.hex;
            try
            {
                _context.Colors.Add(color);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> Update(ColorView updated_color)
        {
            if (updated_color.id == 0)
            {
                return false;
            }

            var color = await _context.Colors.FirstOrDefaultAsync(u => u.Id == updated_color.id);
            try
            {
                color.Name = updated_color.name;
                color.Hex = updated_color.hex;
                _context.Colors.Update(color);
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
            var color = await _context.Colors
                .FirstOrDefaultAsync(u => u.Id == id);
            if (color != null)
            {
                try
                {
                    _context.Colors.Remove(color);
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