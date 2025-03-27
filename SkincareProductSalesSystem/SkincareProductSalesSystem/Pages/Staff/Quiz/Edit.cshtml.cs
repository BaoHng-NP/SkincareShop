using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using System.DAL;

namespace SkincareProductSalesSystem.Pages.Staff.Quiz
{
    public class EditModel : PageModel
    {
        private readonly System.DAL.SkincareShopContext _context;

        public EditModel(System.DAL.SkincareShopContext context)
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

            var skinquizanswer =  await _context.SkinQuizAnswers.FirstOrDefaultAsync(m => m.Id == id);
            if (skinquizanswer == null)
            {
                return NotFound();
            }
            SkinQuizAnswer = skinquizanswer;
           ViewData["QuestionId"] = new SelectList(_context.SkinQuizQuestions, "Id", "Question");
           ViewData["SkinTypeId"] = new SelectList(_context.SkinTypes, "Id", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(SkinQuizAnswer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkinQuizAnswerExists(SkinQuizAnswer.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool SkinQuizAnswerExists(int id)
        {
            return _context.SkinQuizAnswers.Any(e => e.Id == id);
        }
    }
}
