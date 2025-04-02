using Microsoft.AspNetCore.Mvc.RazorPages;
using System.BLL.Services;
using System.Collections.Generic;
using System.DAL.Repositories;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace SkincareProductSalesSystem.Pages.Manager.Dashboard
{
    public class DashboardModel : PageModel
    {
        private readonly DashboardService _dashboardService;

        public DashboardModel(DashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public List<OrderStatistic> OrderStatistics { get; set; }
        public OrderCompletionRate OrderCompletionRate { get; set; }
        public List<UserStatistic> UserStatistics { get; set; }

        public int TotalUsers { get; set; }
        public decimal TotalRevenue { get; set; }
        public List<TopSellingProductDetail> TopSellingProducts { get; set; }
        public List<RevenueByMonth> MonthlyRevenue { get; set; }
        public List<ProductStockStatus> LowStockProducts { get; set; }
        public List<ProductStockStatus> OutOfStockProducts { get; set; }

        // Daily data for smaller date ranges
        public List<OrderStatistic> DailyOrderStatistics { get; set; }
        public List<RevenueByDay> DailyRevenue { get; set; }
        public List<UserStatistic> DailyUserStatistics { get; set; }
        public bool ShowDailyData { get; set; }

        [BindProperty(SupportsGet = true)]
        public string TimePeriod { get; set; } = "12months";

        [BindProperty(SupportsGet = true)]
        public int Year { get; set; } = DateTime.Now.Year;

        [BindProperty(SupportsGet = true)]
        public DateTime? StartDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? EndDate { get; set; }

        public List<int> AvailableYears { get; set; } = new List<int>();

        public async Task OnGetAsync()
        {
            try
            {
                // Get available years for filter dropdowns
                AvailableYears = await _dashboardService.GetAvailableDataYears();

                // If no year selected but years are available, use the most recent
                if (Year == 0 && AvailableYears.Any())
                {
                    Year = AvailableYears.First();
                }

                // Get date range
                DateTime startDate, endDate;

                if (TimePeriod == "custom" && StartDate.HasValue && EndDate.HasValue)
                {
                    startDate = StartDate.Value;
                    endDate = EndDate.Value;
                }
                else if (TimePeriod == "year")
                {
                    // For year selection, show the entire year
                    startDate = new DateTime(Year, 1, 1);
                    endDate = new DateTime(Year, 12, 31);
                }
                else // "12months" or default
                {
                    // Last 12 months from current date
                    DateTime now = DateTime.Now;
                    startDate = new DateTime(now.Year, now.Month, 1).AddMonths(-11);
                    endDate = new DateTime(now.Year, now.Month, DateTime.DaysInMonth(now.Year, now.Month));
                }

                // Set the date values for display in the UI
                StartDate = startDate;
                EndDate = endDate;

                // Determine if we should show daily data (for date ranges less than a month)
                ShowDailyData = _dashboardService.ShouldUseDailyData(startDate, endDate);
                ViewData["ShowDailyData"] = ShowDailyData ? true : (object)null;

                // Get filtered data
                if (ShowDailyData)
                {
                    // Get daily data for smaller date ranges
                    DailyOrderStatistics = await _dashboardService.GetDailyOrderStatisticsByDateRange(startDate, endDate);
                    DailyRevenue = await _dashboardService.GetDailyRevenueByDateRange(startDate, endDate);
                    DailyUserStatistics = await _dashboardService.GetDailyUserStatisticsByDateRange(startDate, endDate);

                    // Still load monthly data for the UI components that don't have daily equivalents
                    OrderStatistics = await _dashboardService.GetOrderStatisticsByDateRange(startDate, endDate);
                    MonthlyRevenue = await _dashboardService.GetMonthlyRevenueByDateRange(startDate, endDate);
                    UserStatistics = await _dashboardService.GetUserStatisticsByDateRange(startDate, endDate);
                }
                else
                {
                    // Get monthly data for larger date ranges
                    OrderStatistics = await _dashboardService.GetOrderStatisticsByDateRange(startDate, endDate);
                    MonthlyRevenue = await _dashboardService.GetMonthlyRevenueByDateRange(startDate, endDate);
                    UserStatistics = await _dashboardService.GetUserStatisticsByDateRange(startDate, endDate);
                }

                // Get data that's not affected by monthly/daily switch
                OrderCompletionRate = await _dashboardService.GetOrderCompletionRateByDateRange(startDate, endDate);
                TotalUsers = await _dashboardService.GetTotalUsers();
                TotalRevenue = await _dashboardService.GetTotalRevenueByDateRange(startDate, endDate);
                TopSellingProducts = await _dashboardService.GetTopSellingProductsByDateRange(startDate, endDate, 5);
                LowStockProducts = await _dashboardService.GetLowStockProducts(10, 5);
                OutOfStockProducts = await _dashboardService.GetOutOfStockProducts(5);
            }
            catch (Exception ex)
            {
                // Error handling...
            }
        }
    }
}
