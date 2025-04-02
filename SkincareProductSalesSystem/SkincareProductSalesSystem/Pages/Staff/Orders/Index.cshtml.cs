using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using System.DAL;
using System.BLL.Services;
using System.Security.Claims;
using FUNewsManagementSystem.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;

namespace SkincareProductSalesSystem.Pages.Staff.Orders
{
    public class IndexModel : PageModel
    {
        private readonly IOrderService _orderService;
        private readonly IAccountService _accountService;
        private readonly IHubContext<SignalrServer> _hubContext;
        private readonly IDiscountService _discountService;
        private readonly IUserVourcherService _userVourcherService;
      
        public IndexModel(IOrderService orderService, IAccountService accountService, IDiscountService discountService, IUserVourcherService userVourcherService, IHubContext<SignalrServer> hubContext)
        {
            _orderService = orderService;
            _accountService = accountService;
            _hubContext = hubContext;
            _discountService = discountService;
            _userVourcherService = userVourcherService;
        }

        public IEnumerable<Order> Order { get; set; } = default!;
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }

        public async Task OnGetAsync(int pageNumber = 1)
        {
            int pageSize = 9;
            var orders = await _orderService.GetAllOrdersAsync();
            if (orders == null || !orders.Any())
            {
                ViewData["NoProductsMessage"] = "No Orders available.";
                Order = new List<Order>(); 
            }
            else
            {
                int totalProducts = orders.Count();
                TotalPages = (int)Math.Ceiling(totalProducts / (double)pageSize);
                CurrentPage = pageNumber;
                Order = orders.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            }
        }
        public async Task<IActionResult> OnPostConfirmAsync(int id)
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
            if (order.Status == "Pending")
            {
                order.Status = "Confirmed";
            }
            else if (order.Status == "Confirmed")
            {
                order.Status = "Shipping";
            }
            else if (order.Status == "Shipping")
            {
                order.Status = "Delivered";
                order.CreatedAt = DateTime.Now;
            }                     
            else
            {
                ModelState.AddModelError("", "Order cannot be updated further");
                return Page();
            }
            await _orderService.UpdateOrderAsync(order);
            await _hubContext.Clients.All.SendAsync("LoadAllOrder");

            Order = await _orderService.GetAllOrdersAsync();
            return RedirectToPage();
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
              
                var newUserVoucher = new UserVoucher
                {
                    UserId = order.UserId,
                    DiscountId = dis.Id,
                    RedeemedAt = DateTime.Now,
                };
                await _userVourcherService.AddUserVoucherAsync(newUserVoucher);
            }
            TempData["SuccessMessage"] = "Order has been cancelled successfully!";
            await _hubContext.Clients.All.SendAsync("LoadAllOrder");

            return RedirectToPage();
        }


    }
}
