using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using System.Threading.Tasks;
using System.BLL.Services;
using BusinessObjects.Models;
using System.Text;
using System.Security.Cryptography;

namespace SkincareProductSalesSystem.Pages.Auth
{
    public class ChangePasswordModel : PageModel
    {
        private readonly IAccountService _accountService;

        public ChangePasswordModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [BindProperty]
        public ChangePasswordInputModel Input { get; set; } = new ChangePasswordInputModel();

        public class ChangePasswordInputModel
        {
            [Required]
            [DataType(DataType.Password)]
            public string CurrentPassword { get; set; } = string.Empty;

            [Required]
            [DataType(DataType.Password)]
            [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
            public string NewPassword { get; set; } = string.Empty;

            [Required]
            [DataType(DataType.Password)]
            [Compare("NewPassword", ErrorMessage = "Passwords do not match.")]
            public string ConfirmPassword { get; set; } = string.Empty;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdClaim, out int userId))
            {
                ModelState.AddModelError(string.Empty, "User not found.");
                return Page();
            }

            var user = await _accountService.GetAccountByIdAsync(userId);
            if (user == null || !VerifyPassword(Input.CurrentPassword, user.PasswordHash))
            {
                ModelState.AddModelError(string.Empty, "Current password is incorrect.");
                return Page();
            }
            string hashedPassword = HashPassword(Input.NewPassword);
            user.PasswordHash = hashedPassword; 
            await _accountService.UpdateAccountAsync(user);

            TempData["SuccessMessage"] = "Password changed successfully!";
            return RedirectToPage("/Auth/Profile");
        }
        private bool VerifyPassword(string enteredPassword, string storedHash)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] enteredBytes = Encoding.UTF8.GetBytes(enteredPassword);
                byte[] enteredHash = sha256.ComputeHash(enteredBytes);
                return Convert.ToBase64String(enteredHash) == storedHash;
            }
        }
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
}
