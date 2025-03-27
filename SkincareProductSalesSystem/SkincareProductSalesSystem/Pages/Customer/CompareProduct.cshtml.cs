using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.BLL.Services;
using System.Security.Claims;

namespace SkincareProductSalesSystem.Pages.Customer
{
    public class CompareProductModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly IAccountService _accountService;
        public CompareProductModel(IProductService productService, IAccountService accountService)
        {
            _productService = productService;
            _accountService = accountService;
        }
        public Product Product1 { get; set; }
        public Product Product2 { get; set; }
        public User? UserInfo { get; set; }
        public async Task<IActionResult> OnGet(int product1, int product2)
        {
            Product1 = await _productService.GetProductByIdAsync(product1);
            Product2 = await _productService.GetProductByIdAsync(product2);


            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (int.TryParse(userIdClaim, out var userId) && userId > 0)
            {
                UserInfo = await _accountService.GetAccountByIdAsync(userId);
            }
         
            if (Product1 == null || Product2 == null)
            {
                return RedirectToPage("/Store");
            }

            return Page();
        }

    }
}

