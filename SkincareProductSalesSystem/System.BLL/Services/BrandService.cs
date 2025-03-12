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
    public class BrandService : IBrandService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Brand> _repository;
        public BrandService(IUnitOfWork unitOfWork, IGenericRepository<Brand> genericRepository)
        {
            _repository = genericRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task AddBrandAsync(Brand brand)
        {
            if (brand == null)
                throw new ArgumentNullException(nameof(brand));

            _repository.Create(brand);
            await _unitOfWork.SaveChange();
        }

        public async Task DeleteBrandAsync(int id)
        {
            var brand = await _repository.FindById(id);
            if (brand == null)
                throw new ArgumentException("brand not found");

            _repository.Delete(brand);
            await _unitOfWork.SaveChange();
        }

        public async Task<IEnumerable<Brand>> GetAllBrandAsync()
        {
            return await _repository.FindAll().ToListAsync();
        }

        public async Task<Brand?> GetBrandByIdAsync(int id)
        {
            return await _repository.FindById(id);
        }

        public async Task UpdateBrandAsync(Brand brand)
        {
            if (brand == null)
                throw new ArgumentNullException(nameof(brand));

            _repository.Update(brand);
            await _unitOfWork.SaveChange();
        }

        public async Task<IEnumerable<Brand>> GetAllActiveBrandAsync()
        {
            return await _repository.FindAll(u => u.IsActive == true).ToListAsync() ?? new List<Brand>();
        }
    }
}
