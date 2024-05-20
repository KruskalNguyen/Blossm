using BlossmAPI.Models;
using BlossmAPI.ModelViews;

namespace BlossmAPI.Repositories.Interfaces
{
    public interface ISizeServices
    {
        public Task<IEnumerable<SizeView>> GetAll();
        public Task<bool> Create(SizeView new_size);
        public Task<bool> Update(SizeView updated_size);
        public Task<bool> Delete(int id);
    }
}