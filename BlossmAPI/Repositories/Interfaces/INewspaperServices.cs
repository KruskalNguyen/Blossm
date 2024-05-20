using BlossmAPI.Models;
using BlossmAPI.Utilities;

namespace BlossmAPI.Repositories.Interfaces
{
    public interface INewspaperServices
    {
        public Task<List<Newspaper>> GetNewspaper();
        public Task<bool> CreateNewspaper(Newspaper newspaper);
        public Task<bool> UpdateNewspaper(Newspaper newspaper);
        public Task<bool> DeleteNewspaper(int id_news);
    }
}
