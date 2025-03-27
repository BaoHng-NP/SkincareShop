using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using System.DAL;

namespace SkincareProductSalesSystem.Pages.Staff.Quiz
{
    public class DeleteModel : PageModel
    {
        private readonly System.DAL.SkincareShopContext _context;

        public DeleteModel(System.DAL.SkincareShopContext context)
        {
            _context = context;
        }

        [BindProperty]
        public SkinQuizAnswer SkinQuizAnswer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skinquizanswer = await _context.SkinQuizAnswers.FirstOrDefaultAsync(m => m.Id == id);

            if (skinquizanswer == null)
            {
                return NotFound();
            }
            else
            {
                SkinQuizAnswer = skinquizanswer;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skinquizanswer = await _context.SkinQuizAnswers.FindAsync(id);
            if (skinquizanswer != null)
            {
                SkinQuizAnswer = skinquizanswer;
                _context.SkinQuizAnswers.Remove(SkinQuizAnswer);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
