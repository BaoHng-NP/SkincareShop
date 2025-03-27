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

        // 1️⃣ Thống kê số đơn hàng theo tháng
        public async Task<List<OrderStatistic>> GetOrderStatistics()
        {
            var data = await _context.Orders
                .Where(o => o.CreatedAt.HasValue)
                .GroupBy(o => new { o.CreatedAt.Value.Year, o.CreatedAt.Value.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    TotalOrders = g.Count()
                })
                .OrderBy(g => g.Year)
                .ThenBy(g => g.Month)
                .ToListAsync(); // ✅ Lấy dữ liệu từ DB trước

            return data
                .Select(g => new OrderStatistic
                {
                    Time = $"{g.Month}/{g.Year}", // ✅ Xử lý string sau khi tải dữ liệu
                    TotalOrders = g.TotalOrders
                })
                .ToList(); // ✅ Dùng ToList() vì lúc này đang xử lý trên List<T>
        }


        // 2️⃣ Tính tỷ lệ đơn hàng hoàn thành vs hủy
        public async Task<OrderCompletionRate> GetOrderCompletionRate()
        {
            // Updated to include both completed status types
            var completed = await _context.Orders.CountAsync(o =>
                o.Status == "Completed-Feedback" || o.Status == "Completed-NonFeedback");
            var canceled = await _context.Orders.CountAsync(o => o.Status == "Canceled");

            return new OrderCompletionRate
            {
                CompletedOrders = completed,
                CanceledOrders = canceled
            };
        }

        // 3️⃣ Lấy sản phẩm bán chạy nhất
        public async Task<List<BestSellingProduct>> GetBestSellingProducts(int top = 5)
        {
            return await _context.OrderItems
                .Where(oi => oi.Product != null)
                .GroupBy(oi => oi.Product.Name)
                .Select(g => new BestSellingProduct
                {
                    ProductName = g.Key,
                    TotalSold = g.Sum(oi => oi.Quantity)
                })
                .OrderByDescending(p => p.TotalSold)
                .Take(top)
                .ToListAsync();
        }

        // 4️⃣ Thống kê số lượng user theo tháng
        public async Task<List<UserStatistic>> GetUserStatistics()
        {
            return await _context.Users
                .Where(u => u.CreatedAt.HasValue)
                .GroupBy(u => new { u.CreatedAt.Value.Year, u.CreatedAt.Value.Month })
                .Select(g => new UserStatistic
                {
                    Month = g.Key.Month,
                    TotalUsers = g.Count()
                })
                .OrderBy(u => u.Month)
                .ToListAsync();
        }

        // 5️⃣ Lấy tổng số user
        public async Task<int> GetTotalUsers()
        {
            return await _context.Users.CountAsync();
        }

        // 6️⃣ Lấy tổng doanh thu từ các đơn hàng đã hoàn thành
        public async Task<decimal> GetTotalRevenue()
        {
            // Updated to include both completed status types
            return await _context.Orders
                .Where(o => o.Status == "Completed-Feedback" || o.Status == "Completed-NonFeedback")
                .SumAsync(o => o.TotalPrice);
        }

        // 7️⃣ Lấy chi tiết sản phẩm bán chạy nhất
        public async Task<List<TopSellingProductDetail>> GetTopSellingProducts(int count = 5)
        {
            return await _context.OrderItems
                // Updated to include both completed status types
                .Where(oi => (oi.Order.Status == "Completed-Feedback" ||
                             oi.Order.Status == "Completed-NonFeedback") &&
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

        // 8️⃣ Lấy doanh thu theo tháng
        public async Task<List<RevenueByMonth>> GetMonthlyRevenue()
        {
            var data = await _context.Orders
                // Updated to include both completed status types
                .Where(o => (o.Status == "Completed-Feedback" || o.Status == "Completed-NonFeedback") &&
                       o.CreatedAt.HasValue)
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
                    Revenue = g.TotalRevenue
                })
                .ToList();
        }
    }

    // Model trả về dữ liệu thống kê
    public class OrderStatistic { public string Time { get; set; } public int TotalOrders { get; set; } }
    public class OrderCompletionRate { public int CompletedOrders { get; set; } public int CanceledOrders { get; set; } }
    public class BestSellingProduct { public string ProductName { get; set; } public int TotalSold { get; set; } }
    public class UserStatistic { public int Month { get; set; } public int TotalUsers { get; set; } }

    // Thêm các class mới cho các chức năng mới
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
    }
}
