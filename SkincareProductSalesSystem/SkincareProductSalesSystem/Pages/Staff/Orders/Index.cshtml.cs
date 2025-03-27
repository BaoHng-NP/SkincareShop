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

namespace SkincareProductSalesSystem.Pages.Staff.Orders
{
    public class IndexModel : PageModel
    {
        private readonly IOrderService _orderService;
        private readonly IAccountService _accountService;
        private readonly IHubContext<SignalrServer> _hubContext;

        public IndexModel(IOrderService orderService, IAccountService accountService, IHubContext<SignalrServer> hubContext)
        {
            _orderService = orderService;
            _accountService = accountService;
            _hubContext = hubContext;
        }

        public IEnumerable<Order> Order { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Order = await _orderService.GetAllOrdersAsync();
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



    }
}
