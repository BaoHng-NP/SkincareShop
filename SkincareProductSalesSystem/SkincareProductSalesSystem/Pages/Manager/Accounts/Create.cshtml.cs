using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects.Models;
using System.DAL;
using System.BLL.Services;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Security.Cryptography;

namespace SkincareProductSalesSystem.Pages.Manager.Accounts
{
    public class CreateModel : PageModel
    {
        private readonly IAccountService _accountService;

        public CreateModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [BindProperty]
        public RegisterInputModel Input { get; set; } = new();

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Input.Password != Input.ConfirmPassword)
            {
                ModelState.AddModelError("Input.ConfirmPassword", "Passwords do not match.");
                return Page();
            }

            string hashedPassword = HashPassword(Input.Password);

            var user = new User
            {
                FullName = Input.FullName,
                Email = Input.Email,
                Phone = Input.Phone,
                Address = Input.Address,
                Birthdate = Input.BirthDate,
                PasswordHash = hashedPassword,
                RoleId = Input.RoleId,
            };

            await _accountService.AddAccountAsync(user);
            return RedirectToPage("/Manager/Accounts/Index");
        }

        // Hàm hash mật khẩu
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }

    public class RegisterInputModel
    {
        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required]
        [Phone]
        [MinLength(9, ErrorMessage = "Phone must be at least 9 number.")]
        [Display(Name = "Phone Number")]
        public string Phone { get; set; } = string.Empty;
        [Required]
        public DateOnly BirthDate { get; set; }
        [Required]
        public int? RoleId { get; set; }
        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; } = string.Empty;
    }
}
