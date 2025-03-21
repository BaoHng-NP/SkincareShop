using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects.Models;
using SkincareProductSalesSystem.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace SkincareProductSalesSystem.Pages.Customer
{
    public class CartModel : PageModel
    {
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
        public decimal Subtotal { get; set; }

        public void OnGet()
        {
            if (!User.Identity.IsAuthenticated)
            {
                CartItems = new List<CartItem>();
                return;
            }

            string userId = User.Identity.Name;
            string cartKey = $"Cart_{userId}";
            CartItems = HttpContext.Session.GetObjectFromJson<List<CartItem>>(cartKey) ?? new List<CartItem>();
            Subtotal = (decimal)CartItems.Sum(item => item.Price * item.Quantity);

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

            return RedirectToPage("/Customer/Cart");
        }

        public IActionResult OnPostUpdateCart(int productId, int quantity)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Auth/Login");
            }

            string userId = User.Identity.Name;
            string cartKey = $"Cart_{userId}";

            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>(cartKey) ?? new List<CartItem>();

            var item = cart.FirstOrDefault(p => p.ProductId == productId);
            if (item != null && quantity > 0)
            {
                item.Quantity = quantity;
            }
            else
            {
                cart.RemoveAll(p => p.ProductId == productId);
            }

            HttpContext.Session.SetObjectAsJson(cartKey, cart);

            return RedirectToPage("/Customer/Cart");
        }

        [HttpPost]
        public JsonResult OnPostUpdateQuantity(int productId, string action)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return new JsonResult(new { success = false, message = "User not authenticated" });
            }

            string userId = User.Identity.Name;
            string cartKey = $"Cart_{userId}";

            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>(cartKey) ?? new List<CartItem>();

            var cartItem = cart.FirstOrDefault(c => c.ProductId == productId);
            if (cartItem != null)
            {
                if (action == "increase") cartItem.Quantity++;
                if (action == "decrease" && cartItem.Quantity > 1) cartItem.Quantity--;

                HttpContext.Session.SetObjectAsJson(cartKey, cart);

                decimal newTotal = (decimal)(cartItem.Quantity * cartItem.Price);
                return new JsonResult(new { success = true, newQuantity = cartItem.Quantity, newTotal });
            }

            return new JsonResult(new { success = false, message = "Item not found" });
        }


    }
}
