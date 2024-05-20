using BlossmAPI.Models;
using BlossmAPI.ModelViews;

namespace BlossmAPI.Repositories.Interfaces
{
    public interface IProductServices
    {
        //public IEnumerable<Product> GetProducts(ProductFilter _filter);
        public IEnumerable<ProductVariant> GetVariants(ProductFilterView _filter);
        public Task<IEnumerable<Product>> GetProducts();
		public Task<bool> CreateProduct(ProductView new_product);
        public Task<bool> UpdateProduct(ProductView updated_product);
    }
}
