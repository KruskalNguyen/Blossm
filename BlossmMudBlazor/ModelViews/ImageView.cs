namespace BlossmAPI.ModelViews
{
    public class ImageVIew
    {
        public int image_id { get; set; }
        public bool? avatar { get; set; }
        public List<string> listimage { get; set; } = new List<string>();
        public int variant_id { get; set; }
    }
}
