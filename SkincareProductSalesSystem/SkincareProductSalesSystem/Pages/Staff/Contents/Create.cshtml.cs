using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects.Models;
using System.DAL;

namespace SkincareProductSalesSystem.Pages.Staff.Contents
{
    public class CreateModel : PageModel
    {
        private readonly System.DAL.SkincareShopContext _context;

        public CreateModel(System.DAL.SkincareShopContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["AuthorId"] = new SelectList(_context.Users, "Id", "Email");
            return Page();
        }

        [BindProperty]
        public Content Content { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Contents.Add(Content);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
