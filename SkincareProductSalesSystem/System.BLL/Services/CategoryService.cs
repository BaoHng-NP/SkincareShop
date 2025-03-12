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
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Category> _repository;
        public CategoryService(IUnitOfWork unitOfWork, IGenericRepository<Category> genericRepository)
        {
            _repository = genericRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task AddCategoryAsync(Category category)
        {
            if (category == null)
                throw new ArgumentNullException(nameof(category));

            _repository.Create(category);
            await _unitOfWork.SaveChange();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _repository.FindById(id);
            if (category == null)
                throw new ArgumentException("category not found");

            _repository.Delete(category);
            await _unitOfWork.SaveChange();
        }

        public async Task<IEnumerable<Category>> GetAllCategoryAsync()
        {
            return await _repository.FindAll().ToListAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await _repository.FindById(id);
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            if (category == null)
                throw new ArgumentNullException(nameof(category));

            _repository.Update(category);
            await _unitOfWork.SaveChange();
        }

        public async Task<IEnumerable<Category>> GetAllActiveCategoryAsync()
        {
            return await _repository.FindAll(u => u.IsActive == true).ToListAsync() ?? new List<Category>();
        }
        
    }
}
