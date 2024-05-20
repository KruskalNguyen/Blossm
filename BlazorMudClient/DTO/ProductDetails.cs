using BlossmAPI.Models;
using BlossmMudBlazor.DTO;

namespace BlazorMudClient.DTO
{
    public class ProductDetails
    {
        public string Name { get; set; }
        public VariantDetails details { get; set; } = new VariantDetails();
        public ProductVariant variant { get; set; } = new ProductVariant();
    }
}
