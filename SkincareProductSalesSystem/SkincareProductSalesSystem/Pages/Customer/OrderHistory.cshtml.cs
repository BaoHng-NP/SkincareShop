using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using System.BLL.Services;
using System.Security.Claims;

namespace SkincareProductSalesSystem.Pages.Customer
{
    public class OrderHistoryModel : PageModel
    {
        private readonly IOrderService _orderService;
        private readonly IDiscountService _discountService;
        private readonly IUserVourcherService _userVourcherService;
        public OrderHistoryModel(IOrderService orderService, IDiscountService discountService, IUserVourcherService userVourcherService)
        {
            _orderService = orderService;
            _discountService = discountService;
            _userVourcherService = userVourcherService;
        }

        public IEnumerable<Order> Order { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Auth/Login");
            }

            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            Order = await _orderService.GetAllOrdersByuserId(userId);

            return Page();
        }

        public async Task<IActionResult> OnPostCancelAsync(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                TempData["ErrorMessage"] = "Order not found!";
                return Page();
            }


            order.Status = "Canceled";
            await _orderService.UpdateOrderAsync(order);
            if(order.PaymentMethod == "VNPay") { 
            var dis = new Discount { 
                Code = order.Status, 
               DiscountType = "fixed",
               DiscountValue = order.TotalPrice,
               RequiredPoints = 0,
               MinOrderValue = 0,
               CreatedAt = DateTime.Now,
            };
            await _discountService.AddDiscountAsync(dis);
            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId) || userId <= 0)
            {
                throw new InvalidOperationException("User is not authenticated.");
            }
            var newUserVoucher = new UserVoucher
            {
                UserId = userId,
                DiscountId = dis.Id,
                RedeemedAt = DateTime.Now,
            };
            await _userVourcherService.AddUserVoucherAsync(newUserVoucher);
            }
            TempData["SuccessMessage"] = "Order has been cancelled successfully!";
            return RedirectToPage("/Customer/OrderHistory");
        }
    }
}
