using BlossmAPI.Models;

namespace BlossmMudBlazor.DTO
{
    public class CategoryWaterfall
    {
        public Category parent { get; set; }
        public List<Category> childs { get; set; } = new List<Category>();
    }
}
