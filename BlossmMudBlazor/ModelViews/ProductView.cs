
namespace BlossmAPI.ModelViews
{
    public class ProductView
    {
        public int product_id { get; set; }
        public string product_name { get; set; } = "";
        public int? cate_id { get; set; }
        public string? short_des { get; set; }
        public string? main_des { get; set; }
        public int variant_id { get; set; }
        public int? unit_id { get; set; }
        public int? color_id { get; set; }
        public int? size_id { get; set; }
        public int? product_purchase_price { get; set; }
        public int? product_selling_price { get; set; }
        public bool? publish { get; set; }
        public List<string> Images { get; set; }
    }
}
