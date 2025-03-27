using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.BLL.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product?> GetProductByIdAsync(int id);
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);
        Task<IEnumerable<Product>> GetProductsByFilterAsync(int? categoryId, int? brandId);
        Task<IEnumerable<Product>> GetProductsBySkinTypeAsync(int skinTypeId);
        Task<IEnumerable<Product>> GetAllProductsByCateIdAsync(int? cateId);
    }
}
