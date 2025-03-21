using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.BLL.Services;

namespace SkincareProductSalesSystem.Pages.Customer
{
    public class RecommendedModel : PageModel
    {
        private readonly ISkinTypeService _skinTypeService;
        private readonly IProductService _productService;

        public RecommendedModel(ISkinTypeService skinTypeService, IProductService productService)
        {
            _skinTypeService = skinTypeService;
            _productService = productService;
        }

        [BindProperty(SupportsGet = true)]
        public int SkinTypeId { get; set; }

        public SkinType SkinType { get; set; }
        public List<SkinCareRoutine> SkinCareRoutine { get; set; } = new List<SkinCareRoutine>();
        public IEnumerable<Product> RecommendedProducts { get; set; }

        private List<SkinCareRoutine> GetSkinCareRoutineForSkinType(int skinTypeId)
        {
            return new List<SkinCareRoutine>
    {
        new SkinCareRoutine
        {
            StepOrder = 1,
            StepName = "Cleanser",
            Description = "Start your skincare routine with a gentle cleanser to remove dirt, oil, sweat, and other impurities that accumulate throughout the day. Cleansing is essential as it clears the skin, unclogs pores, and prepares it to absorb subsequent skincare products more effectively. Choose a cleanser that suits your skin type—hydrating for dry skin, foaming for oily skin, or a mild micellar water for sensitive skin.",
            Category = "Cleanser"
        },
        new SkinCareRoutine
        {
            StepOrder = 2,
            StepName = "Toner",
            Description = "After cleansing, apply a toner to restore the skin’s natural pH balance, remove any leftover residue, and provide a fresh, hydrated base for the next steps. Depending on your skin type, opt for a hydrating toner with ingredients like hyaluronic acid, or a clarifying toner with salicylic acid if you have oily or acne-prone skin.",
            Category = "Toner"
        },
        new SkinCareRoutine
        {
            StepOrder = 3,
            StepName = "Serum",
            Description = "Serums are packed with active ingredients designed to target specific skin concerns such as hydration, brightening, anti-aging, or acne control. A vitamin C serum helps with radiance and pigmentation, while a niacinamide or hyaluronic acid serum supports hydration and barrier repair. Apply a few drops and gently pat it into your skin.",
            Category = "Serum"
        },
        new SkinCareRoutine
        {
            StepOrder = 4,
            StepName = "Moisturizer",
            Description = "Lock in hydration and keep your skin soft, smooth, and protected with a moisturizer. A lightweight, gel-based moisturizer works well for oily or combination skin, while richer creams are best for dry or mature skin. Moisturizers also help maintain the skin’s protective barrier and prevent transepidermal water loss.",
            Category = "Moisturizer"
        },
        new SkinCareRoutine
        {
            StepOrder = 5,
            StepName = "Sunscreen",
            Description = "Sunscreen is a non-negotiable step to protect your skin from harmful UV rays, preventing premature aging, sun damage, and hyperpigmentation. Use a broad-spectrum sunscreen with SPF 30 or higher every morning, even on cloudy days, and reapply every two hours if you’re exposed to direct sunlight.",
            Category = "Sunscreen"
        },
        new SkinCareRoutine
        {
            StepOrder = 6,
            StepName = "Essence",
            Description = "An essence is a lightweight, hydrating liquid that preps the skin for better absorption of serums and moisturizers. It typically contains nourishing ingredients that support cell regeneration and hydration, making it an essential step in many Korean skincare routines.",
            Category = "Essence"
        },
        new SkinCareRoutine
        {
            StepOrder = 7,
            StepName = "Exfoliator",
            Description = "Exfoliation removes dead skin cells, unclogs pores, and helps improve skin texture. Use a chemical exfoliant (like AHAs or BHAs) 2-3 times a week for smoother, more radiant skin. Physical scrubs can also be used, but avoid harsh scrubs that may damage the skin barrier.",
            Category = "Exfoliator"
        },
        new SkinCareRoutine
        {
            StepOrder = 8,
            StepName = "Eye Cream",
            Description = "The skin around the eyes is thinner and more delicate than the rest of the face, making it prone to dryness, fine lines, and puffiness. Apply a small amount of eye cream using your ring finger to gently tap around the orbital bone, helping to reduce dark circles and keep the area hydrated.",
            Category = "Eye Cream"
        },
        new SkinCareRoutine
        {
            StepOrder = 9,
            StepName = "Face Mask",
            Description = "Face masks provide an extra boost to your skincare routine, delivering intense hydration, detoxification, or nourishment based on your skin's needs. Sheet masks are great for instant hydration, clay masks help with oil control, and overnight masks lock in moisture while you sleep. Use 1-2 times per week.",
            Category = "Face Mask"
        },
        new SkinCareRoutine
        {
            StepOrder = 10,
            StepName = "Night Cream",
            Description = "A night cream is richer than a regular moisturizer and is designed to support the skin’s overnight repair process. Many night creams contain peptides, retinol, or ceramides to help rejuvenate and restore the skin while you sleep, leaving you with a smoother, more refreshed complexion in the morning.",
            Category = "Night Cream"
        }
    };
        }


        public async Task<IActionResult> OnGetAsync()
        {
            // Get Skin Type Details
            SkinType = await _skinTypeService.GetSkinTypeByIdAsync(SkinTypeId);
            if (SkinType == null)
            {
                return NotFound();
            }

            // Get all products suitable for this Skin Type
            var allProducts = await _productService.GetProductsBySkinTypeAsync(SkinTypeId) ?? new List<Product>();

            // Load static skincare routine steps
            SkinCareRoutine = GetSkinCareRoutineForSkinType(SkinTypeId);

            // Map products to each skincare step based on category
            foreach (var step in SkinCareRoutine)
            {
                step.RecommendedProducts = allProducts
                    .Where(p => p.Category != null && p.Category.Name == step.Category)
                    .ToList();
            }

            return Page();
        }



    }
}
