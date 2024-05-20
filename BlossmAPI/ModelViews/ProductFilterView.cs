namespace BlossmAPI.ModelViews
{
    public class ProductFilterView
    {
        public string name { get; set; }
        public int cate_id { get; set; }
        public int size_id { get; set; }
        public int unit_id { get; set; }
        public int color_id { get; set; }
        public int page_num { get; set; } = 1;      //page number 1
        public int page_size { get; set; } = 10;    //10 products/page
    }
}
