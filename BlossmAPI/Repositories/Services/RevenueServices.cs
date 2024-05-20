using BlossmAPI.Models;
using BlossmAPI.Models.ModelsExtention;
using BlossmAPI.ModelViews;
using BlossmAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;

namespace BlossmAPI.Repositories.Services
{
    public class RevenueServices : IRevenueServices
    {
        private readonly BlossmContext _context;
        public RevenueServices(BlossmContext context)
        {
            _context = context;
        }
        public async Task<int?> GetRevenueByMonth(int idBranch, int month)
        {
            //tổng giá trị của các Purchase Orders đã thanh toán trong tháng input
            var SumPO = await _context.PurchaseOrders
                .Include(po => po.IdPurchaseRequestNavigation)
                .Where(po => po.IdPurchaseRequestNavigation.IdBranch == idBranch
                    && (po.OrderStatus == OrderStatusConst.RECIEVED
                    || po.OrderStatus == OrderStatusConst.Paid)
                    && po.OrderDate.Value.Month == month)
                .SumAsync(po => (po.PaymentAmount != null) ? po.PaymentAmount : po.IdPurchaseRequestNavigation.TotalAmount);
            //tổng giá trị của Orders đã thanh toán trong tháng input
            var SumOrder = await _context.Orders
                .Where(o => o.IdBranch == idBranch
                && (o.IdStatus == OrderStatusConst.RECIEVED
                || o.IdStatus == OrderStatusConst.Paid)
                && o.CreateDate.Value.Month == month)
                .SumAsync(o => o.Subtotal);
            //lấy orders - purchase orders sẽ ra được tổng thu nhập của chi nhánh trong tháng input
            var result = SumOrder - SumPO;
            //return kết quả
            return result;
        }
        public async Task<int?> GetRevenueByYear(int idBranch, int year)
        {
            //tổng giá trị của các Purchase Orders đã thanh toán trong năm input
            var SumPO = await _context.PurchaseOrders
                .Include(po => po.IdPurchaseRequestNavigation)
                .Where(po => po.IdPurchaseRequestNavigation.IdBranch == idBranch
                    && (po.OrderStatus == OrderStatusConst.RECIEVED
                    || po.OrderStatus == OrderStatusConst.Paid)
                    && po.OrderDate.Value.Year == year)
                .SumAsync(po => (po.PaymentAmount != null) ? po.PaymentAmount : po.IdPurchaseRequestNavigation.TotalAmount);
            //tổng giá trị của Orders đã thanh toán trong năm input
            var SumOrder = await _context.Orders
                .Where(o => o.IdBranch == idBranch
                && (o.IdStatus == OrderStatusConst.RECIEVED
                || o.IdStatus == OrderStatusConst.Paid)
                && o.CreateDate.Value.Year == year)
                .SumAsync(o => o.Subtotal);
            //lấy orders - purchase orders sẽ ra được tổng thu nhập của chi nhánh trong năm input
            var result = SumOrder - SumPO;
            //return kết quả
            return result;
        }
        public async Task<int> GetTodayOrder(int idBranch)
        {
            var data = await _context.Orders
                .Where(o => o.IdBranch == idBranch
                && o.CreateDate.Value.Date == DateTime.Now.Date)
                .CountAsync();
            return data;
        }
        public async Task<int> GetTodayPR(int idBranch)
        {
            var data = await _context.PurchaseRequests
                .Include(pr => pr.IdBranchNavigation)
                .Where(pr => pr.IdBranchNavigation.Id == idBranch
                && pr.CreateDate.Value.Date == DateTime.Now.Date)
                .CountAsync();
            return data;
        }
        public async Task<IEnumerable<ProductRankingView>> GetTopSellingProduct(int month, int idBranch)
        {
            #region query data
            var data = await _context.OrderItems
                .Include(o => o.IdOrderNavigation)
                .Include(o => o.IdProductVariantNavigation)
                    .ThenInclude(pv => pv.IdColorNavigation)
                .Include(o => o.IdProductVariantNavigation)
                    .ThenInclude(pv => pv.IdSizeNavigation)
                .Include(o => o.IdProductVariantNavigation)
                    .ThenInclude(pv => pv.IdUnitNavigation)
                .Include(o => o.IdProductVariantNavigation)
                    .ThenInclude(pv => pv.IdProductNavigation)
                .Where(o => o.IdOrderNavigation.IdBranch == idBranch
                && o.IdOrderNavigation.CreateDate.Value.Month == month
                && (o.IdOrderNavigation.IdStatus == OrderStatusConst.RECIEVED
                || o.IdOrderNavigation.IdStatus == OrderStatusConst.Paid))
                .ToListAsync();
            #endregion

            List<ProductRankingView> products = new List<ProductRankingView>();
            foreach(var item in data)
            {
                if(!products.Any(p => p.id == item.IdProductVariantNavigation.Id))
                {
                    ProductRankingView view = new ProductRankingView();
                    view.id = item.IdProductVariantNavigation.Id;
                    #region tên sp
                    string name = item.IdProductVariantNavigation.IdProductNavigation.Name;
                    string colorName = item.IdProductVariantNavigation.IdColorNavigation.Name;
                    string sizeName = item.IdProductVariantNavigation.IdSizeNavigation.Name;
                    string unitName = item.IdProductVariantNavigation.IdUnitNavigation.Name;
                    string variantName = $"{name}";
                    if (unitName != null)
                    {
                        variantName = unitName + " " + variantName;
                    }
                    if (colorName != null)
                    {
                        variantName = variantName + " màu " + colorName;
                    }
                    if (sizeName != null)
                    {
                        variantName = variantName + " kích thước " + sizeName;
                    }
                    #endregion
                    view.name = variantName;
                    view.totalQuantity = item.Quantity;
                    view.totalBenefit = item.SellingPrice * item.Quantity;
                    products.Add(view);
                }
                else
                {
                    ProductRankingView view = products.FirstOrDefault(p => p.id == item.IdProductVariantNavigation.Id);
                    if(view != null)
                    {
                        view.totalQuantity += item.Quantity;
                        view.totalBenefit += (item.Quantity * item.SellingPrice);
                    }
                }
            }
            return products.OrderByDescending(p => p.totalQuantity);
        }
        public async Task<IEnumerable<ProductRankingView>> GetTopBenefitProduct(int month, int idBranch)
        {
            #region query data
            var data = await _context.OrderItems
                .Include(o => o.IdOrderNavigation)
                .Include(o => o.IdProductVariantNavigation)
                    .ThenInclude(pv => pv.IdColorNavigation)
                .Include(o => o.IdProductVariantNavigation)
                    .ThenInclude(pv => pv.IdSizeNavigation)
                .Include(o => o.IdProductVariantNavigation)
                    .ThenInclude(pv => pv.IdUnitNavigation)
                .Include(o => o.IdProductVariantNavigation)
                    .ThenInclude(pv => pv.IdProductNavigation)
                .Where(o => o.IdOrderNavigation.IdBranch == idBranch
                && o.IdOrderNavigation.CreateDate.Value.Month == month
                && (o.IdOrderNavigation.IdStatus == OrderStatusConst.RECIEVED
                || o.IdOrderNavigation.IdStatus == OrderStatusConst.Paid))
                .ToListAsync();
            #endregion

            List<ProductRankingView> products = new List<ProductRankingView>();
            foreach (var item in data)
            {
                if (!products.Any(p => p.id == item.IdProductVariantNavigation.Id))
                {
                    ProductRankingView view = new ProductRankingView();
                    view.id = item.IdProductVariantNavigation.Id;
                    #region tên sp
                    string name = item.IdProductVariantNavigation.IdProductNavigation.Name;
                    string colorName = item.IdProductVariantNavigation.IdColorNavigation.Name;
                    string sizeName = item.IdProductVariantNavigation.IdSizeNavigation.Name;
                    string unitName = item.IdProductVariantNavigation.IdUnitNavigation.Name;
                    string variantName = $"{name}";
                    if (unitName != null)
                    {
                        variantName = unitName + " " + variantName;
                    }
                    if (colorName != null)
                    {
                        variantName = variantName + " màu " + colorName;
                    }
                    if (sizeName != null)
                    {
                        variantName = variantName + " kích thước " + sizeName;
                    }
                    #endregion
                    view.name = variantName;
                    view.totalQuantity = item.Quantity;
                    view.totalBenefit = item.SellingPrice * item.Quantity;
                    products.Add(view);
                }
                else
                {
                    ProductRankingView view = products.FirstOrDefault(p => p.id == item.IdProductVariantNavigation.Id);
                    if (view != null)
                    {
                        view.totalQuantity += item.Quantity;
                        view.totalBenefit += (item.Quantity * item.SellingPrice);
                    }
                }
            }
            return products.OrderByDescending(p => p.totalBenefit);
        }
        public Task<IEnumerable<AspNetUser>> GetTop10User()
        {
            throw new NotImplementedException();
        }
        public async Task<int?> GetTodayPOValue(int idBranch)
        {
            var SumPO = await _context.PurchaseOrders
                .Include(po => po.IdPurchaseRequestNavigation)
                .Where(po => po.IdPurchaseRequestNavigation.IdBranch == idBranch
                    && (po.OrderStatus == OrderStatusConst.RECIEVED
                    || po.OrderStatus == OrderStatusConst.Paid)
                    && po.OrderDate.Value.Day == DateTime.Now.Day)
                .SumAsync(po => (po.PaymentAmount != null) ? po.PaymentAmount : po.IdPurchaseRequestNavigation.TotalAmount);
            return SumPO;
        }
        public async Task<int?> GetPOValueByDay(int idBranch, int year, int month, int day)
        {
            var SumPO = await _context.PurchaseOrders
                .Include(po => po.IdPurchaseRequestNavigation)
                .Where(po => po.IdPurchaseRequestNavigation.IdBranch == idBranch
                    && (po.OrderStatus == OrderStatusConst.RECIEVED
                    || po.OrderStatus == OrderStatusConst.Paid)
                    && po.OrderDate.Value.Day == day
                    && po.OrderDate.Value.Month == month
                    && po.OrderDate.Value.Year == year)
                .SumAsync(po => (po.PaymentAmount != null) ? po.PaymentAmount : po.IdPurchaseRequestNavigation.TotalAmount);
            return SumPO;
        }
        public async Task<int?> GetTodaySOValue(int idBranch)
        {
            var SumOrder = await _context.Orders
                .Where(o => o.IdBranch == idBranch
                && (o.IdStatus == OrderStatusConst.RECIEVED
                || o.IdStatus == OrderStatusConst.Paid)
                && o.CreateDate.Value.Day == DateTime.Now.Day)
                .SumAsync(o => o.Subtotal);
            return SumOrder;
        }
        public async Task<int?> GetSOValueByDay(int idBranch, int year, int month, int day)
        {
            var SumOrder = await _context.Orders
                .Where(o => o.IdBranch == idBranch
                && (o.IdStatus == OrderStatusConst.RECIEVED
                || o.IdStatus == OrderStatusConst.Paid)
                && o.CreateDate.Value.Day == day
                && o.CreateDate.Value.Month == month
                && o.CreateDate.Value.Year == year)
                .SumAsync(o => o.Subtotal);
            return SumOrder;
        }
        public async Task<int?> GetTodaySoldProduct(int idBranch)
        {
            #region query data
            var data = await _context.OrderItems
                .Include(o => o.IdOrderNavigation)
                .Where(o => o.IdOrderNavigation.IdBranch == idBranch
                && o.IdOrderNavigation.CreateDate.Value.Day == DateTime.Now.Day
                && (o.IdOrderNavigation.IdStatus == OrderStatusConst.RECIEVED
                || o.IdOrderNavigation.IdStatus == OrderStatusConst.Paid))
                .ToListAsync();
            #endregion
            int? total = 0;
            foreach(var item in data)
            {
                total += item.Quantity;
            }
            return total;
        }
    }
}
