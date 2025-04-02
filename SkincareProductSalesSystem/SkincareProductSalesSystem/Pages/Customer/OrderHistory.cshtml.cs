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
using Microsoft.AspNetCore.SignalR;

namespace SkincareProductSalesSystem.Pages.Customer
{
    public class OrderHistoryModel : PageModel
    {
        private readonly IOrderService _orderService;
        private readonly IDiscountService _discountService;
        private readonly IUserVourcherService _userVourcherService;
        private readonly IAccountService _accountService;
        public OrderHistoryModel(IOrderService orderService, IDiscountService discountService, IUserVourcherService userVourcherService, IAccountService accountService)
        {
            _orderService = orderService;
            _discountService = discountService;
            _userVourcherService = userVourcherService;
            _accountService = accountService;
        }

        public IEnumerable<Order> Order { get; set; } = default!;

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
            if (order.PaymentMethod == "VNPay")
            {
                var dis = new Discount
                {
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

        public async Task<IActionResult> OnPostCompleteAsync(int id)
        {
            if (id <= 0)
            {
                ModelState.AddModelError("", "Invalid order ID");
                return Page();
            }

            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                ModelState.AddModelError("", "Order not found");
                return Page();
            }

            if (order.UserId.HasValue)
            {
                var acc = await _accountService.GetAccountByIdAsync(order.UserId.Value);
                if (acc != null)
                {
                    order.Status = "Completed-NonFeedback";
                    acc.LoyaltyPoints += (int)(Math.Floor(order.TotalPrice / 10000));
                    await _accountService.UpdateAccountAsync(acc);
                }
            }


            await _orderService.UpdateOrderAsync(order);
            //await _hubContext.Clients.All.SendAsync("LoadAllOrder");

            Order = await _orderService.GetAllOrdersAsync();
            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostAutoCompleteAsync(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order != null && order.Status == "Delivered")
            {
                order.Status = "Completed-NonFeedback";
                await _orderService.UpdateOrderAsync(order);
                return new JsonResult(new { success = true });
            }
            return new JsonResult(new { success = false });
        }
    }
}
