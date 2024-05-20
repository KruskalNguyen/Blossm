using BlossmAPI.Models;
using BlossmAPI.Models.ModelsExtention;
using BlossmAPI.ModelViews;
using BlossmAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlossmAPI.Repositories.Services
{
    public class CartServices : ICartServices
    {
        private readonly BlossmContext _context;

        public static int MAX_QUANTITY = 15;
        public static int MIN_QUANTITY = 1;

        public CartServices(BlossmContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ShoppingCart>> GetShoppingCartByUserId(string id)
        {
            var carts = await _context.ShoppingCarts
                                .Include(c => c.IdUserNavigation)
                                .Include(c => c.IdProductVariantNavigation)
                                .ThenInclude(v => v.IdProductNavigation)
                                .Include(c => c.IdProductVariantNavigation)
                                .ThenInclude(v => v.IdUnitNavigation)
                                .Include(c => c.IdProductVariantNavigation)
                                .ThenInclude(v => v.IdColorNavigation)
                                .Include(c => c.IdProductVariantNavigation)
                                .ThenInclude(v => v.IdSizeNavigation)
                                .Where(c => c.IdUserNavigation.Id == id)
                                .ToListAsync();
            foreach(var cart in carts)
            {
                cart.IdUserNavigation = null;
                cart.IdProductVariantNavigation.ShoppingCarts = null;
                cart.IdProductVariantNavigation.IdProductNavigation.ProductVariants = null;
                cart.IdProductVariantNavigation.IdColorNavigation.ProductVariants = null;
                cart.IdProductVariantNavigation.IdUnitNavigation.ProductVariants = null;
                cart.IdProductVariantNavigation.IdSizeNavigation.ProductVariants = null;
            }

            return carts;
        }
        public async Task<bool> AddVariantToCart(string id, CartView view)
        {
            ShoppingCart cart = AutoMapper.shoppingCart(id, view);
            if (cart != null)
            {
                try
                {
                    _context.ShoppingCarts.Add(cart);
                    await _context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    throw ex.InnerException;
                }
            }
            return false;
        }
        public async Task<bool> AdjustVariantQuantity(string id_user, CartView view)
        {
            ShoppingCart cartItem = await _context.ShoppingCarts
                .FirstOrDefaultAsync(c => c.IdUser == id_user
                                && c.IdProductVariant == view.id_variant);
            if (view.quantity > MAX_QUANTITY || view.quantity < MIN_QUANTITY)
            {
                throw new Exception("Invalid quantity");
            }

            try
            {
                if (cartItem != null)
                {
                    cartItem.Quantity = view.quantity;
                    _context.ShoppingCarts.Update(cartItem);
                    await _context.SaveChangesAsync();
                    return true;
                }
                throw new Exception("Error");
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public async Task<bool> IncreaseVariant(string id_user, CartView view)
        {
            ShoppingCart cartItem = await _context.ShoppingCarts
                .FirstOrDefaultAsync(c => c.IdUser == id_user
                                && c.IdProductVariant == view.id_variant);
            if(cartItem != null)
            {
                try
                {
                    cartItem.Quantity++;
                    if(cartItem.Quantity > MAX_QUANTITY || cartItem.Quantity < MIN_QUANTITY)
                    {
                        throw new Exception("Invalid quantity");
                    }
                    _context.ShoppingCarts.Update(cartItem);
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
        public async Task<bool> DecreaseVariant(string id_user, CartView view)
        {
            ShoppingCart cartItem = await _context.ShoppingCarts
                .FirstOrDefaultAsync(c => c.IdUser == id_user
                                && c.IdProductVariant == view.id_variant);
            if(cartItem != null)
            {
                try
                {
                    cartItem.Quantity--;
                    if (cartItem.Quantity > MAX_QUANTITY || cartItem.Quantity < MIN_QUANTITY)
                    {
                        throw new Exception("Invalid quantity");
                    }
                    _context.ShoppingCarts.Update(cartItem);
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
        public async Task<bool> RemoveVariant(string id_user, int id_variant)
        {
            ShoppingCart cartItem = await _context.ShoppingCarts
                .FirstOrDefaultAsync(c => c.IdUser == id_user
                                && c.IdProductVariant == id_variant);
            if (cartItem != null)
            {
                try
                {
                    _context.ShoppingCarts.Remove(cartItem);
                    await _context.SaveChangesAsync();
                    return true;
                }
                catch(Exception ex)
                {
                    throw ex.InnerException;
                }
            }
            return false;
        }
    }
}
