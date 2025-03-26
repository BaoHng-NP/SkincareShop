using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.BLL.Services
{
    public interface IDiscountService
    {
        Task<IEnumerable<Discount>> GetAllDiscountsAsync();
        Task<Discount> GetDiscountByIdAsync(int id);

        Task AddDiscountAsync(Discount discount);
        Task UpdateDiscountAsync(Discount discount);
        Task DeleteDiscountAsync(int id);
    }
}
