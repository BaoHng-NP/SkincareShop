using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.BLL.UnitOfWorks;
using System.Collections.Generic;
using System.DAL.Repositories;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Order> _repository;
        private readonly IGenericRepository<OrderItem> _orderDetailRepository;
        private readonly IGenericRepository<ProductDetail> _productDetailRepository;
        private readonly IProductService _productService;
        private readonly IOrderRepository _orderRepository;
        public OrderService(IUnitOfWork unitOfWork, IGenericRepository<Order> genericRepository, IGenericRepository<OrderItem> orderDetailRepository, IOrderRepository orderRepository,IGenericRepository<ProductDetail> productDetailRepository, IProductService productService)
        {
            _repository = genericRepository;
            _unitOfWork = unitOfWork;
            _orderDetailRepository = orderDetailRepository;
            _orderRepository = orderRepository;
            _productDetailRepository = productDetailRepository;
            _productService = productService;
        }
        public async Task AddOrderAsync(Order order, List<CartItem> cartItems)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));
            if (cartItems == null || !cartItems.Any())
                throw new ArgumentException("CartItems is null or empty.");
            if (_productDetailRepository == null)
                throw new NullReferenceException("ProductDetailRepository is not initialized.");

            await _unitOfWork.BeginTransactionAsync(); // 🚀 Bắt đầu transaction

            try
            {
                // Thêm đơn hàng vào database
                _repository.Create(order);
                await _unitOfWork.SaveChange(); // Lưu để có order.Id

                foreach (var item in cartItems)
                {
                    var totalStock = await _productDetailRepository
                        .FindAll(p => p.ProductId == item.ProductId && p.Quantity > 0)
                        .SumAsync(p => p.Quantity);

                    if (totalStock < item.Quantity)
                    {
                        throw new InvalidOperationException($"Not enough stock for {item.ProductName}, Stock: {totalStock}");
                    }

                    var orderDetail = new OrderItem
                    {
                        OrderId = order.Id,
                        ProductId = item.ProductId,
                        Quantity = (int)item.Quantity,
                        UnitPrice = (decimal)item.Price
                    };
                    _orderDetailRepository.Create(orderDetail);

                    var productDetails = await _productDetailRepository
                        .FindAll(p => p.ProductId == item.ProductId && p.Quantity > 0)
                        .OrderBy(p => p.CreatedDate)
                        .ToListAsync();

                    int remainingQuantity = (int)item.Quantity;

                    foreach (var detail in productDetails)
                    {
                        if (remainingQuantity <= 0) break;

                        if (detail.Quantity >= remainingQuantity)
                        {
                            detail.Quantity -= remainingQuantity;
                            remainingQuantity = 0;
                        }
                        else
                        {
                            remainingQuantity -= (int)detail.Quantity;
                            detail.Quantity = 0;
                        }

                        _productDetailRepository.Update(detail);
                    }

                    // Cập nhật tổng số lượng trong bảng Product
                    var product = await _productService.GetProductByIdAsync(item.ProductId);
                    if (product != null)
                    {
                        product.Stock = productDetails.Sum(p => p.Quantity);
                        await _productService.UpdateProductAsync(product);
                    }
                }

                await _unitOfWork.SaveChange(); // Lưu thay đổi
                await _unitOfWork.CommitTransactionAsync(); // 🚀 Commit nếu thành công
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync(); // 🔥 Rollback nếu có lỗi
                throw; // Ném lỗi để xử lý ở chỗ khác
            }
        }




        public async Task DeleteOrderAsync(int id)
        {
            var order = await _repository.FindById(id);
            if (order == null)
                throw new ArgumentException("order not found");

            _repository.Delete(order);
            await _unitOfWork.SaveChange();
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _repository.FindAll(null, o => o.OrderItems, o => o.User).OrderByDescending(o=>o.CreatedAt).ToListAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            return await _orderRepository.FindById(id, o => o.User);
        }
        public async Task<IEnumerable<Order>> GetAllOrdersByuserId(int userId)
        {
            return await _repository.FindAll(o => o.UserId == userId, o => o.OrderItems, o => o.User).ToListAsync();
        }
        public async Task UpdateOrderAsync(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            _repository.Update(order);
            await _unitOfWork.SaveChange();
        }
    }
}
