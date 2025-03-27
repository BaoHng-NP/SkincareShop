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
    public class IndexModel : PageModel
    {
        private readonly System.DAL.SkincareShopContext _context;

        public IndexModel(System.DAL.SkincareShopContext context)
        {
            _context = context;
        }

        public IList<SkinQuizAnswer> SkinQuizAnswer { get;set; } = default!;

        public async Task OnGetAsync()
        {
            SkinQuizAnswer = await _context.SkinQuizAnswers
                .Include(s => s.Question)
                .Include(s => s.SkinType).ToListAsync();
        }
    }
}
