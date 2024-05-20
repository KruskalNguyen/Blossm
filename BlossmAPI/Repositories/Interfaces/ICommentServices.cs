using BlossmAPI.Models;
using BlossmAPI.ModelViews;
using BlossmAPI.Utilities;

namespace BlossmAPI.Repositories.Interfaces
{
    public interface ICommentServices
    {
        public Task<ApiResponse<List<Comment>>> GetAllCommentByUser(string userId);
        public Task<ApiResponse<List<Comment>>> GetAllCommentByProduct(int idProduct);
        public Task<ApiResponse<bool>> Create(CommentView view);
        public Task<ApiResponse<bool>> Update(CommentView view);
        public Task<ApiResponse<bool>> Delete(int id);
    }
}