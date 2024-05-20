using BlossmAPI.Models;
using BlossmAPI.ModelViews;

namespace BlossmAPI.Repositories.Interfaces
{
    public interface IAreaService
    {
        public Task<List<Area>> GetAllArea();
        public Task<bool> CreateArea(AreaView mVarea);
        public Task<bool> UpdateArea(AreaView mVarea);
        public Task<bool> DeleteArea(int area_id);
    }
}
