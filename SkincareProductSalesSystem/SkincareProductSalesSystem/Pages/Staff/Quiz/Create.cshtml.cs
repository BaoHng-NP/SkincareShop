using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects.Models;
using System.DAL;

namespace SkincareProductSalesSystem.Pages.Staff.Quiz
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
        ViewData["QuestionId"] = new SelectList(_context.SkinQuizQuestions, "Id", "Question");
        ViewData["SkinTypeId"] = new SelectList(_context.SkinTypes, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public SkinQuizAnswer SkinQuizAnswer { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.SkinQuizAnswers.Add(SkinQuizAnswer);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
