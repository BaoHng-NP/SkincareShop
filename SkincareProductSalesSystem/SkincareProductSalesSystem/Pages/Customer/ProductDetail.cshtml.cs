using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.BLL.Services;

namespace SkincareProductSalesSystem.Pages.Customer
{
    public class ProductDetailModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly IFeedbackService _feedbackService;

        public ProductDetailModel(IProductService productService, IFeedbackService feedbackService)
        {
            _productService = productService;
            _feedbackService = feedbackService;
        }

        public Product Product { get; set; }
        public IList<Feedback> Feedbacks { get; set; }
        public IEnumerable<Product> CompareList { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Product = await _productService.GetProductByIdAsync(id);
            CompareList = await _productService.GetAllProductsByCateIdAsync(cateId: Product.CategoryId, Product.Id);
            Feedbacks = await _feedbackService.GetFeedbackByProductIdAsync(id);

            if (Product == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
