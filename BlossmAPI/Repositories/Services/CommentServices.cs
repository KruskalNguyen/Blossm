using BlossmAPI.Models;
using BlossmAPI.ModelViews;
using BlossmAPI.Repositories.Interfaces;
using BlossmAPI.Utilities;
using Microsoft.EntityFrameworkCore;

namespace BlossmAPI.Repositories.Services
{
    public class CommentServices : ICommentServices
    {
        private readonly BlossmContext _context;
        public CommentServices(BlossmContext context)
        {
            _context = context;
        }
        public async Task<ApiResponse<List<Comment>>> GetAllCommentByUser(string userId)
        {
            var rs = await _context.Comments
                .Include(c => c.IdProductNavigation)
                .Include(c => c.IdUserNavigation)
                .Where(c => c.IdUser == userId)
                .ToListAsync();

            if(rs.Count() > 0)
            {
                foreach(var comment in rs)
                {
                    comment.IdUserNavigation.Comments = null;
                    comment.IdProductNavigation.Comments = null;
                }

                ApiResponse<List<Comment>> response = new ApiResponse<List<Comment>>();
                response.Data = rs;
                response.Success = true;

                return response;
            }
            else
            {
                ApiResponse<List<Comment>> response = new ApiResponse<List<Comment>>();
                response.Data = new List<Comment>();
                response.Success = true;

                return response;
            }
        }
        public async Task<ApiResponse<List<Comment>>> GetAllCommentByProduct(int idProduct)
        {
            var rs = await _context.Comments
                .Include(c => c.IdUserNavigation)
                .Where(c => c.IdProduct == idProduct)
                .ToListAsync();

            if(rs.Count > 0)
            {
                ApiResponse<List<Comment>> response = new ApiResponse<List<Comment>>();
                foreach (var comment in rs)
                {
                    comment.IdUserNavigation.Comments = null;
                }
                response.Success = true;
                response.Data = rs;

                return response;
            }
            else
            {
                ApiResponse<List<Comment>> response = new ApiResponse<List<Comment>>();
                response.Success = true;
                response.Data = new List<Comment>();

                return response;
            }
            
        }
        public async Task<ApiResponse<bool>> Create(CommentView view)
        {
            Comment comment = new Comment();
            comment.CreateDate = DateTime.Now;
            comment.IdProduct = view.id_product;
            comment.IdUser = view.id_user;
            comment.Content = view.content;

            try
            {
                await _context.Comments.AddAsync(comment);
                await _context.SaveChangesAsync();

                ApiResponse<bool> response = new ApiResponse<bool>();
                response.Success = true;

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ApiResponse<bool>> Update(CommentView view)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == view.id);
            comment.Content = view.content;

            try
            {
                _context.Comments.Update(comment);
                await _context.SaveChangesAsync();

                ApiResponse<bool> response = new ApiResponse<bool>();
                response.Success = true;

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ApiResponse<bool>> Delete(int id)
        {
            var comment = await _context.Comments.Where(c => c.Id == id).FirstOrDefaultAsync();

            try
            {
                _context.Comments.Remove(comment);
                await _context.SaveChangesAsync();

                ApiResponse<bool> response = new ApiResponse<bool>();
                response.Success = true;

                return response;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
