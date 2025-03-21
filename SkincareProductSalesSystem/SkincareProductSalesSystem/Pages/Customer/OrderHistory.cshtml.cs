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

    }
}
