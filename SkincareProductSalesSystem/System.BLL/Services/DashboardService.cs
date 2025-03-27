using System.Collections.Generic;
using System.Threading.Tasks;
using System.DAL.Repositories;

namespace System.BLL.Services
{
    public class DashboardService
    {
        private readonly DashboardRepository _dashboardRepository;

        public DashboardService(DashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        public async Task<List<OrderStatistic>> GetOrderStatistics() => await _dashboardRepository.GetOrderStatistics();
        public async Task<OrderCompletionRate> GetOrderCompletionRate() => await _dashboardRepository.GetOrderCompletionRate();
        public async Task<List<BestSellingProduct>> GetBestSellingProducts(int top = 5) => await _dashboardRepository.GetBestSellingProducts(top);
        public async Task<List<UserStatistic>> GetUserStatistics() => await _dashboardRepository.GetUserStatistics();

        // Thêm các phương thức mới
        public async Task<int> GetTotalUsers() => await _dashboardRepository.GetTotalUsers();
        public async Task<decimal> GetTotalRevenue() => await _dashboardRepository.GetTotalRevenue();
        public async Task<List<TopSellingProductDetail>> GetTopSellingProducts(int count = 5) => await _dashboardRepository.GetTopSellingProducts(count);
        public async Task<List<RevenueByMonth>> GetMonthlyRevenue() => await _dashboardRepository.GetMonthlyRevenue();
    }
}
