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
    public class DiscountService : IDiscountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Discount> _repository;
        public DiscountService(IUnitOfWork unitOfWork, IGenericRepository<Discount> genericRepository)
        {
            _repository = genericRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task AddDiscountAsync(Discount discount)
        {
            if (discount == null)
                throw new ArgumentNullException(nameof(discount));

            _repository.Create(discount);
            await _unitOfWork.SaveChange();
        }

        public async Task DeleteDiscountAsync(int id)
        {
            var category = await _repository.FindById(id);
            if (category == null)
                throw new ArgumentException("category not found");

            _repository.Delete(category);
            await _unitOfWork.SaveChange();
        }

        public async Task<IEnumerable<Discount>> GetAllDiscountsAsync()
        {

            return await _repository.FindAll().ToListAsync() ;
        }

        public async Task<Discount> GetDiscountByIdAsync(int id)
        {
            return await _repository.FindById(id);
        }

        public async Task UpdateDiscountAsync(Discount discount)
        {
            throw new NotImplementedException();
        }
    }
}
