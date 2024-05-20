using BlossmAPI.Models;
using BlossmAPI.ModelViews;

namespace BlossmAPI.Repositories.Interfaces
{
    public interface IUnitServices
    {
        public Task<IEnumerable<UnitView>> GetAll();
        public Task<bool> Create(UnitView new_unit);
        public Task<bool> Update(UnitView updated_unit);
        public Task<bool> Delete(int id);
    }
}
