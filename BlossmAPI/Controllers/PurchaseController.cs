using BlossmAPI.Attributes;
using BlossmAPI.ModelViews;
using BlossmAPI.Repositories.Interfaces;
using BlossmAPI.Repositories.Services;
using BlossmAPI.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace BlossmAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [BlossmAuthorize]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseServices _purchaseService;
        private readonly IUserServices _userService;

        public PurchaseController(
            IPurchaseServices purchaseService, 
            IUserServices userService)
        {
            _purchaseService = purchaseService;
            _userService = userService;
        }

        [HttpGet("GetAllRequest")]
        public async Task<IActionResult> GetAllRequest()
        {
            var rs = await _purchaseService.GetAllRequest();
            return ResponseHelper.IfNotFound(rs);
        }

        [HttpGet("GetRequestFromUser")]
        public async Task<IActionResult> GetRequestFromUser()
        {
            string id = await _userService.GetCurrentIdUser(HttpContext.User);
            var rs = await _purchaseService.GetRequestFromUser(id);
            return ResponseHelper.IfNotFound(rs);
        }

        [HttpGet("GetRequestFromBranch")]
        public async Task<IActionResult> GetRequestFromBranch(int idBranch)
        {
            var rs = await _purchaseService.GetRequestFromBranch(idBranch);
            return ResponseHelper.IfNotFound(rs);
        }

        [HttpPost("AddPurchaseRequest")]
        public async Task<IActionResult> AddPurchaseRequest(PurchaseRequestView purchaseRequestMV)
        {
            var idUser = await _userService.GetCurrentIdUser(HttpContext.User);
            var rs = await _purchaseService.AddRequest(idUser, purchaseRequestMV);
            return ResponseHelper.IfBad(rs.Success, rs.ErrorMessage);
        }

        [HttpDelete("DeleteRequest")]
        public async Task<IActionResult> DeleteRequest(int id)
        {
            var rs = await _purchaseService.DeleteRequest(id);
            return ResponseHelper.IfBad(rs);
        }

        [HttpDelete("RealDeleteRequest")]
        public async Task<IActionResult> RealDeleteRequest(int id)
        {
            var rs = await _purchaseService.RealDeleteRequest(id);
            return ResponseHelper.IfBad(rs);
        }

        [HttpPost("RejectRequest")]
        public async Task<IActionResult> RejectRequest([FromBody]int id)
        {
            var idUser = await _userService.GetCurrentIdUser(HttpContext.User);
            var rs = await _purchaseService.RejectRequest(id, idUser);
            return ResponseHelper.IfBad(rs);
        }

        [HttpPost("AcceptRequest")]
        public async Task<IActionResult> AcceptRequest([FromBody]int id_pr)
        {
            var idUser = await _userService.GetCurrentIdUser(HttpContext.User);
            var rs = await _purchaseService.AcceptRequest(idUser, id_pr);
            return ResponseHelper.IfBad(rs);
        }

        [HttpPost("ComfirmReceive")]
        public async Task<IActionResult> ComfirmReceive(int id)
        {
            var rs = await _purchaseService.ComfirmReceive(id);
            return ResponseHelper.IfBad(rs);
        }

        [HttpPost("ComfirmPayment")]
        public async Task<IActionResult> ComfirmPayment(ConfirmPayment confirm)
        {
            var rs = await _purchaseService.ComfirmPayment(confirm);
            return ResponseHelper.IfBad(rs.Success, rs.ErrorMessage);
        }

        [HttpPost("UpdatePurchaseOrderStatus")]
        public async Task<IActionResult> UpdatePurchaseOrderStatus(UpdatePurchaseOrderStatusView view)
        {
            var userId = await _userService.GetCurrentIdUser(HttpContext.User);
            var rs = await _purchaseService.UpdatePurchaseOrderStatus(userId, view);
            return ResponseHelper.IfBad(rs.Success, rs.ErrorMessage);
        }

        [HttpGet("GetPurchaseOrderByManager")]
        public async Task<IActionResult> GetPurchaseOrderByManager()
        {
            var userId = await _userService.GetCurrentIdUser(HttpContext.User);
            var rs = await _purchaseService.GetPurchaseOrderByManager(userId);
            return ResponseHelper.IfNotFound(rs.Data);
        }

        [HttpGet("GetAllPurchaseOrder")]
        public async Task<IActionResult> GetAllPurchaseOrder()
        {
            var rs = await _purchaseService.GetAllPurchaseOrder();
            return ResponseHelper.IfNotFound(rs.Data);
        }

        [HttpGet("GetPurchaseOrderById")]
        public async Task<IActionResult> GetPurchaseOrderById(int id)
        {
            var rs = await _purchaseService.GetPurchaseOrderById(id);
            return ResponseHelper.IfNotFound(rs.Data);
        }
    }
}
