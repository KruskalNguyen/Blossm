using BlossmAPI.Models;
using BlossmAPI.ModelViews;

namespace BlossmAPI.Repositories.Interfaces
{
    public interface IImageServices
    {
        public IEnumerable<Image> GetImagesByVariantId(int id);
        public Task<bool> AddImage(ImageVIew new_images);
        public Task<bool> UpdateImage(ImageVIew new_images);
        public Task<bool> DeleteImage(int id);
    }
}
