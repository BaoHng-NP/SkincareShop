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
    public class DetailsModel : PageModel
    {
        private readonly System.DAL.SkincareShopContext _context;

        public DetailsModel(System.DAL.SkincareShopContext context)
        {
            _context = context;
        }

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
    }
}
