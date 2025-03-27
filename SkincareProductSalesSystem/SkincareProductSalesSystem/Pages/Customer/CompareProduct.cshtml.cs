using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.BLL.Services;

namespace SkincareProductSalesSystem.Pages.Customer
{
    public class CompareProductModel : PageModel
    {
        private readonly IProductService _productService;

        public CompareProductModel(IProductService productService)
        {
            _productService = productService;
        }
        public Product Product1 { get; set; }
        public Product Product2 { get; set; }

        public async Task<IActionResult> OnGet(int product1, int product2)
        {
            Product1 = await _productService.GetProductByIdAsync(product1);
            Product2 = await _productService.GetProductByIdAsync(product2);

            if (Product1 == null || Product2 == null)
            {
                return RedirectToPage("/Store");
            }

            return Page();
        }
    }
}

