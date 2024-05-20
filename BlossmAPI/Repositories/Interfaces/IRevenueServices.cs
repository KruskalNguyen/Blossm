using BlossmAPI.Models;
using BlossmAPI.ModelViews;

namespace BlossmAPI.Repositories.Interfaces
{
    public interface IRevenueServices
    {
        public Task<int> GetTodayOrder(int idBranch);
        public Task<int> GetTodayPR(int idBranch);
        public Task<int?> GetTodayPOValue(int idBranch);
        public Task<int?> GetTodaySOValue(int idBranch);
        public Task<int?> GetPOValueByDay(int idBranch, int year, int month, int day);
        public Task<int?> GetSOValueByDay(int idBranch, int year, int month, int day);
        public Task<int?> GetTodaySoldProduct(int idBranch);
        public Task<IEnumerable<AspNetUser>> GetTop10User();
        public Task<IEnumerable<ProductRankingView>> GetTopSellingProduct(int month, int idBranch);
        public Task<IEnumerable<ProductRankingView>> GetTopBenefitProduct(int month, int idBranch);
        public Task<int?> GetRevenueByMonth(int idBranch, int month);
        public Task<int?> GetRevenueByYear(int idBranch, int year);
    }
}
