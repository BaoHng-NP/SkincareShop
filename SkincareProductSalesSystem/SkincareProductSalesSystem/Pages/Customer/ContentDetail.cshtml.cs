using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.BLL.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkincareProductSalesSystem.Pages.Customer
{
    public class ContentDetailModel : PageModel
    {
        private readonly IContentService _contentService;

        public ContentDetailModel(IContentService contentService)
        {
            _contentService = contentService;
        }

        public Content BlogPost { get; set; }
        public List<Content> RelatedPosts { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            BlogPost = await _contentService.GetContentById(id);

            if (BlogPost == null || BlogPost.IsPublished != true)
            {
                return NotFound();
            }

            // Get related/latest posts
            RelatedPosts = await _contentService.GetLatestContents(3);

            return Page();
        }
    }
}