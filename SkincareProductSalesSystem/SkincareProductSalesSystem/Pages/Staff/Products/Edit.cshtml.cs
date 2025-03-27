using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using System.DAL;
using System.BLL.Services;

namespace SkincareProductSalesSystem.Pages.Staff.Products
{
    public class EditModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly IBrandService _brandService;
        private readonly ICategoryService _categoryService;
        public EditModel(IProductService productService, IBrandService brandService, ICategoryService categoryService)
        {
            _productService = productService;
            _brandService = brandService;
            _categoryService = categoryService;
        }
        [BindProperty]
        public Product Product { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product =  await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            Product = product;
            var brand = await _brandService.GetAllActiveBrandAsync();
            var category = await _categoryService.GetAllActiveCategoryAsync();

            ViewData["BrandId"] = new SelectList(brand.ToList(), "Id", "BrandName");
            ViewData["CategoryId"] = new SelectList(category.ToList(), "Id", "Name");

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            await _productService.UpdateProductAsync(Product);

            return RedirectToPage("./Index");
        }

    }
}
