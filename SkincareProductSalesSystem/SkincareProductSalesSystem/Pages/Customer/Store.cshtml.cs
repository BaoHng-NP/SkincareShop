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

namespace SkincareProductSalesSystem.Pages.Customer
{
    public class StoreModel : PageModel
    {
        private readonly IProductService _productService;

        public StoreModel(IProductService productService)
        {
            _productService = productService;
        }

        public IEnumerable<Product> Product { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Product = await _productService.GetAllProductsAsync();
        }
    }
}
