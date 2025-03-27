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
using System.BLL.Services;

namespace SkincareProductSalesSystem.Pages.Manager.Accounts
{
    public class EditModel : PageModel
    {
        private readonly IAccountService _accountService;

        public EditModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [BindProperty]
        public User User { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
           

            var user =  await _accountService.GetAccountByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            User = user;

            return Page();
        }

     
        public async Task<IActionResult> OnPostAsync()
        {
            
            await _accountService.UpdateAccountAsync(User);
            return RedirectToPage("./Index");
        }

      
    }
}
