using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects.Models;
using System.BLL.Services;

namespace SkincareProductSalesSystem.Pages.Staff.Products
{
    public class AddModel : PageModel
    {
        private readonly IProductDetailService _productDetailService;
        private readonly IProductService _productService;

        public AddModel(IProductDetailService productDetailService, IProductService productService)
        {
            _productDetailService = productDetailService;
            _productService = productService;
        }

        [BindProperty]
        public ProductDetail ProductDetail { get; set; } = default!;

        [BindProperty]
        public int ProductId { get; set; } // Lưu ID của sản phẩm

        public Product Product { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {
            Product = await _productService.GetProductByIdAsync(id);
            if (Product == null)
            {
                return NotFound();
            }

            ProductId = id; // Lưu ID sản phẩm vào biến
            var products = await _productService.GetAllProductsAsync();
            ViewData["ProductId"] = new SelectList(products.ToList(), "Id", "Name");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Lấy thông tin sản phẩm theo ID
            var product = await _productService.GetProductByIdAsync(ProductId);
            if (product == null)
            {
                return NotFound();
            }

            // Cập nhật số lượng tồn kho
            product.Stock += ProductDetail.Quantity;

            // Gán ProductId vào ProductDetail trước khi lưu
            ProductDetail.ProductId = ProductId;

            // Lưu vào database
            await _productDetailService.AddPoductDetailServiceAsync(ProductDetail);
            await _productService.UpdateProductAsync(product);

            return RedirectToPage("./Index");
        }
    }
}
