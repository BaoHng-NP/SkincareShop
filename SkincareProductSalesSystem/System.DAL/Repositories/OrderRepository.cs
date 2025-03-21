using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace System.DAL.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly SkincareShopContext _context;

        public OrderRepository(SkincareShopContext context)
        {
            _context = context;
        }

        public async Task<Order?> FindById(int id, params Expression<Func<Order, object>>[] includes)
        {
            IQueryable<Order> query = _context.Orders;

            // Áp dụng các Include từ tham số
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            // Thêm `ThenInclude` để lấy Product từ OrderItems
            query = query.Include(o => o.OrderItems)
                         .ThenInclude(oi => oi.Product);

            return await query.FirstOrDefaultAsync(o => o.Id == id);
        }
  

    }
}
