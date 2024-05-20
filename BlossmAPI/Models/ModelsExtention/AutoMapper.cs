using Azure.Core;
using BlossmAPI.ModelViews;
using static Azure.Core.HttpHeader;

namespace BlossmAPI.Models.ModelsExtention
{
    public class AutoMapper
    {
        public static PurchaseRequest PurchaseRequest(string userID, PurchaseRequestView purchaseRequestMV)
        {
            PurchaseRequest purchaseRequest = new PurchaseRequest();
            purchaseRequest.CreateDate = DateTime.Now;
            purchaseRequest.Requester = userID;
            purchaseRequest.Deadline = purchaseRequestMV.Deadline;
            purchaseRequest.Status = 1;
            purchaseRequest.Priority = purchaseRequestMV.Priority;
            purchaseRequest.Notes = purchaseRequestMV.Notes;
            purchaseRequest.IdBranch = purchaseRequestMV.IdBranch;
            purchaseRequest.IdSupplier = purchaseRequestMV.IdSupplier;

            return purchaseRequest;
        }

        public static ShoppingCart shoppingCart(string id_user, CartView cartView)
        {
            ShoppingCart shoppingCart = new ShoppingCart();
            try
            {
                shoppingCart.DateAdd = DateTime.Now;
                shoppingCart.Quantity = cartView.quantity;
                shoppingCart.IdUser = id_user;
                shoppingCart.IdProductVariant = cartView.id_variant;
                shoppingCart.Quantity = cartView.quantity;
                return shoppingCart;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public static OrderItem ToOrderItem(OrderItemView view)
        {
            OrderItem item = new OrderItem();
            try
            {
                item.IdProductVariant = view.id_variant;
                item.Quantity = view.quantity;
                item.SellingPrice = view.selling_price;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return item;
        }

        public static Order ToOrder(OrderView view)
        {
            Order order = new Order();
            try
            {
                order.Address = view.address;
                order.Note = view.note;
                order.Receiver = view.username;
                order.PhoneNumber = view.phoneNumber;
                order.Subtotal = view.totalValue;
                order.IdPaymentMethod = view.id_payment;
                if(view.voucher != null)
                {
                    order.IdVoucher = view.voucher.Id;
                    //order.Subtotal -= (view.voucher.DiscountPercentage == false) ? view.voucher.DiscountAmount : ((order.Subtotal * view.voucher.DiscountAmount) / 100);
                }
                    
                foreach (var item in view.orderItems)
                {
                    OrderItem new_item = new OrderItem();
                    new_item = ToOrderItem(item);
                    order.OrderItems.Add(new_item);
                }
                return order;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
