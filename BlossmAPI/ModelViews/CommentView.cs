namespace BlossmAPI.ModelViews
{
    public class CommentView
    {
        public int id { get; set; }
        public string? id_user { get; set; }
        public int? id_product { get; set; }
        public DateTime? created_date { get; set; }
        public string? content { get; set; }
    }
}
