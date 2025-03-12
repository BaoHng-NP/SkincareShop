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
    }
}
