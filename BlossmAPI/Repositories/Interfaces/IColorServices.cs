using BlossmAPI.Models;
using BlossmAPI.ModelViews;

namespace BlossmAPI.Repositories.Interfaces
{
    public interface IColorServices
    {
        public Task<IEnumerable<ColorView>> GetAll();
        public Task<bool> Create(ColorView new_color);
        public Task<bool> Update(ColorView updated_color);
        public Task<bool> Delete(int id);
    }
}