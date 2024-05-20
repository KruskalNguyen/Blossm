using BlossmAPI.Models;
using BlossmAPI.Models.DistanceResult;
using BlossmAPI.Models.Geocode;
using BlossmAPI.Models.ModelsExtention;
using BlossmAPI.ModelViews;
using BlossmAPI.Repositories.Interfaces;
using BlossmAPI.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;
using System.ComponentModel.Design;
using System.Net.Http.Json;

namespace BlossmAPI.Repositories.Services
{
    public class OrderServices : IOrderServices
    {
        private readonly BlossmContext _context;
        private readonly HttpClient _httpClient;
        private int PAGE_SIZE = 100;
        private int PAGE_NUMBER = 1;

        public OrderServices(BlossmContext context, HttpClient httpClient)
        {
            _context = context;
            _httpClient = httpClient;
        }
        public async Task<ApiResponse<object>> GetOrders(int page_number)
        {
            object orders;
            if (page_number != 0)
            {
                orders = await _context.Orders
                    .OrderByDescending(v => v.Id)
                    .Include(o => o.OrderItems)
                    .ThenInclude(o => o.IdProductVariantNavigation)
                    .Include(o => o.DeliveryStatusNavigation)
                    .Include(o => o.IdStatusNavigation)
                    .Select(o => new
                    {
                        o.Id,
                        o.CreateDate,
                        o.Address,
                        o.Note,
                        o.Subtotal,
                        o.CancellationReason,
                        DeliveryStatus = o.DeliveryStatusNavigation.Name,
                        o.IdUser,
                        Status = o.IdStatusNavigation.Name,
                        IdBranch = o.IdBranch,
                        latlng = o.Latlng,
                        reciver = o.Receiver,
                        quantity = o.OrderItems.Sum(or => or.Quantity),
                        ProductVariants = o.OrderItems.Select(or => or.IdProductVariantNavigation).ToList(),
                        orderItems = o.OrderItems.Select(oi => new OrderItem() 
                        { 
                            IdProductVariantNavigation = null, 
                            IdOrder = oi.IdOrder, 
                            IdProductVariant = oi.IdProductVariant,
                            Quantity = oi.Quantity,
                            SellingPrice = oi.SellingPrice
                            }),
                        voucher = o.IdVoucherNavigation,
                        paymentMethod = o.IdPaymentMethodNavigation.Name
                    })
                    .Skip((page_number - 1) * PAGE_SIZE)
                    .Take(PAGE_SIZE)
                    .ToListAsync();
            }
            else
            {
                orders = await _context.Orders
                    .OrderByDescending(v => v.Id)
                    .Include(o => o.OrderItems)
                    .ThenInclude(o => o.IdProductVariantNavigation)
                    .Include(o => o.DeliveryStatusNavigation)
                    .Include(o => o.IdStatusNavigation)
                    .Select(o => new
                    {
                        o.Id,
                        o.CreateDate,
                        o.Address,
                        o.Note,
                        o.Subtotal,
                        o.CancellationReason,
                        DeliveryStatus = o.DeliveryStatusNavigation.Name,
                        o.IdUser,
                        Status = o.IdStatusNavigation.Name,
                        reciver = o.Receiver,
                        ProductVariants = o.OrderItems.Select(or => or.IdProductVariantNavigation).ToList(),
                        orderItems = o.OrderItems.ToList(),
                    })
                    .Skip((PAGE_NUMBER - 1) * PAGE_SIZE)
                    .Take(PAGE_SIZE)
                    .ToListAsync();
            }

            

            ApiResponse<object> apiResponse = new ApiResponse<object>();
            if (orders != null)
            {
                apiResponse.Success = true;
                apiResponse.Data = orders;
                return apiResponse;
            }
            apiResponse.ErrorMessage = "Bạn không có đơn hàng nào";
            apiResponse.Success = false;
            return apiResponse;
        }
        public async Task<ApiResponse<Order>> GetOrderById(int id)
        {
            var rs = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(o => o.IdProductVariantNavigation)
                .ThenInclude(o => o.IdProductNavigation)
                .Include(o => o.OrderItems)
                .ThenInclude(o => o.IdProductVariantNavigation)
                .ThenInclude(o => o.IdColorNavigation)
                .Include(o => o.OrderItems)
                .ThenInclude(o => o.IdProductVariantNavigation)
                .ThenInclude(o => o.IdSizeNavigation)
                .Include(o => o.OrderItems)
                .ThenInclude(o => o.IdProductVariantNavigation)
                .ThenInclude(o => o.IdUnitNavigation)
                .Include(o => o.OrderItems)
                .ThenInclude(o => o.IdProductVariantNavigation)
                .ThenInclude(o => o.Images)
                .Include(o => o.IdStatusNavigation)
                .Include(o => o.DeliveryStatusNavigation)
                .FirstOrDefaultAsync(o => o.Id == id);

            if(rs.IdBranchNavigation != null)
                rs.IdBranchNavigation.Orders = null;

            foreach (var oi in rs.OrderItems)
            {
                oi.IdOrderNavigation = null;
                oi.IdProductVariantNavigation.OrderItems = null;
                oi.IdProductVariantNavigation.IdProductNavigation.ProductVariants = null;
                oi.IdProductVariantNavigation.IdColorNavigation.ProductVariants = null;
                oi.IdProductVariantNavigation.IdUnitNavigation.ProductVariants = null;
                oi.IdProductVariantNavigation.IdSizeNavigation.ProductVariants = null;
                foreach (var img in oi.IdProductVariantNavigation.Images)
                {
                    img.IdProductVariantNavigation = null;
                }
            }
            rs.IdStatusNavigation.Orders = null;
            rs.DeliveryStatusNavigation = null;

            ApiResponse<Order> apiResponse = new ApiResponse<Order>();
            if (rs != null)
            {
                apiResponse.Success = true;
                apiResponse.Data = rs;
                return apiResponse;
            }
            apiResponse.ErrorMessage = "Đơn hàng không tồn tại";
            apiResponse.Success = false;
            return apiResponse;
        }
        public async Task<ApiResponse<List<Order>>> GetOrderByUserId(string id)
        {
            var orders = await _context.Orders
                .Include(o => o.IdPaymentMethodNavigation)
                .Include(o => o.IdStatusNavigation)
                .Include(o => o.DeliveryStatusNavigation)
                .Include(o => o.OrderItems)
                .Include(o => o.OrderItems)
                            .ThenInclude(o => o.IdProductVariantNavigation)
                                .ThenInclude(o => o.IdProductNavigation)
                        .Include(o => o.OrderItems)
                            .ThenInclude(o => o.IdProductVariantNavigation)
                                .ThenInclude(o => o.IdColorNavigation)
                        .Include(o => o.OrderItems)
                            .ThenInclude(o => o.IdProductVariantNavigation)
                                .ThenInclude(o => o.IdSizeNavigation)
                        .Include(o => o.OrderItems)
                            .ThenInclude(o => o.IdProductVariantNavigation)
                                .ThenInclude(o => o.IdUnitNavigation)
                        .Include(o => o.OrderItems)
                            .ThenInclude(o => o.IdProductVariantNavigation)
                                .ThenInclude(o => o.Images)
                .Where(o => o.IdUser == id).ToListAsync();


            foreach(var order in orders)
            {
                order.IdPaymentMethodNavigation.Orders = null;
                order.IdStatusNavigation.Orders = null;
                order.DeliveryStatusNavigation.Orders = null;
                foreach (var oi in order.OrderItems)
                {
                    oi.IdOrderNavigation = null;
                    oi.IdProductVariantNavigation.OrderItems = null;
                    oi.IdProductVariantNavigation.IdProductNavigation.ProductVariants = null;
                    oi.IdProductVariantNavigation.IdColorNavigation.ProductVariants = null;
                    oi.IdProductVariantNavigation.IdUnitNavigation.ProductVariants = null;
                    oi.IdProductVariantNavigation.IdSizeNavigation.ProductVariants = null;
                    foreach (var img in oi.IdProductVariantNavigation.Images)
                    {
                        img.IdProductVariantNavigation = null;
                    }
                }
            }


            ApiResponse<List<Order>> apiResponse = new ApiResponse<List<Order>>();
            if(orders != null)
            {
                apiResponse.Success = true;
                apiResponse.Data = orders;
                return apiResponse;
            }
            apiResponse.ErrorMessage = "Bạn không có đơn hàng nào";
            apiResponse.Success = false;
            return apiResponse;
        }
        public async Task<ApiResponse<Order>> CreateOrder(OrderView view, string id)
        {
            view.orderItems = view.orderItems.Distinct().ToList();
            var checkOrder = await _context.Orders
                .Include(o => o.IdStatusNavigation)
                .Include(o => o.DeliveryStatusNavigation)
                .Where(o => o.IdUser == view.id_user)
                .ToListAsync();
            int countOrder = checkOrder.Count() - checkOrder.Where(o => o.DeliveryStatus == DeliveryStatusConst.CANCELED).Count()
                                                - checkOrder.Where(o => o.DeliveryStatus == DeliveryStatusConst.DELIVERED).Count()
                                                - checkOrder.Where(o => o.DeliveryStatus == DeliveryStatusConst.RETURNED).Count();
            if (countOrder > 5)
            {
                ApiResponse<Order> apiResponse = new ApiResponse<Order>();
                apiResponse.Success = false;
                return apiResponse;
            }

            try
            {
                Order order = AutoMapper.ToOrder(view);
                order.IdUser = id;
                order.IdStatus = OrderStatusConst.NEW;
                order.DeliveryStatus = DeliveryStatusConst.PENDING;
                order.CreateDate = DateTime.Now;

                string apiGeoCode = "https://rsapi.goong.io/geocode" +
                    "?address=" + order.Address +
                    "&api_key=" + StaticValue.goongKey;
                Geocode geocode = await _httpClient.GetFromJsonAsync<Geocode>(apiGeoCode);

                string lat = geocode.results[0].geometry.location.lat.ToString();
                string lng = geocode.results[0].geometry.location.lng.ToString();

                string location = $"{lat},{lng}";

                order.Latlng = location;

                List<Branch> branchList = _context.Branches.Where(b => b.Latlng != null).ToList();

                string listLocationBranch = "";

                int startForBranch = 0;
                int endForBranch = branchList.Count() - 1;

                foreach (Branch branch in branchList)
                {


                    if (startForBranch != endForBranch)
                        listLocationBranch = listLocationBranch + branch.Latlng + "|";
                    else
                        listLocationBranch = listLocationBranch + branch.Latlng;
                    startForBranch++;
                }

                string apiDistance = "https://rsapi.goong.io/DistanceMatrix" +
                    "?origins=" + location +
                    "&destinations=" + listLocationBranch +
                    "&vehicle=" + "car" +
                    "&api_key=" + StaticValue.goongKey;

                DistanceResult distanceResult = await _httpClient.GetFromJsonAsync<DistanceResult>(apiDistance);

                var eDis = distanceResult.rows[0].elements.OrderBy(v => v.distance.value).ToList();

                var indexEDis = distanceResult.rows[0].elements.IndexOf(eDis[0]);

                order.IdBranch = branchList[indexEDis].Id;

                _context.Orders.Add(order);
                await _context.SaveChangesAsync();


                ApiResponse<Order> apiResponse = new ApiResponse<Order>();
                apiResponse.Success = true;
                apiResponse.Data = GetOrderById(order.Id).Result.Data;
                return apiResponse;
            }
            catch
            {
                throw new Exception("Fail to create new Order");
            }
        }
        public async Task<ApiResponse<Order>> UpdateOrderDeliveryStatus(OrderView view)
        {
            try
            {
                var current_order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == view.id);
                if (current_order != null)
                {
                    
                    if(view.id_delivery_status > current_order.DeliveryStatus)
                    {
                        current_order.DeliveryStatus = view.id_delivery_status;

                        _context.Update(current_order);
                        await _context.SaveChangesAsync();

                        if(view.id_delivery_status == 3)
                        {
                            foreach(var item in view.orderItems)
                            {
                                var branchPv = _context.BranchProductVariants.FirstOrDefault(b => b.IdProductVariant == item.id_variant && b.IdBranch == view.id_branch);
                                branchPv.InventoryQuantity = branchPv.InventoryQuantity - item.quantity;
                                _context.Update(branchPv);
                                await _context.SaveChangesAsync();
                            }
                            
                        }

                        ApiResponse<Order> apiResponse1 = new ApiResponse<Order>();
                        apiResponse1.Success = true;
                        apiResponse1.Data = current_order;
                        return apiResponse1;
                    }
                    else
                    {
                        ApiResponse<Order> apiResponse1 = new ApiResponse<Order>();
                        apiResponse1.Success = false;
                        apiResponse1.Data = current_order;
                        apiResponse1.ErrorMessage = "Level error! Please pick higher level delivery status";
                        return apiResponse1;
                    }
                    
                }
            }
            catch
            {

            }
            ApiResponse<Order> apiResponse = new ApiResponse<Order>();
            apiResponse.Success = false;
            return apiResponse;
        }
        public async Task<ApiResponse<Order>> UpdateOrderStatus(OrderView view)
        {
            try
            {
                var current_order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == view.id);
                if (current_order != null)
                {

                    if (view.id_status > current_order.IdStatus)
                    {
                        current_order.IdStatus = view.id_status;

                        _context.Update(current_order);
                        await _context.SaveChangesAsync();

                        ApiResponse<Order> apiResponse1 = new ApiResponse<Order>();
                        apiResponse1.Success = true;
                        apiResponse1.Data = current_order;
                        return apiResponse1;
                    }
                    else
                    {
                        ApiResponse<Order> apiResponse1 = new ApiResponse<Order>();
                        apiResponse1.Success = false;
                        apiResponse1.Data = current_order;
                        apiResponse1.ErrorMessage = "Level error! Please pick higher level status";
                        return apiResponse1;
                    }
                }
            }
            catch
            {

            }
            ApiResponse<Order> apiResponse = new ApiResponse<Order>();
            apiResponse.Success = false;
            return apiResponse;
        }
        public int? CountSubtotal(List<OrderItem> items)
        {
            int? total = 0;
            if(items.Count > 0)
            {
                foreach (var item in items)
                {
                    total += (item.SellingPrice * item.Quantity);
                }
            }
            return total;
        }
    }
}