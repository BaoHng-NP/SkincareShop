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
    public class ProductDetailService : IProductDetailService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<ProductDetail> _repository;
        public ProductDetailService(IUnitOfWork unitOfWork, IGenericRepository<ProductDetail> genericRepository)
        {
            _repository = genericRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<IList<ProductDetail>> GetPoductDetailServiceByIdAsync(int id)
        {
            return await _repository
                .FindAll(p => p.ProductId == id, p => p.Product)
                .ToListAsync(); 
        }
        public async Task AddPoductDetailServiceAsync(ProductDetail userVoucher)
        {
            if (userVoucher == null)
                throw new ArgumentNullException(nameof(userVoucher));

            _repository.Create(userVoucher);
            await _unitOfWork.SaveChange();
        }
    }
}
