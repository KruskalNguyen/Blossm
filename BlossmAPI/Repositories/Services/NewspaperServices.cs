using BlossmAPI.Models;
using BlossmAPI.Repositories.Interfaces;
using BlossmAPI.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BlossmAPI.Repositories.Services
{
    public class NewspaperServices:INewspaperServices
    {
        private readonly BlossmContext _context;

        public NewspaperServices(BlossmContext context)
        {
            _context = context;
        }

        
        public async Task<List<Newspaper>> GetNewspaper()
        {
            var news = _context.Newspapers.Include(n => n.IdUserNavigation).ToList();
            foreach(var item in news)
            {
                if(item.IdUserNavigation != null)
                item.IdUserNavigation.Newspapers = null;
            }
            if(news != null)
                return news;
            return null;
        }

        public async Task<bool> CreateNewspaper(Newspaper newspaper)
        {
            try
            {
                await _context.AddAsync(newspaper);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public async Task<bool> UpdateNewspaper(Newspaper newspaper)
        {
            try
            {
                _context.Update(newspaper);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public async Task<bool> DeleteNewspaper(int id_news)
        {
            try
            {
                var rs = _context.Newspapers.FirstOrDefault(v => v.Id == id_news);
                _context.Remove(rs);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }

        }


    }
}
