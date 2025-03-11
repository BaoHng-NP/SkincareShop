using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using System.DAL;
using System.BLL.Services;
using Microsoft.AspNetCore.Authorization;

namespace SkincareProductSalesSystem.Pages.Manager.Accounts
{
    [Authorize(Policy = "ManagerOnly")]
    public class IndexModel : PageModel
    {
        private readonly IAccountService _accountService;

        public IndexModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public IEnumerable<User> User { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public int? RoleFilter { get; set; }

        public async Task OnGetAsync()
        {
            var allUsers = await _accountService.GetAllAccountsAsync();

            if (RoleFilter.HasValue)
            {
                User = allUsers.Where(u => u.RoleId == RoleFilter.Value).ToList();
            }
            else
            {
                User = allUsers;
            }
        }
    }
}
