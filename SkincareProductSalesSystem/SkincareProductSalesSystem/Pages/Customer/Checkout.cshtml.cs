using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SkincareProductSalesSystem.Helpers;
using System.BLL.Services;
using System.Security.Claims;
using System.Globalization;
using System.Text;

namespace SkincareProductSalesSystem.Pages.Customer
{
    public class CheckoutModel : PageModel
    {
        private readonly IAccountService _accountService;
        private readonly IOrderService _orderService;
        private readonly IVnPayService _vnPayService;
        public CheckoutModel(IAccountService accountService, IOrderService orderService, IVnPayService vnpayService)
        {
            _accountService = accountService;
            _orderService = orderService;
            _vnPayService = vnpayService;
        }

        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
        public User UserInfo { get; set; } = default!;
        public decimal Subtotal { get; set; }
        [BindProperty]
        public PaymentInformationModel PaymentInfo { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {
            if (!User.Identity.IsAuthenticated)
            {
                CartItems = new List<CartItem>();
                return RedirectToPage("/Auth/Login");
            }

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userName = User.Identity.Name;

            if (string.IsNullOrEmpty(userId))
            {
                return NotFound();
            }

            var user = await _accountService.GetAccountByIdAsync(int.Parse(userId)); // Lấy thông tin user từ database

            if (user == null)
            {
                return NotFound();
            }

            UserInfo = user;

            string cartKey = $"Cart_{userName}";
            CartItems = HttpContext.Session.GetObjectFromJson<List<CartItem>>(cartKey) ?? new List<CartItem>();
            Subtotal = (decimal)CartItems.Sum(item => item.Price * item.Quantity);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Auth/Login");
            }

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userName = User.Identity.Name;

            if (string.IsNullOrEmpty(userId))
            {
                return NotFound();
            }

            UserInfo = await _accountService.GetAccountByIdAsync(int.Parse(userId));

            string cartKey = $"Cart_{userName}";
            CartItems = HttpContext.Session.GetObjectFromJson<List<CartItem>>(cartKey) ?? new List<CartItem>();
            Subtotal = (decimal)CartItems.Sum(item => item.Price * item.Quantity);

            if (Subtotal <= 0)
            {
                ModelState.AddModelError(string.Empty, "Your cart is empty.");
                return Page();
            }
            if (string.IsNullOrEmpty(PaymentInfo.Phone) || string.IsNullOrEmpty(PaymentInfo.Address))
            {
                ModelState.AddModelError(string.Empty, "Please fill all required fields.");
                return Page();
            }

            if (PaymentInfo.PaymentMethod == "Online")
            {
                HttpContext.Session.SetObjectAsJson("PendingOrder", CartItems);
                string paymentUrl = _vnPayService.CreatePaymentUrl(PaymentInfo, HttpContext);
                return Redirect(paymentUrl);
            }
            else if (PaymentInfo.PaymentMethod == "COD")
            {
                var newOrder = new Order
                {
                    UserId = UserInfo.Id,
                    CreatedAt = DateTime.Now,
                    TotalPrice = Subtotal,
                    PaymentMethod = "COD",
                    Status = "Pending",
                    Phone = PaymentInfo.Phone,
                    Address = PaymentInfo.Address
                };

                await _orderService.AddOrderAsync(newOrder, CartItems);
                HttpContext.Session.Remove(cartKey);

                return RedirectToPage("/Customer/OrderSuccess", new { orderId = newOrder.Id, userName = UserInfo.FullName });
            }

            return Page();
        }


        public IActionResult OnPostRemoveFromCart(int productId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Auth/Login");
            }

            string userId = User.Identity.Name;
            string cartKey = $"Cart_{userId}";

            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>(cartKey) ?? new List<CartItem>();
            cart.RemoveAll(p => p.ProductId == productId);

            HttpContext.Session.SetObjectAsJson(cartKey, cart);

            return RedirectToPage("/Customer/Checkout");
        }

        public static string RemoveDiacritics(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return text;
            }

            text = text.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();

            foreach (char c in text)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(c);
                }
            }

            return sb.ToString().Normalize(NormalizationForm.FormC);
        }

    }
}
