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
    public class FeedbackModel : PageModel
    {
        private readonly IFeedbackService _feedbackService;
        private readonly IOrderService _orderService;
        private readonly IAccountService _accountService;

        public FeedbackModel(IFeedbackService feedbackService, IOrderService orderService, IAccountService accountService)
        {
            _feedbackService = feedbackService ?? throw new ArgumentNullException(nameof(feedbackService));
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
            _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
        }

        public Order? Order { get; private set; }
        public bool IsFeedbackGiven { get; set; }
        public string? SuccessMessage { get; set; }

        [BindProperty]
        public List<Feedback> Feedbacks { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid order ID.");
            }

            Order = await _orderService.GetOrderByIdAsync(id);

            if (Order == null)
            {
                return NotFound("Order not found.");
            }

            if (Order.OrderItems == null || !Order.OrderItems.Any())
            {
                ModelState.AddModelError("", "No items found in the order.");
                return Page();
            }

            var existingFeedbacks = await _feedbackService.GetFeedbackByUserIdAsync(Order.UserId);

            IsFeedbackGiven = Order.OrderItems.All(item => item.ProductId.HasValue);

                Feedbacks = Order.OrderItems
                    .Where(item => item.ProductId.HasValue)
                    .Select(item => new Feedback
                    {
                        ProductId = item.ProductId ?? 0,
                        UserId = Order.UserId
                    })
                    .ToList();
            
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int orderId)
        {
            if (Feedbacks == null || !Feedbacks.Any())
            {
                ModelState.AddModelError("", "No feedback to submit.");
                return Page();
            }

            try
            {
                foreach (var feedback in Feedbacks)
                {
                    await _feedbackService.AddFeedbackAsync(feedback);
                }

                int loyaltyPoints = Feedbacks.Count * 10;
                var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (!int.TryParse(userIdClaim, out var userId) || userId <= 0)
                {
                    ModelState.AddModelError("", "Invalid User ID.");
                    return Page();
                }

                Order = await _orderService.GetOrderByIdAsync(orderId);
                if (Order == null)
                {
                    ModelState.AddModelError("", "Order not found.");
                    return Page();
                }
                var acc = await _accountService.GetAccountByIdAsync(userId);
                if (acc == null)
                {
                    ModelState.AddModelError("", "User account not found.");
                    return Page();
                }

                acc.LoyaltyPoints += loyaltyPoints;
                await _accountService.UpdateAccountAsync(acc);
                if (Order != null)
                {
                    Order.Status = "Completed-Feedback";
                    await _orderService.UpdateOrderAsync(Order);
                }

                TempData["SuccessMessage"] = $"🎉 Thank you for your feedback! You have earned {loyaltyPoints} loyalty points!";
                return RedirectToPage("/Customer/OrderHistory");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred while saving feedback: {ex.Message}");
                return Page();
            }
        }
    }
}
