using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects.Models;
using System.BLL.Services;
using System.Security.Claims;

namespace SkincareProductSalesSystem.Pages.Customer
{
    public class SkinTestModel : PageModel
    {
        private readonly ISkinQuizQuestionService _skinQuizQuestionService;
        private readonly IUserSkinTestService _userSkinTestService;
        private readonly IAccountService _accountService;
        private readonly ISkinQuizAnswerService _skinQuizAnswerService;
        private readonly ISkinTypeService _skinTypeService;
        public SkinTestModel(ISkinQuizQuestionService skinQuizQuestionService, IUserSkinTestService userSkinTestService, IAccountService accountService, ISkinQuizAnswerService skinQuizAnswerService, ISkinTypeService skinTypeService)
        {
            _skinQuizQuestionService = skinQuizQuestionService;
            _userSkinTestService = userSkinTestService;
            _accountService = accountService;
            _skinQuizAnswerService = skinQuizAnswerService;
            _skinTypeService = skinTypeService;
        }

        public IEnumerable<SkinQuizQuestion> Questions { get; private set; } = Enumerable.Empty<SkinQuizQuestion>();
        public SkinType? ResultSkinType { get; private set; }

        [BindProperty]
        public Dictionary<int, int> SelectedAnswers { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            Questions = await _skinQuizQuestionService.GetAllQuestionsAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (SelectedAnswers.Count == 0)
            {
                ModelState.AddModelError("", "You must answer all questions.");
                return Page();
            }
         

            int skinTypeId = await _skinQuizAnswerService.DetermineSkinType(SelectedAnswers.Values);
            HttpContext.Session.SetInt32("SkinTypeId", skinTypeId);
            ResultSkinType = await _skinTypeService.GetSkinTypeByIdAsync(skinTypeId);

            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (int.TryParse(userIdClaim, out var userId) && userId > 0)
            {
                var newUserSkinTest = new UserSkinTest
                {
                    UserId = userId,
                    SkinTypeId = skinTypeId,  
                    CreatedAt = DateTime.UtcNow
                };
                await _userSkinTestService.AddUserSkinTestAsync(newUserSkinTest);
                var user = await _accountService.GetAccountByIdAsync(userId);
                if (user != null)
                {
                    user.SkinTypeId = skinTypeId;
                    await _accountService.UpdateAccountAsync(user);
                }
            }

            return Page();
        }
    }

}
