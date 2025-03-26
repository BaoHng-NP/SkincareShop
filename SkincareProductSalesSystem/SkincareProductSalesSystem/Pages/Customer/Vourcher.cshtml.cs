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
using System.Security.Claims;
using Mono.TextTemplating;

namespace SkincareProductSalesSystem.Pages.Customer
{
    public class VourcherModel : PageModel
    {
        private readonly IUserVourcherService _userVourcherService;
        private readonly IDiscountService _discountService;
        private readonly IAccountService _accountService;
        public VourcherModel(IUserVourcherService userVourcherService, IDiscountService discountService, IAccountService accountService)
        {
            _userVourcherService = userVourcherService;
            _discountService = discountService;
            _accountService = accountService;
        }

        public IEnumerable<UserVoucher> UserVoucher { get;set; } = default!;
        public IEnumerable<Discount> Discounts { get; set; } = default!;
        public User UserInfo { get; set; } = default!;

        public async Task OnGetAsync()
        {
            int userId = int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int id) ? id : 0;
            UserVoucher = await _userVourcherService.GetUserVoucherByUserIdAsync(userId);
            Discounts = await _discountService.GetAllDiscountsAsync();
            UserInfo = await _accountService.GetAccountByIdAsync(userId);
        }
        public async Task<IActionResult> OnPostAsync(int discountId)
        {
            var discount = await _discountService.GetDiscountByIdAsync(discountId);
            int userId = int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int id) ? id : 0;
            UserVoucher = await _userVourcherService.GetUserVoucherByUserIdAsync(userId);
            Discounts = await _discountService.GetAllDiscountsAsync();
            UserInfo = await _accountService.GetAccountByIdAsync(userId);
            if (discount == null)
            {
                ModelState.AddModelError("", "Invalid discount.");
                return Page();
            }

            if (discount.RequiredPoints > UserInfo.LoyaltyPoints)
            {
                TempData["ErrorMessage"] = "Not enough Loyalty Points.!";
                return Page();
            }

            UserInfo.LoyaltyPoints -= discount.RequiredPoints;
            await _accountService.UpdateAccountAsync(UserInfo);

            var newUserVoucher = new UserVoucher
            {
                UserId = UserInfo.Id,
                DiscountId = discountId,
                RedeemedAt = DateTime.Now,
            };

            await _userVourcherService.AddUserVoucherAsync(newUserVoucher);
            TempData["SuccessMessage"] = "Voucher exchanged successfully!";

            return RedirectToPage(); 
        }


    }
}
