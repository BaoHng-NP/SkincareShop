using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace System.DAL.Repositories
{
    public class DashboardRepository
    {
        private readonly SkincareShopContext _context;

        public DashboardRepository(SkincareShopContext context)
        {
            _context = context;
        }

        public async Task<List<OrderStatistic>> GetOrderStatisticsByDateRange(DateTime startDate, DateTime endDate)
        {
            var data = await _context.Orders
                .Where(o => o.CreatedAt.HasValue && o.CreatedAt.Value >= startDate && o.CreatedAt.Value <= endDate)
                .GroupBy(o => new { o.CreatedAt.Value.Year, o.CreatedAt.Value.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    TotalOrders = g.Count()
                })
                .OrderBy(g => g.Year)
                .ThenBy(g => g.Month)
                .ToListAsync();

            return data
                .Select(g => new OrderStatistic
                {
                    Time = $"{g.Month}/{g.Year}",
                    TotalOrders = g.TotalOrders,
                    Date = new DateTime(g.Year, g.Month, 1)
                })
                .ToList();
        }

        // Get daily order statistics for smaller date ranges
        public async Task<List<OrderStatistic>> GetDailyOrderStatisticsByDateRange(DateTime startDate, DateTime endDate)
        {
            var data = await _context.Orders
                .Where(o => o.CreatedAt.HasValue && o.CreatedAt.Value >= startDate && o.CreatedAt.Value <= endDate)
                .GroupBy(o => new { o.CreatedAt.Value.Year, o.CreatedAt.Value.Month, o.CreatedAt.Value.Day })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Day = g.Key.Day,
                    TotalOrders = g.Count()
                })
                .OrderBy(g => g.Year)
                .ThenBy(g => g.Month)
                .ThenBy(g => g.Day)
                .ToListAsync();

            return data
                .Select(g => new OrderStatistic
                {
                    Time = $"{g.Day}/{g.Month}/{g.Year}",
                    TotalOrders = g.TotalOrders,
                    Date = new DateTime(g.Year, g.Month, g.Day)
                })
                .ToList();
        }

        public async Task<OrderCompletionRate> GetOrderCompletionRateByDateRange(DateTime startDate, DateTime endDate)
        {
            var completed = await _context.Orders.CountAsync(o =>
                (o.Status == "Completed-Feedback" || o.Status == "Completed-NonFeedback") &&
                o.CreatedAt.HasValue && o.CreatedAt.Value >= startDate && o.CreatedAt.Value <= endDate);

            var canceled = await _context.Orders.CountAsync(o =>
                o.Status == "Canceled" &&
                o.CreatedAt.HasValue && o.CreatedAt.Value >= startDate && o.CreatedAt.Value <= endDate);

            return new OrderCompletionRate
            {
                CompletedOrders = completed,
                CanceledOrders = canceled
            };
        }

        // Lấy số lượng user theo tháng trong khoảng thời gian
        public async Task<List<UserStatistic>> GetUserStatisticsByDateRange(DateTime startDate, DateTime endDate)
        {
            var data = await _context.Users
                .Where(u => u.CreatedAt.HasValue && u.CreatedAt.Value >= startDate && u.CreatedAt.Value <= endDate)
                .GroupBy(u => new { u.CreatedAt.Value.Year, u.CreatedAt.Value.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    TotalUsers = g.Count()
                })
                .OrderBy(g => g.Year)
                .ThenBy(g => g.Month)
                .ToListAsync();

            return data
                .Select(g => new UserStatistic
                {
                    Month = g.Month,
                    TotalUsers = g.TotalUsers,
                    Date = new DateTime(g.Year, g.Month, 1)
                })
                .ToList();
        }

        // Get daily user statistics for smaller date ranges
        public async Task<List<UserStatistic>> GetDailyUserStatisticsByDateRange(DateTime startDate, DateTime endDate)
        {
            var data = await _context.Users
                .Where(u => u.CreatedAt.HasValue && u.CreatedAt.Value >= startDate && u.CreatedAt.Value <= endDate)
                .GroupBy(u => new { u.CreatedAt.Value.Year, u.CreatedAt.Value.Month, u.CreatedAt.Value.Day })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Day = g.Key.Day,
                    TotalUsers = g.Count()
                })
                .OrderBy(g => g.Year)
                .ThenBy(g => g.Month)
                .ThenBy(g => g.Day)
                .ToListAsync();

            return data
                .Select(g => new UserStatistic
                {
                    Month = g.Month,
                    Day = g.Day,
                    TotalUsers = g.TotalUsers,
                    Date = new DateTime(g.Year, g.Month, g.Day)
                })
                .ToList();
        }

        // Lấy tổng số user
        public async Task<int> GetTotalUsers()
        {
            return await _context.Users.CountAsync();
        }

        // Lấy tổng doanh thu từ các đơn hàng đã hoàn thành
        public async Task<decimal> GetTotalRevenue()
        {
            // Updated to include both completed status types
            return await _context.Orders
                .Where(o => o.Status == "Completed-Feedback" || o.Status == "Completed-NonFeedback")
                .SumAsync(o => o.TotalPrice);
        }

        public async Task<decimal> GetTotalRevenueByDateRange(DateTime startDate, DateTime endDate)
        {
            return await _context.Orders
                .Where(o => (o.Status == "Completed-Feedback" || o.Status == "Completed-NonFeedback") &&
                       o.CreatedAt.HasValue && o.CreatedAt.Value >= startDate && o.CreatedAt.Value <= endDate)
                .SumAsync(o => o.TotalPrice);
        }

        // Top sản phẩm bán chạy
        public async Task<List<TopSellingProductDetail>> GetTopSellingProductsByDateRange(DateTime startDate, DateTime endDate, int count = 5)
        {
            return await _context.OrderItems
                // Filter orders by date range and completed status
                .Where(oi => (oi.Order.Status == "Completed-Feedback" ||
                             oi.Order.Status == "Completed-NonFeedback") &&
                             oi.Order.CreatedAt.HasValue &&
                             oi.Order.CreatedAt.Value >= startDate &&
                             oi.Order.CreatedAt.Value <= endDate &&
                             oi.Product != null)
                .GroupBy(oi => new { oi.ProductId, oi.Product.Name, oi.Product.ImageUrl, oi.Product.Price })
                .Select(g => new TopSellingProductDetail
                {
                    ProductId = g.Key.ProductId.Value,
                    ProductName = g.Key.Name,
                    ImageUrl = g.Key.ImageUrl,
                    Price = g.Key.Price,
                    TotalQuantity = g.Sum(oi => oi.Quantity),
                    TotalRevenue = g.Sum(oi => oi.Quantity * oi.UnitPrice)
                })
                .OrderByDescending(p => p.TotalQuantity)
                .Take(count)
                .ToListAsync();
        }

        // Lấy doanh thu theo tháng
        public async Task<List<RevenueByMonth>> GetMonthlyRevenueByDateRange(DateTime startDate, DateTime endDate)
        {
            var data = await _context.Orders
                .Where(o => (o.Status == "Completed-Feedback" || o.Status == "Completed-NonFeedback") &&
                       o.CreatedAt.HasValue && o.CreatedAt.Value >= startDate && o.CreatedAt.Value <= endDate)
                .GroupBy(o => new { o.CreatedAt.Value.Year, o.CreatedAt.Value.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    TotalRevenue = g.Sum(o => o.TotalPrice)
                })
                .OrderBy(g => g.Year)
                .ThenBy(g => g.Month)
                .ToListAsync();

            return data
                .Select(g => new RevenueByMonth
                {
                    Period = $"{g.Month}/{g.Year}",
                    Revenue = g.TotalRevenue,
                    Date = new DateTime(g.Year, g.Month, 1)
                })
                .ToList();
        }

        // Get daily revenue for smaller date ranges
        public async Task<List<RevenueByDay>> GetDailyRevenueByDateRange(DateTime startDate, DateTime endDate)
        {
            var data = await _context.Orders
                .Where(o => (o.Status == "Completed-Feedback" || o.Status == "Completed-NonFeedback") &&
                       o.CreatedAt.HasValue && o.CreatedAt.Value >= startDate && o.CreatedAt.Value <= endDate)
                .GroupBy(o => new { o.CreatedAt.Value.Year, o.CreatedAt.Value.Month, o.CreatedAt.Value.Day })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Day = g.Key.Day,
                    TotalRevenue = g.Sum(o => o.TotalPrice)
                })
                .OrderBy(g => g.Year)
                .ThenBy(g => g.Month)
                .ThenBy(g => g.Day)
                .ToListAsync();

            return data
                .Select(g => new RevenueByDay
                {
                    Period = $"{g.Day}/{g.Month}/{g.Year}",
                    Revenue = g.TotalRevenue,
                    Date = new DateTime(g.Year, g.Month, g.Day)
                })
                .ToList();
        }

        public async Task<List<int>> GetAvailableDataYears()
        {
            // Get years from Orders data
            var orderYears = await _context.Orders
                .Where(o => o.CreatedAt.HasValue)
                .Select(o => o.CreatedAt.Value.Year)
                .Distinct()
                .ToListAsync();

            // Get years from Users data (for user growth chart)
            var userYears = await _context.Users
                .Where(u => u.CreatedAt.HasValue)
                .Select(u => u.CreatedAt.Value.Year)
                .Distinct()
                .ToListAsync();

            // Combine and sort years
            return orderYears.Union(userYears).OrderByDescending(y => y).ToList();
        }

        public async Task<List<ProductStockStatus>> GetLowStockProducts(int threshold = 10, int count = 5)
        {
            return await _context.Products
                .Where(p => p.Stock.HasValue && p.Stock.Value <= threshold && p.Stock.Value > 0)
                .OrderBy(p => p.Stock)
                .Select(p => new ProductStockStatus
                {
                    ProductId = p.Id,
                    ProductName = p.Name,
                    ImageUrl = p.ImageUrl,
                    CurrentStock = p.Stock.Value,
                    Price = p.Price
                })
                .Take(count)
                .ToListAsync();
        }

        public async Task<List<ProductStockStatus>> GetOutOfStockProducts(int count = 5)
        {
            return await _context.Products
                .Where(p => p.Stock.HasValue && p.Stock.Value == 0)
                .OrderBy(p => p.Name)
                .Select(p => new ProductStockStatus
                {
                    ProductId = p.Id,
                    ProductName = p.Name,
                    ImageUrl = p.ImageUrl,
                    CurrentStock = 0,
                    Price = p.Price
                })
                .Take(count)
                .ToListAsync();
        }
    }

    // Model trả về dữ liệu thống kê
    public class OrderStatistic
    {
        public string Time { get; set; }
        public int TotalOrders { get; set; }
        public DateTime Date { get; set; } // Property for filtering
    }

    public class OrderCompletionRate
    {
        public int CompletedOrders { get; set; }
        public int CanceledOrders { get; set; }
    }

    public class UserStatistic
    {
        public int Month { get; set; }
        public int Day { get; set; }
        public int TotalUsers { get; set; }
        public DateTime Date { get; set; }
    }

    // Các model cho các chức năng khác
    public class TopSellingProductDetail
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int TotalQuantity { get; set; }
        public decimal TotalRevenue { get; set; }
    }

    public class RevenueByMonth
    {
        public string Period { get; set; }
        public decimal Revenue { get; set; }
        public DateTime Date { get; set; } // Property for filtering
    }

    public class RevenueByDay
    {
        public string Period { get; set; }
        public decimal Revenue { get; set; }
        public DateTime Date { get; set; }
    }

    public class ProductStockStatus
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public int CurrentStock { get; set; }
        public decimal Price { get; set; }
    }
}
