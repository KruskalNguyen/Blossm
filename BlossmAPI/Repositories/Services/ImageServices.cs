using BlossmAPI.Models;
using BlossmAPI.ModelViews;
using BlossmAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlossmAPI.Repositories.Services
{
    public class ImageServices : IImageServices
    {
        private readonly BlossmContext _context;
        public ImageServices(BlossmContext context)
        {
            _context = context;
        }
        public IEnumerable<Image> GetImagesByVariantId(int id)
        {
            var rs = _context.Images.Where(i => i.IdProductVariant == id);
            if(rs != null) { return rs; } else { return null; }
        }
        public async Task<bool> AddImage(ImageVIew new_images)
        {
            List<Image> images = new List<Image>();
            foreach(var image in new_images.listimage)
            {
                Image img = new Image();
                img.Images = image;
                img.Avatar = false;
                img.IdProductVariant = new_images.variant_id;
                images.Add(img);
            }
            if(images != null)
            {
                foreach(var image in images)
                {
                    try
                    {
                        _context.Images.Add(image);
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                return true;
            }
            return false;
        }
        public async Task<bool> UpdateImage(ImageVIew new_images)
        {
            if(new_images.image_id != 0)
            {
                var image = await _context.Images.FirstOrDefaultAsync(i => i.Id == new_images.image_id);
                image.Avatar = new_images.avatar;
                try
                {
                    _context.Images.Update(image);
                    await _context.SaveChangesAsync();
                    return true;
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }
            return false;
        }
        public async Task<bool> DeleteImage(int id)
        {
            var image = await _context.Images.FirstOrDefaultAsync(i => i.Id == id);
            if (image != null)
            {
                try
                {
                    _context.Images.Remove(image);
                    await _context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            return false;
        }

    }
}
