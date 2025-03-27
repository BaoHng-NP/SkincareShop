using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects.Models;
using System.DAL;
using System.BLL.Services;

namespace SkincareProductSalesSystem.Pages.Staff.Products
{
    public class CreateModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly IBrandService _brandService;
        private readonly ICategoryService _categoryService;
        public CreateModel(IProductService productService, IBrandService brandService, ICategoryService categoryService)
        {
            _productService = productService;
            _brandService = brandService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var brand = await _brandService.GetAllActiveBrandAsync();
            var category = await _categoryService.GetAllActiveCategoryAsync();

            ViewData["BrandId"] = new SelectList(brand.ToList(), "Id", "BrandName");
            ViewData["CategoryId"] = new SelectList(category.ToList(), "Id", "Name");

            return Page();
        }


        [BindProperty]
        public Product Product { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _productService.AddProductAsync(Product);

            return RedirectToPage("./Index");
        }
    }
}
