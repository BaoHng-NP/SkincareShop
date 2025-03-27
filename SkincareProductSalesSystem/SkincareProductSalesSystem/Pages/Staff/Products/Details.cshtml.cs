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

namespace SkincareProductSalesSystem.Pages.Staff.Products
{
    public class DetailsModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly IProductDetailService _productDetailService;
        public DetailsModel(IProductService productService, IProductDetailService productDetailService)
        {
            _productService = productService;
            _productDetailService = productDetailService;
        }

        public Product Product { get; set; } = default!;
        public IList<ProductDetail> Detail { get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productService.GetProductByIdAsync(id);
            Detail = await _productDetailService.GetPoductDetailServiceByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            else
            {
                Product = product;
            }
            return Page();
        }
    }
}
