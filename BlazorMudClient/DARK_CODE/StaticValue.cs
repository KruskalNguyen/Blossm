using BlazorMudClient.DTO;
using BlossmAPI.Models;
using BlossmMudBlazor.DTO;

namespace BlazorMudClient.DARK_CODE
{
    public class StaticValue
    {
        public static AspNetUser AspNetUser { get; set; }
        public static int? cartNum { get; set; } = 0;
        public static string goongKey = "0mN89XYzELLo4G0Ga5CpPhVHCEi2hfTIpwTEtHvE";
        public static List<VariantDetails> variantDetails { get; set; } = new List<VariantDetails>();
        public static List<ProductSearch> productSearches { get; set; } = new List<ProductSearch>();
    }
}
