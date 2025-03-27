using Microsoft.AspNetCore.Mvc.RazorPages;
using System.BLL.Services;
using System.Collections.Generic;
using System.DAL.Repositories;
using System.Threading.Tasks;

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
        public List<BestSellingProduct> BestSellingProducts { get; set; }
        public List<UserStatistic> UserStatistics { get; set; }

        // Thêm các property mới
        public int TotalUsers { get; set; }
        public decimal TotalRevenue { get; set; }
        public List<TopSellingProductDetail> TopSellingProducts { get; set; }
        public List<RevenueByMonth> MonthlyRevenue { get; set; }

        public async Task OnGetAsync()
        {
            OrderStatistics = await _dashboardService.GetOrderStatistics();
            OrderCompletionRate = await _dashboardService.GetOrderCompletionRate();
            BestSellingProducts = await _dashboardService.GetBestSellingProducts(5);
            UserStatistics = await _dashboardService.GetUserStatistics();

            // Thêm dữ liệu mới
            TotalUsers = await _dashboardService.GetTotalUsers();
            TotalRevenue = await _dashboardService.GetTotalRevenue();
            TopSellingProducts = await _dashboardService.GetTopSellingProducts(5);
            MonthlyRevenue = await _dashboardService.GetMonthlyRevenue();
        }
    }
}
