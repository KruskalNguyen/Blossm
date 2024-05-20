using BlossmAPI.Models;
namespace BlazorMudClient.DTO
{
    public class CartItem
    {
        public bool IsSelected { get; set; }
        public ShoppingCart shoppingCart { get; set; } = new ShoppingCart();
    }
}
