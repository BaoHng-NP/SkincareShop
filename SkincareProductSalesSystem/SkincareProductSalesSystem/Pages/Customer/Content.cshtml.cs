using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.BLL.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkincareProductSalesSystem.Pages.Customer
{
    public class ContentModel : PageModel
    {
        private readonly IContentService _contentService;

        public ContentModel(IContentService contentService)
        {
            _contentService = contentService;
        }

        public List<Content> Contents { get; set; }
        public List<Content> LatestContents { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; } = 6; // Display 6 blogs per page

        public async Task<IActionResult> OnGetAsync(int pageNumber = 1)
        {
            if (pageNumber < 1)
                pageNumber = 1;

            CurrentPage = pageNumber;

            Contents = await _contentService.GetPublishedContents(CurrentPage, PageSize);
            LatestContents = await _contentService.GetLatestContents(3);

            int totalContents = await _contentService.GetTotalPublishedContents();
            TotalPages = (int)Math.Ceiling(totalContents / (double)PageSize);

            return Page();
        }
    }
}
