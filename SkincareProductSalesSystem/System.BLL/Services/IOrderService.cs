using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.BLL.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<IEnumerable<Order>> GetAllOrdersByuserId(int userId);
        Task<Order?> GetOrderByIdAsync(int id);
        Task AddOrderAsync(Order order, List<CartItem> cartItems);
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(int id);
    }
}
