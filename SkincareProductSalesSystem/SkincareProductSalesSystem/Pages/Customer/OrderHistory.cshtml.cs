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

        public OrderHistoryModel(IOrderService orderService)
        {
            _orderService = orderService;
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

            TempData["SuccessMessage"] = "Order has been cancelled successfully!";
            return RedirectToPage("/Customer/OrderHistory");
        }
    }
}
