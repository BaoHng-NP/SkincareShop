using System;
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

        public async Task<int> GetTotalUsers() => await _dashboardRepository.GetTotalUsers();
        public async Task<decimal> GetTotalRevenue() => await _dashboardRepository.GetTotalRevenue();

        public async Task<List<OrderStatistic>> GetOrderStatisticsByDateRange(DateTime startDate, DateTime endDate) =>
            await _dashboardRepository.GetOrderStatisticsByDateRange(startDate, endDate);

        public async Task<List<OrderStatistic>> GetDailyOrderStatisticsByDateRange(DateTime startDate, DateTime endDate) =>
            await _dashboardRepository.GetDailyOrderStatisticsByDateRange(startDate, endDate);

        public async Task<OrderCompletionRate> GetOrderCompletionRateByDateRange(DateTime startDate, DateTime endDate) =>
            await _dashboardRepository.GetOrderCompletionRateByDateRange(startDate, endDate);

        public async Task<decimal> GetTotalRevenueByDateRange(DateTime startDate, DateTime endDate) =>
            await _dashboardRepository.GetTotalRevenueByDateRange(startDate, endDate);

        public async Task<List<RevenueByMonth>> GetMonthlyRevenueByDateRange(DateTime startDate, DateTime endDate) =>
            await _dashboardRepository.GetMonthlyRevenueByDateRange(startDate, endDate);

        public async Task<List<RevenueByDay>> GetDailyRevenueByDateRange(DateTime startDate, DateTime endDate) =>
            await _dashboardRepository.GetDailyRevenueByDateRange(startDate, endDate);

        public async Task<List<int>> GetAvailableDataYears() =>
            await _dashboardRepository.GetAvailableDataYears();

        public async Task<List<UserStatistic>> GetUserStatisticsByDateRange(DateTime startDate, DateTime endDate) =>
            await _dashboardRepository.GetUserStatisticsByDateRange(startDate, endDate);

        public async Task<List<UserStatistic>> GetDailyUserStatisticsByDateRange(DateTime startDate, DateTime endDate) =>
            await _dashboardRepository.GetDailyUserStatisticsByDateRange(startDate, endDate);

        public async Task<List<TopSellingProductDetail>> GetTopSellingProductsByDateRange(DateTime startDate, DateTime endDate, int count = 5) =>
            await _dashboardRepository.GetTopSellingProductsByDateRange(startDate, endDate, count);

        public async Task<List<ProductStockStatus>> GetLowStockProducts(int threshold = 10, int count = 5) =>
            await _dashboardRepository.GetLowStockProducts(threshold, count);

        public async Task<List<ProductStockStatus>> GetOutOfStockProducts(int count = 5) =>
            await _dashboardRepository.GetOutOfStockProducts(count);

        // Helper method to determine if daily data should be used
        public bool ShouldUseDailyData(DateTime startDate, DateTime endDate)
        {
            // Use daily data if the date range is less than a month
            int daysDifference = (endDate - startDate).Days;
            return daysDifference < 31;
        }
    }
}
