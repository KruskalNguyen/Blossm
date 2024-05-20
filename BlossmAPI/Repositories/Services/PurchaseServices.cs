using Azure.Core;
using BlossmAPI.Models;
using BlossmAPI.Models.ModelsExtention;
using BlossmAPI.ModelViews;
using BlossmAPI.Repositories.Interfaces;
using BlossmAPI.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;
using System.Security.Claims;

namespace BlossmAPI.Repositories.Services
{
    public class PurchaseServices: IPurchaseServices
    {
        private readonly BlossmContext _context;

        public PurchaseServices(BlossmContext context)
        {
            _context = context;
        }

        public async Task<object> GetAllRequest()
        {
            var data = await _context.PurchaseRequests
               .Include(d => d.RequesterNavigation)
               .Include(d => d.IdBranchNavigation)
               .Include(d => d.IdSupplierNavigation.SupplierContacts)
               .Include(d => d.ApproverNavigation)
               .Include(d => d.PurchaseRequestItems)
               .ThenInclude(d => d.IdProductVariantNavigation)
               .ThenInclude(d => d.IdSizeNavigation)
               .Include(d => d.PurchaseRequestItems)
               .ThenInclude(d => d.IdProductVariantNavigation)
               .ThenInclude(d => d.IdColorNavigation)
               .Include(d => d.PurchaseRequestItems)
               .ThenInclude(d => d.IdProductVariantNavigation)
               .ThenInclude(d => d.IdUnitNavigation)
               .Select(d => new
               {
                   d.Id,
                   d.CreateDate,
                   d.Deadline,
                   idRequester = d.Requester,
                   fullNameRequester = d.RequesterNavigation.FirstName + " " + d.RequesterNavigation.LastName,
                   idStatus = d.StatusNavigation.Id,
                   Status = d.StatusNavigation.Name,
                   idPrioviry = d.PriorityNavigation.Id,
                   Prioviry = d.PriorityNavigation.Name,
                   d.Notes,
                   d.ApprovalDate,
                   d.TotalAmount,
                   d.TotalQuantity,
                   ApproverEmail = d.ApproverNavigation.Email,
                   ApproverFirstName = d.ApproverNavigation.FirstName,
                   ApproverLastName = d.ApproverNavigation.LastName,
                   ApproverPhoneNumner = d.ApproverNavigation.PhoneNumber,
                   IdBranch = d.IdBranchNavigation.Id,
                   BranchAdress = d.IdBranchNavigation.Address,
                   IdSupplier = d.IdSupplierNavigation.Id,
                   SupplierName = d.IdSupplierNavigation.Name,
                   SupplierContacts = d.IdSupplierNavigation.SupplierContacts.Select(c => new
                   {
                       c.Id,
                       c.ContactPerson,
                       c.Email,
                       c.Phone
                   }),
                   ProductItems = d.PurchaseRequestItems.Select(p => new
                   {
                       p.IdProductVariantNavigation.Id,
                       Product = p.IdProductVariantNavigation.IdProductNavigation.Name,
                       Unit = p.IdProductVariantNavigation.IdUnitNavigation.Name,
                       Size = p.IdProductVariantNavigation.IdSizeNavigation.Name,
                       Color = p.IdProductVariantNavigation.IdColorNavigation.Name,
                       PurchasePrice = p.IdProductVariantNavigation.PurchasePrice,
                       Quantity = p.Quantity,
                   }),
               })
               .ToListAsync();
            return data;
        }

        public async Task<object> GetRequestFromUser(string idUser)
        {
            var data = await _context.PurchaseRequests
                .Include(d => d.RequesterNavigation)
                .Include(d => d.IdBranchNavigation)
                .Include(d => d.IdSupplierNavigation.SupplierContacts)
                .Include(d => d.ApproverNavigation)
                .Include(d => d.PurchaseRequestItems)
                .ThenInclude(d => d.IdProductVariantNavigation)
                .ThenInclude(d => d.IdSizeNavigation)
                .Include(d => d.PurchaseRequestItems)
                .ThenInclude(d => d.IdProductVariantNavigation)
                .ThenInclude(d => d.IdColorNavigation)
                .Include(d => d.PurchaseRequestItems)
                .ThenInclude(d => d.IdProductVariantNavigation)
                .ThenInclude(d => d.IdUnitNavigation)
                .Select(d => new
                {
                    d.Id,
                    d.CreateDate,
                    d.Deadline,
                    idRequester = d.Requester,
                    fullNameRequester = d.RequesterNavigation.FirstName + " " + d.RequesterNavigation.LastName,
                    idStatus = d.StatusNavigation.Id,
                    Status = d.StatusNavigation.Name,
                    idPrioviry = d.PriorityNavigation.Id,
                    Prioviry = d.PriorityNavigation.Name,
                    d.Notes,
                    d.ApprovalDate,
                    d.TotalAmount,
                    d.TotalQuantity,
                    ApproverEmail = d.ApproverNavigation.Email,
                    ApproverFirstName = d.ApproverNavigation.FirstName,
                    ApproverLastName = d.ApproverNavigation.LastName,
                    ApproverPhoneNumner = d.ApproverNavigation.PhoneNumber,
                    IdBranch = d.IdBranchNavigation.Id,
                    BranchAdress = d.IdBranchNavigation.Address,
                    IdSupplier = d.IdSupplierNavigation.Id,
                    SupplierName = d.IdSupplierNavigation.Name,
                    SupplierContacts = d.IdSupplierNavigation.SupplierContacts.Select(c => new
                    {
                        c.Id,
                        c.ContactPerson,
                        c.Email,
                        c.Phone
                    }),
                    ProductItems = d.PurchaseRequestItems.Select(p => new
                    {
                        p.IdProductVariantNavigation.Id,
                        Product =  p.IdProductVariantNavigation.IdProductNavigation.Name,
                        Unit = p.IdProductVariantNavigation.IdUnitNavigation.Name,
                        Size = p.IdProductVariantNavigation.IdSizeNavigation.Name,
                        Color = p.IdProductVariantNavigation.IdColorNavigation.Name,
                        PurchasePrice = p.IdProductVariantNavigation.PurchasePrice,
                        Quantity = p.Quantity,
                    }),
                })
                .Where(p => p.idRequester.Equals(idUser) && p.idStatus != RequestStatusConst.DELETED).ToListAsync();
            return data;
        }

        public async Task<List<PurchaseRequest>> GetRequestFromBranch(int idBranch)
        {
            return _context.PurchaseRequests.Where(p => p.IdBranch == idBranch).ToList();
        }

        public async Task<ApiResponse<bool>> AddRequest(string userID, PurchaseRequestView purchaseRequestMV)
        {
            ApiResponse<bool> apiResponse = new ApiResponse<bool>();
            try
            {
                Employee employee = await _context.Employees.FirstOrDefaultAsync(e => e.IdUser.Equals(userID));

                if(employee.IdBranch != purchaseRequestMV.IdBranch)
                    return apiResponse.FalseResult("You are not allowed to perform this action.");

                PurchaseRequest purchaseRequest = AutoMapper.PurchaseRequest(userID, purchaseRequestMV);

                var toltalQual = purchaseRequestMV.Items.Sum(p => p.quantity);

                purchaseRequest.TotalQuantity = toltalQual;

                int totalAmount = 0;

                foreach(var item in purchaseRequestMV.Items)
                {
                    var product = await _context.ProductVariants.FirstOrDefaultAsync(pv => pv.Id == item.id_product_variant);
                    totalAmount = (int)(totalAmount + product.PurchasePrice*item.quantity);
                }

                purchaseRequest.TotalAmount = totalAmount;

                await _context.PurchaseRequests.AddAsync(purchaseRequest);
                await _context.SaveChangesAsync();  

                foreach (var item in purchaseRequestMV.Items)
                {
                    PurchaseRequestItem purchaseRequestItem = new PurchaseRequestItem();
                    purchaseRequestItem.IdProductVariant = item.id_product_variant;
                    purchaseRequestItem.IdPurchaseRequest = purchaseRequest.Id;
                    purchaseRequestItem.Quantity = item.quantity;
                    await _context.PurchaseRequestItems.AddAsync(purchaseRequestItem);

                    await _context.SaveChangesAsync();
                }
               
                apiResponse.Success = true;
                return apiResponse;
            }
            catch (Exception ex) {
                throw ex.InnerException;
            }
            
        }

        public async Task<bool> DeleteRequest(int id)
        {
            try
            {
                var request = await _context.PurchaseRequests.FirstOrDefaultAsync(r => r.Id == id);

                if (request.Status == RequestStatusConst.ACCEPT || 
                    request.Status == RequestStatusConst.REJECTED || 
                    request.Status == RequestStatusConst.DELETED)
                    return false;

                request.Status = RequestStatusConst.DELETED;

                await _context.SaveChangesAsync();

                return true;
            }
            catch(Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public async Task<bool> RealDeleteRequest(int id)
        {
            try
            {
                var request = await _context.PurchaseRequests.Include(p => p.PurchaseRequestItems).FirstOrDefaultAsync(r => r.Id == id);

                request.PurchaseRequestItems = null;
                await _context.SaveChangesAsync();

                _context.PurchaseRequests.Remove(request); 
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public async Task<bool> RejectRequest(int id, string id_user)
        {
            try
            {
                var request = await _context.PurchaseRequests.FirstOrDefaultAsync(r => r.Id == id);
                request.Status = RequestStatusConst.REJECTED;
                request.Approver = id_user;
                request.ApprovalDate = DateTime.Now;

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public async Task<bool> AcceptRequest(string id_user, int id_pr)
        {
            try
            {
                var purchaseRequest = await _context.PurchaseRequests
                    .FirstOrDefaultAsync(p => p.Id == id_pr);

                purchaseRequest.ApprovalDate = DateTime.Now;
                purchaseRequest.Status = RequestStatusConst.ACCEPT;
                purchaseRequest.Approver = id_user;

                await _context.SaveChangesAsync();

                PurchaseOrder purchaseOrder = new PurchaseOrder();
                purchaseOrder.IdPurchaseRequest = id_pr;
                purchaseOrder.OrderDate = DateTime.Now;
                purchaseOrder.OrderStatus = OrderStatusConst.NEW;
                purchaseOrder.DeliveryStatus = DeliveryStatusConst.PENDING;

                await _context.PurchaseOrders.AddAsync(purchaseOrder);
                await _context.SaveChangesAsync();

                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> ComfirmReceive(int id)
        {
            try
            {
                var purchaseOrder = await _context.PurchaseOrders
                    .FirstOrDefaultAsync(p => p.Id == id);

                purchaseOrder.ReceivingDate = DateTime.Now;

                await _context.SaveChangesAsync();  

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ApiResponse<bool>> ComfirmPayment(ConfirmPayment confirm)
        {
            var response = new ApiResponse<bool>();
            try
            {
                var purchaseOrder = await _context.PurchaseOrders
                    .FirstOrDefaultAsync(p => p.Id == confirm.id_purchase_order); 

                if (purchaseOrder.PaymentAmount != null ||
                    purchaseOrder.PaymentAmount != null ||
                    purchaseOrder.Note != null)
                {
                    response.Success = false;
                    response.ErrorMessage = "This order has been confirmed";
                    return response;
                }

                purchaseOrder.PaymentAmount = confirm.payment_amount;
                purchaseOrder.PaymentMethod = confirm.id_payment_method;

                if(confirm.note != null)
                    purchaseOrder.Note = confirm.note;

                await _context.SaveChangesAsync();

                response.Success = true;
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ApiResponse<bool>> UpdatePurchaseOrderStatus(string user_id, UpdatePurchaseOrderStatusView view)
        {
            ApiResponse<bool> apiResponse = new ApiResponse<bool>();
            try
            {
                var purchaseOrder = await _context.PurchaseOrders
                    .Include(p => p.IdPurchaseRequestNavigation.PurchaseRequestItems)
                    .FirstOrDefaultAsync(p => p.Id == view.id_purchase_order);

                int? id_branch = purchaseOrder.IdPurchaseRequestNavigation.IdBranch;

                var employee = await _context.Employees
                    .FirstOrDefaultAsync(e => e.IdUser == user_id);

                if(employee.IdBranch != 1)
                    if (employee.IdBranch != id_branch)
                        return apiResponse.FalseResult("You are not allowed to perform this action.");     
                
                if(view.id_status == purchaseOrder.OrderStatus)
                    return apiResponse.FalseResult("The order has already and is currently in this state.");

                if(view.id_status == OrderStatusConst.CANCLED &&
                    purchaseOrder.OrderStatus > OrderStatusConst.PROCESSING)
                    return apiResponse.FalseResult("Your order is being delivered, you are not allowed to cancel.");

                if (view.id_status < OrderStatusConst.CANCLED &&
                    purchaseOrder.OrderStatus >= view.id_status)
                    return apiResponse.FalseResult("You are not allowed to update the completed processes.");

                if (purchaseOrder.OrderStatus == OrderStatusConst.CANCLED)
                    return apiResponse.FalseResult("The canceled order cannot be updated anymore.");


                purchaseOrder.OrderStatus = view.id_status;

                if(purchaseOrder.OrderStatus == OrderStatusConst.RECIEVED)
                {
                    purchaseOrder.DeliveryStatus = DeliveryStatusConst.DELIVERED;
                    purchaseOrder.Receiver = user_id;
                    purchaseOrder.ReceivingDate = DateTime.Now;

                    var bPV = await _context.BranchProductVariants
                        .Where(b => b.IdBranch == purchaseOrder.IdPurchaseRequestNavigation.IdBranch)
                        .ToListAsync();
                    foreach (var item in purchaseOrder.IdPurchaseRequestNavigation.PurchaseRequestItems)
                    {
                        var pv = bPV.FirstOrDefault(p => p.IdProductVariant == item.IdProductVariant);
                        if(pv != null)
                        {
                            pv.InventoryQuantity = pv.InventoryQuantity + item.Quantity;
                            _context.BranchProductVariants.Update(pv);
                        }
                        else
                        {
                            _context.BranchProductVariants.AddAsync(pv);
                        }
                        await _context.SaveChangesAsync();
                    }
                }

                if (purchaseOrder.OrderStatus == OrderStatusConst.CANCLED)
                    purchaseOrder.DeliveryStatus = DeliveryStatusConst.CANCELED;

                if (purchaseOrder.OrderStatus == OrderStatusConst.SHIPPING)
                    purchaseOrder.DeliveryStatus = DeliveryStatusConst.INTRANSIT;


                await _context.SaveChangesAsync();

                apiResponse.Success = true;
                return apiResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ApiResponse<object>> GetPurchaseOrderByManager(string id)
        {
            ApiResponse<object> apiResponse = new ApiResponse<object>();
            try
            {
                var employee = await _context.Employees.FirstOrDefaultAsync(b => b.IdUser == id);
                var orders = await _context.PurchaseOrders
                    .Include(o => o.IdPurchaseRequestNavigation)
                    .Include(o => o.PaymentMethodNavigation)
                    .Include(o => o.OrderStatusNavigation)
                    .Include(o => o.DeliveryStatusNavigation)
                    .Include(o => o.ReceiverNavigation)
                    .Where(o => o.IdPurchaseRequestNavigation.IdBranch == employee.IdBranch && o.IdPurchaseRequestNavigation.Requester == id)
                    .Select(o => new {
                        o.Id,
                        o.OrderDate,
                        IdPaymentMethod = (int?)o.PaymentMethodNavigation.Id,
                        NamePaymentMethod = o.PaymentMethodNavigation.Name,
                        IdOrderStatus = (int?)o.OrderStatusNavigation.Id,
                        NameOrderStatus = o.OrderStatusNavigation.Name,
                        IdReceiver = o.Receiver,
                        LastNameReceiver = o.ReceiverNavigation.LastName,
                        FirstNameReceiver = o.ReceiverNavigation.FirstName,
                        o.ReceivingDate,
                        IdDeliveryStatus = (int?)o.DeliveryStatusNavigation.Id,
                        NameDeliveryStatus = o.DeliveryStatusNavigation.Name,
                        o.EstimatedDeliveryDate,
                        o.IdPurchaseRequest,
                        FirstNameRequester = _context.AspNetUsers.FirstOrDefault(u => u.Id == o.IdPurchaseRequestNavigation.Requester).FirstName,
                        LastNameRequester = _context.AspNetUsers.FirstOrDefault(u => u.Id == o.IdPurchaseRequestNavigation.Requester).LastName,
                        PRTotalAmount = o.IdPurchaseRequestNavigation.TotalAmount,
                        RequestDate = o.IdPurchaseRequestNavigation.CreateDate,
                        o.PaymentAmount,
                        o.Note
                    })
                    .ToListAsync();

                return apiResponse.SuccessResult(orders);
            }
            catch (Exception ex)
            {
                return apiResponse.FalseResult(ex.InnerException.Message);
            }
        }

        public async Task<ApiResponse<object>> GetAllPurchaseOrder()
        {
            ApiResponse<object> apiResponse = new ApiResponse<object>();
            try
            {
                var orders = await _context.PurchaseOrders
                    .Include(o => o.IdPurchaseRequestNavigation)
                    .Include(o => o.PaymentMethodNavigation)
                    .Include(o => o.OrderStatusNavigation)
                    .Include(o => o.DeliveryStatusNavigation)
                    .Include(o => o.ReceiverNavigation)
                    .Select(o => new {
                        o.Id,
                        o.OrderDate,
                        IdPaymentMethod = (int?)o.PaymentMethodNavigation.Id,
                        NamePaymentMethod = o.PaymentMethodNavigation.Name,
                        IdOrderStatus = (int?)o.OrderStatusNavigation.Id,
                        NameOrderStatus = o.OrderStatusNavigation.Name,
                        IdReceiver = o.Receiver,
                        LastNameReceiver = o.ReceiverNavigation.LastName,
                        FirstNameReceiver = o.ReceiverNavigation.FirstName,
                        o.ReceivingDate,
                        IdDeliveryStatus = (int?)o.DeliveryStatusNavigation.Id,
                        NameDeliveryStatus = o.DeliveryStatusNavigation.Name,
                        o.EstimatedDeliveryDate,
                        o.IdPurchaseRequest,
                        FirstNameRequester = _context.AspNetUsers.FirstOrDefault(u => u.Id == o.IdPurchaseRequestNavigation.Requester).FirstName,
                        LastNameRequester = _context.AspNetUsers.FirstOrDefault(u => u.Id == o.IdPurchaseRequestNavigation.Requester).LastName,
                        PRTotalAmount = o.IdPurchaseRequestNavigation.TotalAmount,
                        RequestDate = o.IdPurchaseRequestNavigation.CreateDate,
                        o.PaymentAmount,
                        o.Note
                    })
                    .ToListAsync();

                return apiResponse.SuccessResult(orders);
            }
            catch (Exception ex)
            {
                return apiResponse.FalseResult(ex.InnerException.Message);
            }
        }

        public async Task<ApiResponse<object>> GetPurchaseOrderById(int id)
        {
            ApiResponse<object> apiResponse = new ApiResponse<object>();
            try
            {
                var orders = await _context.PurchaseOrders
                    .Include(o => o.IdPurchaseRequestNavigation)
                    .Include(o => o.PaymentMethodNavigation)
                    .Include(o => o.OrderStatusNavigation)
                    .Include(o => o.DeliveryStatusNavigation)
                    .Include(o => o.ReceiverNavigation)
                    .Select(o => new {
                        o.Id,
                        o.OrderDate,
                        IdPaymentMethod = (int?)o.PaymentMethodNavigation.Id,
                        NamePaymentMethod = o.PaymentMethodNavigation.Name,
                        IdOrderStatus = (int?)o.OrderStatusNavigation.Id,
                        NameOrderStatus = o.OrderStatusNavigation.Name,
                        IdReceiver = o.Receiver,
                        LastNameReceiver = o.ReceiverNavigation.LastName,
                        FirstNameReceiver = o.ReceiverNavigation.FirstName,
                        o.ReceivingDate,
                        IdDeliveryStatus = (int?)o.DeliveryStatusNavigation.Id,
                        NameDeliveryStatus = o.DeliveryStatusNavigation.Name,
                        o.EstimatedDeliveryDate,
                        o.IdPurchaseRequest,
                        FirstNameRequester = _context.AspNetUsers.FirstOrDefault(u => u.Id == o.IdPurchaseRequestNavigation.Requester).FirstName,
                        LastNameRequester = _context.AspNetUsers.FirstOrDefault(u => u.Id == o.IdPurchaseRequestNavigation.Requester).LastName,
                        PRTotalAmount = o.IdPurchaseRequestNavigation.TotalAmount,
                        RequestDate = o.IdPurchaseRequestNavigation.CreateDate,
                        o.PaymentAmount,
                        o.Note
                    })
                    .FirstOrDefaultAsync(o => o.Id == id);

                return apiResponse.SuccessResult(orders);
            }
            catch (Exception ex)
            {
                return apiResponse.FalseResult(ex.InnerException.Message);
            }
        }
    }
}
