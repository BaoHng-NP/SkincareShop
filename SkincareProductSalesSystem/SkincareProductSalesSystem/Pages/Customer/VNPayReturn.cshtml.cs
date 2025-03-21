using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkincareProductSalesSystem.Helpers;
using System.BLL.Services;
using System.Security.Claims;

namespace SkincareProductSalesSystem.Pages.Customer
{
    public class VNPayReturnModel : PageModel
    {
       private readonly IOrderService _orderService;

        public string Message { get; set; } = string.Empty;

        public VNPayReturnModel( IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var queryParams = HttpContext.Request.Query;
            string vnpResponseCode = queryParams["vnp_ResponseCode"];
            string orderInfo = queryParams["vnp_OrderInfo"];
            decimal amount = decimal.Parse(queryParams["vnp_Amount"]) / 100;

            // Kiểm tra nếu orderInfo bị thiếu hoặc sai format
            string[] orderData = orderInfo.Split('|');
            if (orderData.Length < 3)
            {
                Message = "Invalid order information.";
                return RedirectToPage("/Customer/Checkout");
            }

            string phone = orderData[0];
            string address = orderData[1];

            // Lấy giỏ hàng từ session
            var cartItems = HttpContext.Session.GetObjectFromJson<List<CartItem>>("PendingOrder") ?? new List<CartItem>();

            if (vnpResponseCode == "00") // Thanh toán thành công
            {
                Message = "Payment successful! Redirecting to order history...";

                var newOrder = new Order
                {
                    UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)),
                    CreatedAt = DateTime.Now,
                    TotalPrice = amount,
                    PaymentMethod = "VNPay",
                    Status = "Pending",
                    Phone = phone,
                    Address = address
                };

                await _orderService.AddOrderAsync(newOrder, cartItems);

                // Xóa session sau khi đặt hàng thành công
                HttpContext.Session.Remove("PendingOrder");
                return Page();
            }
            else
            {
                Message = "Payment failed. Please try again.";
                return RedirectToPage("/Customer/Checkout");
            }
        }

    }
}
