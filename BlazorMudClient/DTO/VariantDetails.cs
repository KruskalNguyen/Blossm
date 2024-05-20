using BlossmAPI.Models;

namespace BlossmMudBlazor.DTO
{
	public class VariantDetails
	{
		public string id { get; set; }
		public bool ShowDetails { get; set; }
		public Product product { get; set; }
		public List<ProductVariant> variant { get; set; } = new List<ProductVariant>();
        public List<string> allImg { get; set; } = new List<string>();
    }
}
