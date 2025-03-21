using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects.Models;
using System.BLL.Services;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace SkincareProductSalesSystem.Pages.Auth
{
    public class RegisterModel : PageModel
    {
        private readonly IAccountService _accountService;

        public RegisterModel(IAccountService accountService)
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

            // Hash mật khẩu
            string hashedPassword = HashPassword(Input.Password);

            // Tạo User mới
            var user = new User
            {
                FullName = Input.FullName,
                Email = Input.Email,
                Phone = Input.Phone,
                Address = Input.Address,
                PasswordHash = hashedPassword,
                RoleId = 2
            };

            // Lưu vào database
            await _accountService.AddAccountAsync(user);
            return RedirectToPage("/Auth/Login");
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

    // Model cho dữ liệu nhập vào form
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
        [Display(Name = "Address")]
        public string Address { get; set; } = string.Empty;
    }
}
