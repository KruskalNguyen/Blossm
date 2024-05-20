using BlossmAPI.Models;
using BlossmAPI.ModelViews;

namespace BlossmAPI.Repositories.Interfaces
{
    public interface IProductVariantServices
    {
        public Task<IEnumerable<ProductVariant>> GetVariantsByProductId(int id);
        public Task<ProductVariant> GetProductVariantById(int id);
        public Task<bool> CreateVariant(ProductView new_variant);
        public Task<bool> UpdateVariant(ProductView updated_variant);
    }
}
