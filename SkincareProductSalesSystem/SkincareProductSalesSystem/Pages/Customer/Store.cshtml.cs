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
using SkincareProductSalesSystem.Helpers;
using Microsoft.AspNetCore.SignalR;
using FUNewsManagementSystem.Hubs;

namespace SkincareProductSalesSystem.Pages.Customer
{
    public class StoreModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _brandService;

        public StoreModel(IProductService productService, ICategoryService categoryService, IBrandService brandService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _brandService = brandService;
        }
        public IEnumerable<Category> Category { get; set; } = default!;
        public IEnumerable<Brand> Brand { get; set; } = default!;
        public IEnumerable<Product> Product { get;set; } = default!;
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }


        public async Task<IActionResult> OnGetAsync(int pageNumber = 1, int? categoryId = null, int? brandId = null)
        {
            Category = await _categoryService.GetAllActiveCategoryAsync();
            if (Category == null)
            {
                // Xử lý trường hợp Category là null
                Category = new List<Category>();
            }
            Brand = await _brandService.GetAllActiveBrandAsync();
            if (Brand == null)
            {
                // Xử lý trường hợp Brand là null
                Brand = new List<Brand>();
            }

            int pageSize = 9; // Số sản phẩm mỗi trang

            // Lấy sản phẩm có lọc theo category và brand
            var products = await _productService.GetProductsByFilterAsync(categoryId, brandId);

            if (products == null || !products.Any())
            {
                ViewData["NoProductsMessage"] = "No products available.";
                Product = new List<Product>(); // Để tránh lỗi NullReferenceException
            }
            else
            {
                int totalProducts = products.Count();
                TotalPages = (int)Math.Ceiling(totalProducts / (double)pageSize);
                CurrentPage = pageNumber;
                Product = products.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            }

            return Page();
        }


        public IActionResult OnPostAddToCart([FromBody] CartItem cartItem)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
            {
                return new JsonResult(new { success = false, message = "Unauthorized" });
            }

            string userId = User.Identity.Name;
            string cartKey = $"Cart_{userId}";

            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>(cartKey) ?? new List<CartItem>();

            var item = cart.FirstOrDefault(p => p.ProductId == cartItem.ProductId);
            if (item != null)
            {
                    item.Quantity += cartItem.Quantity;

                }
            else
            {
                cart.Add(new CartItem
                {
                    ProductId = cartItem.ProductId,
                    ProductName = cartItem.ProductName,
                    Price = cartItem.Price,
                    Quantity = cartItem.Quantity,
                    ImageUrl = cartItem.ImageUrl
                });
            }

            HttpContext.Session.SetObjectAsJson(cartKey, cart);

            return new JsonResult(new { success = true, cartCount = cart.Sum(p => p.Quantity) });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, message = ex.Message });
            }
        }

    }
}
