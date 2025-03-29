using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using System.DAL;

namespace SkincareProductSalesSystem.Pages.Staff.Contents
{
    public class DeleteModel : PageModel
    {
        private readonly System.DAL.SkincareShopContext _context;

        public DeleteModel(System.DAL.SkincareShopContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Content Content { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var content = await _context.Contents.FirstOrDefaultAsync(m => m.Id == id);

            if (content == null)
            {
                return NotFound();
            }
            else
            {
                Content = content;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var content = await _context.Contents.FindAsync(id);
            if (content != null)
            {
                Content = content;
                _context.Contents.Remove(Content);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
