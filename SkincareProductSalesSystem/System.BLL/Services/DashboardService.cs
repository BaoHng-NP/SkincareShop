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

        public async Task<List<OrderStatistic>> GetOrderStatistics() => await _dashboardRepository.GetOrderStatistics();
        public async Task<OrderCompletionRate> GetOrderCompletionRate() => await _dashboardRepository.GetOrderCompletionRate();
        public async Task<List<BestSellingProduct>> GetBestSellingProducts(int top = 5) => await _dashboardRepository.GetBestSellingProducts(top);
        public async Task<List<UserStatistic>> GetUserStatistics() => await _dashboardRepository.GetUserStatistics();

        // Thêm các phương thức mới
        public async Task<int> GetTotalUsers() => await _dashboardRepository.GetTotalUsers();
        public async Task<decimal> GetTotalRevenue() => await _dashboardRepository.GetTotalRevenue();
        public async Task<List<TopSellingProductDetail>> GetTopSellingProducts(int count = 5) => await _dashboardRepository.GetTopSellingProducts(count);
        public async Task<List<RevenueByMonth>> GetMonthlyRevenue() => await _dashboardRepository.GetMonthlyRevenue();

        public async Task<List<OrderStatistic>> GetOrderStatisticsByDateRange(DateTime startDate, DateTime endDate) =>
            await _dashboardRepository.GetOrderStatisticsByDateRange(startDate, endDate);

        public async Task<OrderCompletionRate> GetOrderCompletionRateByDateRange(DateTime startDate, DateTime endDate) =>
            await _dashboardRepository.GetOrderCompletionRateByDateRange(startDate, endDate);

        public async Task<decimal> GetTotalRevenueByDateRange(DateTime startDate, DateTime endDate) =>
            await _dashboardRepository.GetTotalRevenueByDateRange(startDate, endDate);

        public async Task<List<RevenueByMonth>> GetMonthlyRevenueByDateRange(DateTime startDate, DateTime endDate) =>
            await _dashboardRepository.GetMonthlyRevenueByDateRange(startDate, endDate);

        public async Task<List<int>> GetAvailableDataYears() =>
            await _dashboardRepository.GetAvailableDataYears();

        public async Task<List<UserStatistic>> GetUserStatisticsByDateRange(DateTime startDate, DateTime endDate) =>
            await _dashboardRepository.GetUserStatisticsByDateRange(startDate, endDate);

        public async Task<List<TopSellingProductDetail>> GetTopSellingProductsByDateRange(DateTime startDate, DateTime endDate, int count = 5) =>
            await _dashboardRepository.GetTopSellingProductsByDateRange(startDate, endDate, count);

        public async Task<List<ProductStockStatus>> GetLowStockProducts(int threshold = 10, int count = 5) =>
            await _dashboardRepository.GetLowStockProducts(threshold, count);

        public async Task<List<ProductStockStatus>> GetOutOfStockProducts(int count = 5) =>
            await _dashboardRepository.GetOutOfStockProducts(count);

        public (DateTime startDate, DateTime endDate) GetDateRangeForPeriod(string period, int year = 0)
        {
            DateTime now = DateTime.Now;
            int targetYear = year > 0 ? year : now.Year;
            DateTime startDate, endDate;

            switch (period)
            {
                case "12months":
                    startDate = new DateTime(now.Year, now.Month, 1).AddMonths(-11);
                    endDate = new DateTime(now.Year, now.Month, DateTime.DaysInMonth(now.Year, now.Month));
                    break;
                case "year":
                    startDate = new DateTime(targetYear, 1, 1);
                    endDate = new DateTime(targetYear, 12, 31);
                    break;
                case "custom":
                    // This will be handled in the controller/page model
                    startDate = DateTime.MinValue;
                    endDate = DateTime.MaxValue;
                    break;
                default:
                    startDate = new DateTime(now.Year, now.Month, 1).AddMonths(-11);
                    endDate = new DateTime(now.Year, now.Month, DateTime.DaysInMonth(now.Year, now.Month));
                    break;
            }

            return (startDate, endDate);
        }

        public async Task<(List<DailyRevenue> Revenue, List<DailyOrder> Orders)> GetDailyRevenueAndOrdersForRange(
            DateTime startDate, DateTime endDate)
        {
            var revenue = await _dashboardRepository.GetDailyRevenueForRange(startDate, endDate);
            var orders = await _dashboardRepository.GetDailyOrdersForRange(startDate, endDate);

            return (revenue, orders);
        }
    }
}
