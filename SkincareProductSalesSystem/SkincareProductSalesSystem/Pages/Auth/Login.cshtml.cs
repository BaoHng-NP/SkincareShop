using System.BLL.Services;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace FUNewsManagementSystem.Pages.Auth
{
    public class LoginModel : PageModel
    {
        private readonly IAccountService _accountService;
        private readonly IConfiguration _config;

        public LoginModel(IAccountService accountService, IConfiguration config)
        {
            _accountService = accountService;
            _config = config;
        }

        [BindProperty]
        public LoginInputModel Input { get; set; } = new();

        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var defaultManagerEmail = _config["DefaultAccount:Email"];
            var defaultManagerPassword = _config["DefaultAccount:Password"];

            if (Input.Email == defaultManagerEmail && Input.Password == defaultManagerPassword)
            {
                var adminClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "Default Manager"),
                    new Claim(ClaimTypes.Email, defaultManagerEmail),
                    new Claim(ClaimTypes.Role, "Manager")
                };

                var adminIdentity = new ClaimsIdentity(adminClaims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(adminIdentity));

                return RedirectToPage("/Manager/Accounts/Index");
            }

            var user = (await _accountService.GetAllAccountsAsync())
                .FirstOrDefault(u => u.Email == Input.Email);

            if (user == null || !VerifyPassword(Input.Password, user.PasswordHash))
            {
                ErrorMessage = "Invalid credentials!";
                return Page(); 
            }

            var role = user.RoleId switch
            {
                1 => "Staff",
                2 => "Customer"
            };

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FullName ?? ""),
                new Claim(ClaimTypes.Email, user.Email ?? ""),
                new Claim(ClaimTypes.Role, role)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return role switch
            {
                "Staff" => RedirectToPage("/NewsArticles/Index"),
                "Customer" => RedirectToPage("/Index"),            
            };
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

        public class LoginInputModel
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }
    }
}
