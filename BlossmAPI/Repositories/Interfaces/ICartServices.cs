using BlossmAPI.Models;
using BlossmAPI.ModelViews;

namespace BlossmAPI.Repositories.Interfaces
{
    public interface ICartServices
    {
        public Task<IEnumerable<ShoppingCart>> GetShoppingCartByUserId(string id);
        public Task<bool> AddVariantToCart(string id, CartView view);
        public Task<bool> AdjustVariantQuantity(string id_user, CartView view);
        public Task<bool> IncreaseVariant(string id_user, CartView view);
        public Task<bool> DecreaseVariant(string id_user, CartView view);
        public Task<bool> RemoveVariant(string id_user, int id_variant);
    }
}
